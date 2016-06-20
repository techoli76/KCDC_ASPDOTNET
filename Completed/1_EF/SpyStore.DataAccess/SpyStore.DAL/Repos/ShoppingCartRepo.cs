using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SpyStore.DAL.EF;
using SpyStore.DAL.Repos.Base;
using SpyStore.DAL.Repos.Interfaces;
using SpyStore.Models.Entities;
using SpyStore.Models.ViewModels;

namespace SpyStore.DAL.Repos
{
    public class ShoppingCartRepo : RepoBase<ShoppingCartRecord>, IShoppingCartRepo
    {
        public ShoppingCartRepo(DbContextOptions<SpyStoreContext> options) : base(options)
        {
            Table = Context.ShoppingCartRecords;
        }
        public ShoppingCartRepo() : base()
        {
            Table = Context.ShoppingCartRecords;
        }

        public ShoppingCartRecord Find(int customerId, int productId)
        {
            return Table.SingleOrDefault(x => x.CustomerId == customerId && x.ProductId == productId);
        }

        public override int Update(ShoppingCartRecord entity)
        {
            return entity.Quantity <= 0 ? Delete(entity) : base.Update(entity);
        }

        public override int Add(ShoppingCartRecord entity)
        {
            var item = Find(entity.CustomerId,entity.ProductId);
            if (item == null)
            {
                return base.Add(entity);
            }
            item.Quantity += entity.Quantity;
            return item.Quantity <= 0 ? Delete(item) : Update(item);
        }

        public CartRecordWithProductInfo GetShoppingCartRecord(
            int customerId, int productId)
            => Table
            .Where(x => x.CustomerId == customerId && x.ProductId == productId)
            .Include(x => x.Product)
            .ThenInclude(p => p.Category)
            .Select(x => new CartRecordWithProductInfo
            {
                Id = x.Id,
                DateCreated = x.DateCreated,
                CustomerId = customerId,
                Quantity = x.Quantity,
                ProductId = x.ProductId,
                Description = x.Product.Description,
                ModelName = x.Product.ModelName,
                ModelNumber = x.Product.ModelNumber,
                ProductImage = x.Product.ProductImage,
                ProductImageLarge = x.Product.ProductImageLarge,
                ProductImageThumb = x.Product.ProductImageThumb,
                CurrentPrice = x.Product.CurrentPrice,
                UnitsInStock = x.Product.UnitsInStock,
                CategoryName = x.Product.Category.CategoryName,
                LineItemTotal = x.Quantity * x.Product.CurrentPrice,
                TimeStamp = x.TimeStamp
            }).FirstOrDefault();

        public IEnumerable<CartRecordWithProductInfo> GetShoppingCartRecords(
            int customerId)
            => Table
            .Where(x => x.CustomerId == customerId)
            .Include(x => x.Product)
            .ThenInclude(p => p.Category)
            .Select(x => new CartRecordWithProductInfo
            {
                Id = x.Id,
                DateCreated = x.DateCreated,
                CustomerId = customerId,
                Quantity = x.Quantity,
                ProductId = x.ProductId,
                Description = x.Product.Description,
                ModelName = x.Product.ModelName,
                ModelNumber = x.Product.ModelNumber,
                ProductImage = x.Product.ProductImage,
                ProductImageLarge = x.Product.ProductImageLarge,
                ProductImageThumb = x.Product.ProductImageThumb,
                CurrentPrice = x.Product.CurrentPrice,
                UnitsInStock = x.Product.UnitsInStock,
                CategoryName = x.Product.Category.CategoryName,
                LineItemTotal = x.Quantity * x.Product.CurrentPrice,
                TimeStamp = x.TimeStamp
            })
            .OrderBy(x => x.ModelName);

        public int Purchase(int customerId)
        {
            var customerIdParam = new SqlParameter("@customerId", SqlDbType.Int)
            {
                Direction = ParameterDirection.Input,
                Value = customerId
            };
            SqlConnection connection = null;
            var orderId = 0;
            try
            {
                //currently not working. tracked here: https://github.com/aspnet/EntityFramework/issues/3115
                //Context.Database.ExecuteSqlCommand("EXEC [Store].[PurchaseItemsInCart] @customerId", customerIdParam, orderIdParam);
                connection = (SqlConnection)Context.Database.GetDbConnection();
                var command = new SqlCommand("store.PurchaseItemsInCart @customerId", connection);
                command.Parameters.Add(customerIdParam);
                connection.Open();
                orderId = (int)command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                if (connection?.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            return orderId;
        }



    }
}
