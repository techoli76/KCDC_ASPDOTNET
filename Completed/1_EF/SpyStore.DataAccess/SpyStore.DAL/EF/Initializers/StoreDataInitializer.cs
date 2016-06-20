using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SpyStore.DAL.Repos;

namespace SpyStore.DAL.EF.Initializers
{
    public static class StoreDataInitializer
    {
        public static void InitializeData(SpyStoreContext context)
        {
            ClearData(context);
            SeedData(context);
        }
        public static void InitializeData(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<SpyStoreContext>();
            InitializeData(context);
        }
        public static void DropAndCreateDataBase(SpyStoreContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }

        public static void ClearData(SpyStoreContext context)
        {
            ExecuteDeleteSQL(context, CategoryRepo.GetTableName(context));
            ExecuteDeleteSQL(context, CustomerRepo.GetTableName(context));
            ResetIdentity(context);
        }

        public static void ExecuteDeleteSQL(
            SpyStoreContext context, string tableName)
        {
            context.Database.ExecuteSqlCommand($"Delete from {tableName}");

        }
        public static void ResetIdentity(SpyStoreContext context)
        {
            var tables = new[] {CategoryRepo.GetTableName(context),CustomerRepo.GetTableName(context),OrderDetailRepo.GetTableName(context),OrderRepo.GetTableName(context),ProductRepo.GetTableName(context),ShoppingCartRepo.GetTableName(context)};
            foreach (var itm in tables)
            {
                ResetIdentity(context, itm);
                //context.Database.ExecuteSqlCommand($"DBCC CHECKIDENT (\"{itm}\", RESEED, 0);");
            }
        }

        public static void ResetIdentity(SpyStoreContext context, string tableName)
        {
            context.Database.ExecuteSqlCommand($"DBCC CHECKIDENT (\"{tableName}\", RESEED, 1);");
        }

        public static void SeedData(SpyStoreContext context)
        {
            try
            {
                if (!context.Categories.Any())
                {
                    context.Categories.AddRange(
                        StoreSampleData.GetCategories());
                    context.SaveChanges();
                }
                if (!context.Products.Any())
                {
                    context.Products.AddRange(
                        StoreSampleData.GetProducts(context.Categories.ToList()));
                    context.SaveChanges();
                }
                if (!context.Customers.Any())
                {
                    context.Customers.AddRange(
                        StoreSampleData.GetAllCustomerRecords(context));
                    context.SaveChanges();
                }
                var customer = context.Customers.FirstOrDefault();
                if (!context.Orders.Any())
                {
                    context.Orders.AddRange(StoreSampleData.GetOrders(customer));
                    context.SaveChanges();
                    var order = context.Orders.FirstOrDefault();
                    context.OrderDetails.AddRange(
                        StoreSampleData.GetOrderDetails(order, context));
                    context.SaveChanges();
                }
                if (!context.ShoppingCartRecords.Any())
                {
                    context.ShoppingCartRecords.AddRange(
                        StoreSampleData.GetCart(customer, context));
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

    }
}
