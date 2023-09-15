using Application;
using Domain.Entities;
using Domain.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Data
{

    public class StorageBus
    {
        public static void Add()
        {
            using(var db = new WeightContext())
            {
                try
                {
                    foreach (var product in StartData.Products)
                    {
                        var query = db.Products.Where(q => q.IsDeleted == false && EF.Functions.Like(q.Name, product.Name))
                            .Where(q => q.CreatorId == null);

                        if(!string.IsNullOrEmpty(product.Manufacturer))
                        {
                            query = query.Where(q => EF.Functions.Like(q.Manufacturer, product.Manufacturer));
                        }

                        var exist = query.Any();

                        if (!exist)
                        {
                            db.Products.Add(product);
                            db.SaveChanges(null);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
        public static void Initialize(ILoggerFactory loggerFactory)
        {
            loggerFactory.CreateLogger<StorageBus>().LogWarning("Storage Bus Logging");

            var recreate = bool.Parse(Environment.GetEnvironmentVariable("ASPNETCORE_DB_RECREATE"));

            if (recreate)
            {
                StorageBus.ReCreateDatabaseIfNotPresent();
                StorageBus.MigrateDatabase();
            }
            else
            {
                StorageBus.MigrateDatabase();
            }

            Add();
        }

        public static void CreateDatabaseIfNotPresent()
        {
            using (var client = new WeightContext())
            {
                client.Database.EnsureCreated();
            }
        }
        public static void ReCreateDatabaseIfNotPresent()
        {
            using (var client = new WeightContext())
            {
                client.Database.EnsureDeleted();
            }
        }
        public static void MigrateDatabase()
        {
            using (var client = new WeightContext())
            {
                try
                {
                    client.Database.Migrate();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
    }
}
