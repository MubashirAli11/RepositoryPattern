using Models.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DALRepository.IRepositories
{
    public interface IBaseRepository<TEntity ,TKey>
    {
        void Insert(TEntity entity);
        void InsertMultiple(List<TEntity> entities);
        void Update(TEntity entity);
        void UpdateMultiple(List<TEntity> entities);
        /// <summary>
        /// use when entity is given 
        /// by default soft delete
        /// /// recomended to use for delete operation
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="isHardDelete"></param>
        void Delete(TEntity entity, bool isHardDelete = false);
        /// <summary>
        ///  use when id is provided
        ///  by default soft delete
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isHardDelete"></param>
        /// <returns></returns>
        Task Delete(TKey id, bool isHardDelete = false);
        void DeleteMultiple(List<TEntity> entities, bool isHardDelete = false);
        Task DeleteMultiple(List<TKey> ids, bool isHardDelete = false);
    }
}
