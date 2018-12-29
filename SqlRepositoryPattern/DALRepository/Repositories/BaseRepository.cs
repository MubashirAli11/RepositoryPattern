using Core.DBContext;
using Core.Entities;
using DALRepository.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALRepository.Repositories
{
    public abstract class BaseRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey> where TEntity : BaseEntity<TKey> where TKey : IEquatable<TKey>
    {
        private readonly LifebookDbContext _context;
        protected DbSet<TEntity> _Entity;

        protected BaseRepository(LifebookDbContext context)
        {
            this._context = context;
            this._Entity = context.Set<TEntity>();
        }

        public async Task<TEntity> Get(TKey id)
        {
            if (id.Equals(null))
            {
                throw new ArgumentNullException("key");
            }
            return await this._Entity.FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public async Task<List<TEntity>> GetByIds(List<TKey> ids)
        {
            return await this._Entity?.Where(x => ids.Contains(x.Id))?.ToListAsync();
        }

        public async Task<List<TEntity>> GetAll()
        {
            return await this._Entity.ToListAsync();
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
            var entity = await Get(id);
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
            var entities = await GetByIds(ids);

            for (int i = 0; i < entities.Count; i++)
            {
                Delete(entities[i], isHardDelete);
            }
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
