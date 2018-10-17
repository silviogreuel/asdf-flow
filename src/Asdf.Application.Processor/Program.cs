using RawRabbit;
using RawRabbit.Instantiation;
using System.Threading.Tasks;
using RawRabbit.Configuration;
using System;
using System.IO;
using Asdf.Application.Processor.Enrichers.PlainText;
using Microsoft.Extensions.Configuration;
using RawRabbit.Configuration.BasicPublish;
using Serilog;

namespace Asdf.Application.Processor
{
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
                .AddJsonFile("rawrabbit.json")
                .Build()
                .Get<RawRabbitConfiguration>();

            var options = new RawRabbitOptions()
            {
                ClientConfiguration = configuration,
                Plugins = p => p.UsePlainText(),
            };

            _bus = RawRabbitFactory.CreateSingleton(options);

            await _bus.BindQueueAsync("string", "amq.topic", "*");
            await _bus.SubscribeAsync<string>(async msg =>
            {
               Console.WriteLine(msg);
            });
        }
    }
}
    