using System;
using System.Collections.Generic;
using System.Linq;
using Asdf.Application.Database.Seeds;
using Asdf.Domain.Actions;
using Asdf.Domain.Templates;
using Microsoft.EntityFrameworkCore;

namespace Asdf.Application.Database.Migrator
{
    public class MigrationContext : AsdfContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies()
                .UseNpgsql("Host=localhost;Database=iot;Username=admin;Password=admin", o => o.MigrationsAssembly("Asdf.Application.Database"))
                .EnableSensitiveDataLogging();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
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
                        Name = "MQTT PUBLISH",
                        ActivatorType = typeof(HttpGetNode).FullName,
                        ActivatorAssembly = typeof(HttpGetNode).Assembly.FullName,
                        Fields = new List<FieldTemplate>
                        {
                            new FieldTemplate() { Name = "Name", Type = typeof(String).FullName },
                            new FieldTemplate() { Name = "Device", Type = typeof(Guid).FullName },
                            new FieldTemplate() { Name = "Field", Type = typeof(String).FullName },
                        }
                    }
                };

                db.AddRange(templates);
                db.SaveChanges();
            }
        }
    }
}
