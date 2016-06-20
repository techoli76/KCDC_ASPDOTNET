using System;
using System.Linq;
using SpyStore.DAL.EF;
using SpyStore.DAL.EF.Initializers;
using Xunit;

namespace SpyStore.DAL.Tests
{
    [Collection("Database Testing")]
    public class InitializerTests : IDisposable
    {
        private readonly SpyStoreContext _db;

        public InitializerTests()
        {
            _db = new SpyStoreContext();
            StoreDataInitializer.InitializeData(_db);
        }
        public void Dispose()
        {
            StoreDataInitializer.ClearData(_db);
            _db.Dispose();
        }


        [Fact]
        public void ShouldGetAllCategories()
        {
            Assert.Equal(7, _db.Categories.Count());
        }

        [Fact]
        public void ShouldGetFirstCategory()
        {
            var cat = _db.Categories.First();
            Assert.Equal("Communications", cat.CategoryName);
        }

    }
}
