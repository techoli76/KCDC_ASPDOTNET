using System.Collections.Generic;
using SpyStore.Models.Entities.Base;

namespace SpyStore.DAL.Repos.Base
{
    public interface IRepo<T> where T : EntityBase
    {
        int GetCount();
        T Find(int? id);
        T GetOne(int? id);
        T GetFirst();
        IEnumerable<T> GetAll();
        int Add(T entity);
        int AddRange(IEnumerable<T> entities);
        int Update(T entity);
        int UpdateRange(IEnumerable<T> entities);
        int Delete(int id, byte[] timeStamp);
        int Delete(T entity);
        string GetTableName();
    }
}
