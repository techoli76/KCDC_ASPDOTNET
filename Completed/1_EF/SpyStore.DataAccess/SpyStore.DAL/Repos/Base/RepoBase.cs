using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using SpyStore.DAL.EF;
using SpyStore.Models.Entities.Base;

namespace SpyStore.DAL.Repos.Base
{
    public abstract class RepoBase<T> :
        IDisposable, IRepo<T> where T : EntityBase, new()
    {
        protected readonly SpyStoreContext Db;
        protected DbSet<T> Table;
        public SpyStoreContext Context => Db;
        protected RepoBase()
        {
            Db = new SpyStoreContext();
        }
        protected RepoBase(DbContextOptions<SpyStoreContext> options)
        {
            Db = new SpyStoreContext(options);
        }

        public int GetCount() => Table.Count();

        public T Find(int? id)
        {
            var entity =
                Context.ChangeTracker.Entries<T>()
                    .Select((EntityEntry e) => (T) e.Entity)
                    .FirstOrDefault(x => x.Id == id);
            return entity ?? GetOne(id);
        }

        public T GetOne(int? id) => Table.SingleOrDefault(x => x.Id == id);
        public T GetFirst() => Table.FirstOrDefault();
        public virtual IEnumerable<T> GetAll()
        {
            return Table;
        }
        public virtual int Add(T entity)
        {
            Table.Add(entity);
            return SaveChanges();
        }
        public virtual int AddRange(IEnumerable<T> entities)
        {
            Table.AddRange(entities);
            return SaveChanges();
        }
        public virtual int Update(T entity)
        {
            //Context.Entry(entity).State = EntityState.Modified;
            Table.Update(entity);
            return SaveChanges();
        }
        public int UpdateRange(IEnumerable<T> entities)
        {
            Table.UpdateRange(entities);
            return SaveChanges();
        }
        public int Delete(int id, byte[] timeStamp)
        {
            Context.Entry(new T() { Id = id, TimeStamp = timeStamp })
                .State = EntityState.Deleted;
            return SaveChanges();
        }
        public int Delete(T entity)
        {
            Context.Entry(entity).State = EntityState.Deleted;
            return SaveChanges();
        }

        internal int SaveChanges()
        {
            try
            {
                return Context.SaveChanges();
            }
            //production app should catch specific exceptions 
            //and handle them appropriately
            catch (DbUpdateConcurrencyException ex)
            {
                //Handle intelligently
                Console.WriteLine(ex);
                throw;
            }
            catch (Exception ex)
            {
                //Handle intelligently
                Console.WriteLine(ex);
                throw;
            }
        }

        public static string GetTableName(SpyStoreContext context)
        {
            var metaData = context.Model.FindEntityType(typeof(T).FullName).SqlServer();
            return $"{metaData.Schema}.{metaData.TableName}";
        }
        public string GetTableName()
        {
            var metaData = Context.Model.FindEntityType(typeof(T).FullName).SqlServer();
            return $"{metaData.Schema}.{metaData.TableName}";
        }

        bool _disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here. 
                //
            }
            Context.Dispose();
            _disposed = true;
        }


    }
}
