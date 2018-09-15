using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Newtonsoft.Json;
using Asdf.Domain.Actions;
using Asdf.Domain.Templates;
using Asdf.Domain.Flows;
using Asdf.Domain.Triggers;
using Asdf.Domain.Devices;

namespace Asdf.Playground
{
    public class AsdfContext : DbContext
    {
        //Actions
        public DbSet<Node> Nodes { get; set; }
        public DbSet<DecisionNode> Decisions { get; set; }
        public DbSet<HttpGetNode> HttpGets { get; set; }
        public DbSet<HttpPostNode> HttpPosts { get; set; }
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

        //Device
        public DbSet<Device> Devices { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlite("Data Source=d:\\asdf.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<AttributeNode>(b =>
            {
                b.Property(e => e.Attributes)
                    .HasConversion(
                        d => JsonConvert.SerializeObject(d, Formatting.None),
                        s => JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(s));
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
        }
    }
}
