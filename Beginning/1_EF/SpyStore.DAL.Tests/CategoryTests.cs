using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SpyStore.DAL.EF;
using SpyStore.Models.Entities;
using Xunit;

namespace SpyStore.DAL.Tests
{
    [Collection("Database Testing")]
    public class CategoryTests : IDisposable
    {
        private readonly SpyStoreContext _db;

        public CategoryTests()
        {
            _db = new SpyStoreContext();
            DatabaseUtilities.CleanDataBase(_db, "Store.Categories");
        }
        public void Dispose()
        {
            DatabaseUtilities.CleanDataBase(_db, "Store.Categories");
            _db.Dispose();
        }

        private Category CreateCategoryInstance(string categoryName)
        {
            return new Category { CategoryName = categoryName };
        }

        [Fact]
        public void ShouldAddACategory()
        {
            var category = CreateCategoryInstance("Foo");
            _db.Categories.Add(category);
            Assert.Equal(EntityState.Added, _db.Entry(category).State);
            Assert.True(category.Id < 0);
            _db.SaveChanges();
            Assert.Equal(EntityState.Unchanged, _db.Entry(category).State);
            Assert.Equal(2, category.Id);
            Assert.Equal(1, _db.Categories.Count());
        }

        [Fact]
        public void ShouldGetAllCategories()
        {
            _db.Categories.Add(CreateCategoryInstance("Foo"));
            _db.Categories.Add(CreateCategoryInstance("Bar"));
            _db.SaveChanges();
            var categories = _db.Categories.ToList();
            Assert.Equal(2, _db.Categories.Count());
            Assert.Equal("Foo", categories[0].CategoryName);
            Assert.Equal("Bar", categories[1].CategoryName);
        }

        [Fact]
        public void ShouldGetFirstCategory()
        {
            _db.Categories.Add(CreateCategoryInstance("Foo"));
            _db.Categories.Add(CreateCategoryInstance("Bar"));
            _db.SaveChanges();
            var cat = _db.Categories.First();
            Assert.Equal("Foo", cat.CategoryName);
            Assert.Equal(2, cat.Id);
        }

        [Fact]
        public void ShouldUpdateACategory()
        {
            _db.Categories.Add(CreateCategoryInstance("Foo"));
            _db.SaveChanges();
            var category = _db.Categories.First();
            category.CategoryName = "Bar";
            _db.Categories.Update(category);
            Assert.Equal(EntityState.Modified, _db.Entry(category).State);
            _db.SaveChanges();
            Assert.Equal(EntityState.Unchanged, _db.Entry(category).State);
            Assert.Equal("Bar", _db.Categories.First().CategoryName);
        }

        [Fact]
        public void ShouldNotUpdateANonAttachedCategory()
        {
            var category = CreateCategoryInstance("Foo");
            _db.Categories.Add(category);
            category.CategoryName = "Bar";
            Assert.Throws<InvalidOperationException>(() => _db.Categories.Update(category));
        }

        [Fact]
        public void ShouldDeleteACategory()
        {
            _db.Categories.Add(CreateCategoryInstance("Foo"));
            _db.Categories.Add(CreateCategoryInstance("Bar"));
            _db.SaveChanges();
            Assert.Equal(2, _db.Categories.Count());
            var category = _db.Categories.First();
            _db.Categories.Remove(category);
            Assert.Equal(EntityState.Deleted, _db.Entry(category).State);
            _db.SaveChanges();
            Assert.Equal(EntityState.Detached, _db.Entry(category).State);
            Assert.Equal(1, _db.Categories.Count());
        }
        [Fact]
        public void ShouldNotDeleteACategoryWithoutTimestampData()
        {
            var category = CreateCategoryInstance("Foo");
            _db.Categories.Add(category);
            _db.SaveChanges();
            var db2 = new SpyStoreContext();
            var catToDelete = new Category { Id = category.Id};
            db2.Categories.Remove(catToDelete);
            var ex = Assert.Throws<DbUpdateConcurrencyException>(() =>db2.SaveChanges());
            Assert.Equal(1,ex.Entries.Count);
            Assert.Equal(2,((Category)ex.Entries[0].Entity).Id);
        }
        [Fact]
        public void ShouldDeleteACategoryWithTimestampData()
        {
            var category = CreateCategoryInstance("Foo");
            _db.Categories.Add(category);
            _db.SaveChanges();
            var db2 = new SpyStoreContext();
            var catToDelete = new Category { Id = category.Id, TimeStamp = category.TimeStamp};
            db2.Entry(catToDelete).State = EntityState.Deleted;
            var affected = db2.SaveChanges();
            Assert.Equal(1,affected);
        }

    }
}
