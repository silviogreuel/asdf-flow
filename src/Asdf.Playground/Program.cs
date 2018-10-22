using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Asdf.Domain.Actions;
using Asdf.Kernel;
using Asdf.Domain.Flows;
using Asdf.Domain.Triggers;
using Asdf.Domain.Templates;
using Asdf.Application.Database;
using Asdf.Domain.Users;

namespace Asdf.Playground
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //Execute trigger (/buttons/1/trigger)
            /*
            using (var db = new AsdfContext())
            {
                long? id = 1;
                var trigger = (ICanBeExecutable)db.Triggers.Find(id);
                trigger.ExecuteAsync().Wait();

                db.SaveChanges();
            }
            */


            //Base Template
            using (var db = new AsdfContext())
            {
                var templates = new List<NodeTemplate>
                {
                    new NodeTemplate
                    {
                        Name = "HTTP GET",
                        ActivatorType = typeof(HttpGetNode).FullName,
                        ActivatorAssembly = typeof(HttpGetNode).Assembly.FullName,
                        Fields = new List<FieldTemplate>
                        {
                            new FieldTemplate() { Name = "Name", Type = typeof(String).FullName },
                            new FieldTemplate() { Name = "Url", Type = typeof(String).FullName },
                            new FieldTemplate() { Name = "Field", Type = typeof(String).FullName },
                        }
                    },
                    new NodeTemplate
                    {
                        Name = "HTTP POST",
                        ActivatorType = typeof(HttpPostNode).FullName,
                        ActivatorAssembly = typeof(HttpPostNode).Assembly.FullName,
                        Fields = new List<FieldTemplate>
                        {
                            new FieldTemplate() { Name = "Name", Type = typeof(String).FullName },
                            new FieldTemplate() { Name = "Url", Type = typeof(String).FullName },
                            new FieldTemplate() { Name = "Content", Type = typeof(String).FullName },
                            new FieldTemplate() { Name = "Content-Type", Type = typeof(String).FullName },
                        }
                    },
                    new NodeTemplate
                    {
                        Name = "MQTT TOPIC",
                        ActivatorType = typeof(HttpGetNode).FullName,
                        ActivatorAssembly = typeof(HttpGetNode).Assembly.FullName,
                        Fields = new List<FieldTemplate>
                        {
                            new FieldTemplate() { Name = "Name", Type = typeof(String).FullName },
                            new FieldTemplate() { Name = "Device", Type = typeof(Guid).FullName },
                            new FieldTemplate() { Name = "Topic", Type = typeof(String).FullName },
                            new FieldTemplate() { Name = "Content", Type = typeof(String).FullName },
                        }
                    }

                };

                db.AddRange(templates);
                db.SaveChanges();
            }

            //Creating new flow
            using (var db = new AsdfContext())
            {
                var flow = new Flow(new User(),"Request CNPJ info");
                await db.AddAsync(flow);
                await db.SaveChangesAsync();
            }

            //Creating a button trigger
            using (var db = new AsdfContext())
            {
                long? id = 1;
                var flow = db.Flows.Find(id);

                var buttonTrigger = new ButtonTrigger(new User(), "Manual");

                flow.AddTrigger(buttonTrigger);

                await db.SaveChangesAsync();
            }

            using (var db = new AsdfContext())
            {
                long? id = 1;
                var trigger = db.Triggers.Find(id);

                var attributeNode = new AttributeNode(new User(), "Adding CNPJ", "cnpj", "00447041000120");

                trigger.AddRoot(attributeNode);

                await db.SaveChangesAsync();
            }

            //Add new action to trigger
            using (var db = new AsdfContext())
            {
                long? id = 1;
                var template = db.NodeTemplates.Find(id);
                var arguments = new object[]
                {
                    "Query CNPJ",
                    "https://www.receitaws.com.br/v1/cnpj/{0:cnpj}",
                    "response",
                };
                var child = template.Activate(arguments);

                var parent = db.Nodes.Find(id);
                parent.AddPass(child);

                db.SaveChanges();
            }

            //Execute trigger (/buttons/1/trigger)
            using (var db = new AsdfContext())
            {
                long? id = 1;
                var trigger = (ICanBeExecutable)db.Triggers.Find(id);
                trigger.ExecuteAsync().Wait();

                db.SaveChanges();
            }
        }
    }
}
