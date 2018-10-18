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
                        ActivatorType = typeof(HttpGetNode).FullName,
                        ActivatorAssembly = typeof(HttpGetNode).Assembly.FullName,
                    });

            return modelBuilder;
        }
    }
}
