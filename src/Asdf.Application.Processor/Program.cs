using RawRabbit;
using RawRabbit.Instantiation;
using System.Threading.Tasks;
using RawRabbit.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Asdf.Application.Database;
using Asdf.Application.Processor.Dynamics;
using Asdf.Application.Processor.Enrichers.PlainText;
using Asdf.Kernel.Utils;
using Castle.Core.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RawRabbit.Configuration.BasicPublish;
using RawRabbit.Pipe;
using Serilog;

namespace Asdf.Application.Processor
{
    public static class Env
    {
        public static string Name()
        {
#if (DEBUG)
            return "Debug";
#else
            return "Release";
#endif
        }
}

class Program
    {
        private static IBusClient _bus;

        static void Main(string[] args)
        {
            RunAsync().GetAwaiter().GetResult();
        }

        static async Task RunAsync()
        {
            Log.Logger = new LoggerConfiguration()
				.WriteTo.LiterateConsole()
				.CreateLogger();

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"rawrabbit.{Env.Name()}.json")
                .Build()
                .Get<RawRabbitConfiguration>();

            var options = new RawRabbitOptions()
            {
                ClientConfiguration = configuration,
                Plugins = p => p.UsePlainText(),
            };

            _bus = RawRabbitFactory.CreateSingleton(options);

            await _bus.DeclareQueueAsync<string>();
            await _bus.BindQueueAsync("string", "amq.topic", "#");

            await _bus.SubscribeAsync<string>(async (msg) =>
            {
                Log.Logger.Information(msg);
                var fields = msg
                    .Split("\n")
                    .Where(field => field != "!")
                    .Select(line => new DynamicField(line))
                    .ToList();

                IDictionary<string, dynamic> context = new Dictionary<string, dynamic>();
                foreach (var field in fields)
                {
                    context[field.Name] = field.Value;
                }

                var token = fields.Where(f => f.Name == "token").FirstOrDefault();
                if (token == null)
                    return;

                using (var db = new AsdfContext())
                {
                    Guid userToken = (Guid)token.Value;
                    var triggers = await db.Mqtts.Where(f => f.User.Token == userToken).ToListAsync();
                    if (triggers.IsNullOrEmpty())
                        return;

                    foreach (var trigger in triggers)
                    {
                        trigger.Context = context;
                        await trigger.ExecuteAsync();
                        
                    }

                    db.UpdateRange(triggers);
                    await db.SaveChangesAsync();
                }

                await Task.CompletedTask;
            });

            GlobalBus.Publish = async (exchange, routing, body) =>
            {
                await _bus.BasicPublishAsync(new BasicPublishConfiguration()
                {
                    ExchangeName = exchange,
                    RoutingKey = routing,
                    Body =  body
                });
            };
        }
    }
}
    