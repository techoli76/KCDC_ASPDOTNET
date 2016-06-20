using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SpyStore.DAL.EF.Migrations
{
    public partial class Sproc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
        "CREATE PROCEDURE [Store].[PurchaseItemsInCart](@customerId INT = 0) AS BEGIN " +
        " SET NOCOUNT ON; " +
        " DECLARE @orderId INT;" +
        " INSERT INTO Store.Orders (CustomerId, OrderDate, ShipDate) " +
        "    VALUES(@customerId, GETDATE(), GETDATE()); " +
        " SET @orderId = SCOPE_IDENTITY(); " +
        " DECLARE @TranName VARCHAR(20);SELECT @TranName = 'CommitOrder'; " +
        "   BEGIN TRANSACTION @TranName; " +
        "   BEGIN TRY " +
        "       INSERT INTO Store.OrderDetails (OrderId, ProductId, Quantity, UnitCost) " +
        "       SELECT @orderId, ProductId, Quantity, p.CurrentPrice " +
        "       FROM Store.ShoppingCartRecords scr " +
        "          INNER JOIN Store.Products p ON p.Id = scr.ProductId " +
        "       WHERE CustomerId = @customerId; " +
        "       DELETE FROM Store.ShoppingCartRecords WHERE CustomerId = @customerId; " +
        "       COMMIT TRANSACTION @TranName; " +
        "       SELECT @orderId; " +
        "   END TRY " +
        "   BEGIN CATCH " +
        "       ROLLBACK TRANSACTION @TranName; " +
        "       SELECT -1; " +
        "   END CATCH; " +
        "END;");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE [Store].[PurchaseItemsInCart]");
        }
    }
}
