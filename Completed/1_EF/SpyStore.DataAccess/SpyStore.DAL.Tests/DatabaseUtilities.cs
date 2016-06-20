using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpyStore.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace SpyStore.DAL.Tests
{
    public static class DatabaseUtilities
    {
        public static void CleanDataBase(SpyStoreContext context)
        {
            
        }

        public static void CleanDataBase(SpyStoreContext context, string tableName)
        {
            context.Database.ExecuteSqlCommand($"Delete from {tableName}");
            context.Database.ExecuteSqlCommand($"DBCC CHECKIDENT (\"{tableName}\", RESEED, 1);");

        }
    }
}
