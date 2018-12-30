using Core.DBContext;
using Core.Entities;
using DALRepository.IRepositories;
using Microsoft.EntityFrameworkCore;
using Models.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DALRepository.Repositories
{
    public abstract class BaseRepository<TEntity, TKey> :
    IBaseRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    where TKey : IEquatable<TKey>
    {
        private readonly LifebookDbContext _context;
        protected DbSet<TEntity> _Entity;

        protected BaseRepository(LifebookDbContext context)
        {
            this._context = context;
            this._Entity = context.Set<TEntity>();
        }

        protected IQueryable<TEntity> Get(TKey id)
        {
            if (id.Equals(null))
            {
                throw new ArgumentNullException("key");
            }
            return this._Entity.Where(x => x.Id.Equals(id));
        }

        protected IQueryable<TEntity> GetByIds(List<TKey> ids)
        {
            return this._Entity?.Where(x => ids.Contains(x.Id));
        }

        protected IQueryable<TEntity> GetAll()
        {
            return this._Entity;
        }

        public void Insert(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entity.CreatedOn = DateTime.UtcNow;
            //entity.CreatedBy = Utilities.GetUserId();
            this._context.Entry(entity).State = EntityState.Added;
        }

        public void InsertMultiple(List<TEntity> entities)
        {
            for (int i = 0; i < entities.Count; i++)
            {
                Insert(entities[i]);
            }
        }

        public void Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entity.LastModifiedOn = DateTime.UtcNow;
            //entity.LastModifiedBy = Utilities.GetUserId();
            this._context.Entry(entity).State = EntityState.Modified;
        }

        public void UpdateMultiple(List<TEntity> entities)
        {
            for (int i = 0; i < entities.Count; i++)
            {
                Update(entities[i]);
            }
        }

        public void Delete(TEntity entity, bool isHardDelete = false)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            if (isHardDelete)
            {
                this._context.Entry(entity).State = EntityState.Deleted;
            }
            else
            {
                entity.IsDeleted = true;
                Update(entity);
            }
        }

        public void DeleteMultiple(List<TEntity> entities, bool isHardDelete = false)
        {
            for (int i = 0; i < entities.Count; i++)
            {
                Delete(entities[i], isHardDelete);
            }
        }

        public async Task Delete(TKey id, bool isHardDelete = false)
        {
            if (id.Equals(null))
            {
                throw new ArgumentNullException("key");
            }
            var entity = await Get(id).FirstOrDefaultAsync();
            if (isHardDelete)
            {
                this._context.Entry(entity).State = EntityState.Deleted;
            }
            else
            {
                entity.IsDeleted = true;
                Update(entity);
            }
        }

        public async Task DeleteMultiple(List<TKey> ids, bool isHardDelete = false)
        {
            var entities = await GetByIds(ids).ToListAsync();

            for (int i = 0; i < entities.Count; i++)
            {
                Delete(entities[i], isHardDelete);
            }
        }

        protected IQueryable<TEntity> GetFilteredData(Expression<Func<TEntity, bool>> exp)
        {
            return this._Entity.Where(exp);
        }

        protected IQueryable<TEntity> _DefaultQuery
        {
            get
            {
                return this._Entity.AsQueryable();
            }
        }

    }
}
