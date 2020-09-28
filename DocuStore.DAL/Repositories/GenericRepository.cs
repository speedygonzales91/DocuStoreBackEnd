using DocuStore.DAL.Interfaces.ModelExtensions;
using DocuStore.DAL.Interfaces.Repositories;
using DocuStore.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DocuStore.DAL.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        internal DocuStoreContext context;
        internal DbSet<TEntity> dbSet;

        public GenericRepository(DocuStoreContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public virtual List<TEntity> GetAll()
        {
            IQueryable<TEntity> query = dbSet;
            return query.ToList();
        }

        public virtual TEntity GetByID(long id)
        {
            return dbSet.Find(id);
        }

        public virtual TEntity GetByID(int id)
        {
            return dbSet.Find(id);
        }

        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public virtual void InsertRange(List<TEntity> entities)
        {
            dbSet.AddRange(entities);
        }

        public virtual void Delete(long id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            dbSet.Remove(entityToDelete);
        }

        public virtual void DeleteRange(List<TEntity> entitiesToDelete)
        {
            dbSet.RemoveRange(entitiesToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public virtual void Save(int userId)
        {
            FillAuditProperties(userId);
            context.SaveChanges();
        }

        public virtual void Save()
        {
            context.SaveChanges();
        }


        /// <summary>
        /// Audit mezők generikus kitöltése
        /// </summary>
        /// <param name="userId"></param>
        private void FillAuditProperties(int userId)
        {
            ///Új entitások lekérdezése
            IEnumerable<EntityEntry> addedEntities = context.ChangeTracker.Entries().Where(e => e.State == EntityState.Added);

            foreach (var insertedItem in addedEntities)
            {
                if (insertedItem.Entity is ITrackCreate createdEntity)
                {
                    createdEntity.CreatedBy = userId;
                    createdEntity.CreatedAt = DateTime.Now;
                }

                if (insertedItem.Entity is ITrackUpdate updatedEntity)
                {
                    updatedEntity.ModifiedBy = userId;
                    updatedEntity.ModifiedAt = DateTime.Now;
                }
            }


            ///Módosított entitások lekérdezése
            IEnumerable<EntityEntry> modifiedEntities = context.ChangeTracker.Entries().Where(e => e.State == EntityState.Modified);

            foreach (var insertedItem in modifiedEntities)
            {
                if (insertedItem.Entity is ITrackUpdate updatedEntity)
                {
                    updatedEntity.ModifiedBy = userId;
                    updatedEntity.ModifiedAt = DateTime.Now;
                }
            }


            ///Törölt entitások lekérdezése
            IEnumerable<EntityEntry> deletedEntities = context.ChangeTracker.Entries().Where(e => e.State == EntityState.Deleted);

            foreach (var insertedItem in deletedEntities)
            {
                if (insertedItem.Entity is ITrackDelete deleteEntity)
                {
                    deleteEntity.DeletedBy = userId;
                    deleteEntity.DeletedAt = DateTime.Now;
                    deleteEntity.IsDeleted = true;
                    insertedItem.State = EntityState.Modified;
                }
            }
        }
    }
}
