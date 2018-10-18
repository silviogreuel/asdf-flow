using System;
using System.Linq;
using Asdf.Application.Database.Seeds;
using Microsoft.EntityFrameworkCore;

namespace Asdf.Application.Database.Migrator
{
    public class MigrationContext : AsdfContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
                /*
            modelBuilder
                .SeedNodeTemplates()
                .SeedFieldTemplates();
                */
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new MigrationContext())
            {
                foreach (var migration in db.Database.GetPendingMigrations())
                {
                    Console.WriteLine(migration);
                }

                db.Database.Migrate();

                foreach (var migration in db.Database.GetAppliedMigrations())
                {
                    Console.WriteLine(migration);
                }

                db.SaveChanges();
            }
        }
    }
}
