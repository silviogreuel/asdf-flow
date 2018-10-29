using Asdf.Domain.Actions;
using Asdf.Domain.Templates;
using Microsoft.EntityFrameworkCore;

namespace Asdf.Application.Database.Seeds
{
    public static class NodeTemplateSeeder
    {
        public static ModelBuilder SeedNodeTemplates(this ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<NodeTemplate>()
                .HasData(
                    new NodeTemplate
                    {
                        Id = 1,
                        Name = "HTTP GET",
                        ActivatorType = typeof(HttpGetNode).FullName,
                        ActivatorAssembly = typeof(HttpGetNode).Assembly.FullName,
                    },
                    new NodeTemplate
                    {
                        Id = 2,
                        Name = "HTTP POST",
                        ActivatorType = typeof(HttpPostNode).FullName,
                        ActivatorAssembly = typeof(HttpPostNode).Assembly.FullName,
                    },
                    new NodeTemplate
                    {
                        Id = 3,
                        Name = "ATTRIBUTE",
                        ActivatorType = typeof(AttributeNode).FullName,
                        ActivatorAssembly = typeof(AttributeNode).Assembly.FullName,
                    },
                    new NodeTemplate
                    {
                        Id = 4,
                        Name = "MQTT PUBLISH",
                        ActivatorType = typeof(MqttPublishNode).FullName,
                        ActivatorAssembly = typeof(MqttPublishNode).Assembly.FullName,
                    },
                    new NodeTemplate
                    {
                        Id = 5,
                        Name = "DECISION",
                        ActivatorType = typeof(DecisionNode).FullName,
                        ActivatorAssembly = typeof(DecisionNode).Assembly.FullName,
                    });

            return modelBuilder;
        }
    }
}
