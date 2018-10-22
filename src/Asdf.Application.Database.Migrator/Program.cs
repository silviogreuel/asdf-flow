using System;
using System.Linq;
using Asdf.Application.Database.Seeds;
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
