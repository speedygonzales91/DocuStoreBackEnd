using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DocuStore.DAL.Interfaces.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        void Delete(long id);
        void Delete(TEntity entityToDelete);
        void DeleteRange(List<TEntity> entitiesToDelete);
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
        List<TEntity> GetAll();
        TEntity GetByID(long id);
        TEntity GetByID(int id);
        void InsertRange(List<TEntity> entities);
        void Insert(TEntity entity);
        void Update(TEntity entityToUpdate);
        void Save(int userId);
        void Save();
    }
}
