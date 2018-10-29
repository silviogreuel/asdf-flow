using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Asdf.Application.Database.Seeds;
using Newtonsoft.Json;
using Asdf.Domain.Actions;
using Asdf.Domain.Templates;
using Asdf.Domain.Flows;
using Asdf.Domain.Triggers;
using Asdf.Domain.Devices;
using Asdf.Domain.Users;

namespace Asdf.Application.Database
{
    public class AsdfContext : DbContext
    {
        //Actions
        public DbSet<Node> Nodes { get; set; }
        public DbSet<DecisionNode> Decisions { get; set; }
        public DbSet<HttpGetNode> HttpGets { get; set; }
        public DbSet<HttpPostNode> HttpPosts { get; set; }
        public DbSet<MqttPublishNode> MqttPublish { get; set; }
        public DbSet<TemplateNode> TemplateNode { get; set; }
        public DbSet<AttributeNode> Attributes { get; set; }
        public DbSet<LogNode> Logs { get; set; }

        //Templates
        public DbSet<NodeTemplate> NodeTemplates { get; set; }
        public DbSet<FieldTemplate> FieldTemplates { get; set; }

        //Flows
        public DbSet<Flow> Flows { get; set; }

        //Triggers
        public DbSet<Trigger> Triggers { get; set; }
        public DbSet<ButtonTrigger> Buttons { get; set; }
        public DbSet<MqttTrigger> Mqtts { get; set; }

        //Devices
        public DbSet<Device> Devices { get; set; }

        //Users
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies()
                .UseNpgsql("Host=localhost;Database=iot;Username=iot;Password=iot", o => o.MigrationsAssembly("Asdf.Application.Database"))
                .EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>(b =>
            {
                b.Property(p => p.Id)
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<Trigger>(b =>
            {
                b.Property(e => e.Context)
                    .HasConversion(
                        d => JsonConvert.SerializeObject(d, Formatting.None),
                        s => JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(s));
            });

            modelBuilder.Entity<Node>(b =>
            {
                b.HasOne(p => p.Pass).WithOne().IsRequired(false);
                b.HasOne(p => p.Fail).WithOne().IsRequired(false);
            });

            modelBuilder
                .SeedNodeTemplates()
                .SeedFieldTemplates();
        }
    }
}
