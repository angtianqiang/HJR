using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ZtxFrameWork.UI.Comm.Utils;

namespace ZtxFrameWork.UI.Comm
{
    public static class  DbSetExtensions
    {
  

        /// <summary>
        /// Returns IQuerable representing sequence of entities from repository filtered by the given predicate and projected to the specified projection entity type by the given LINQ function.
        /// 返回IQuerable代表序列的实体从存储库由给定谓词过滤,将指定的实体类型的投影LINQ功能
        /// </summary>
        /// <typeparam name="TEntity">A repository entity type.</typeparam>
        /// <typeparam name="TProjection">A projection entity type.</typeparam>
        /// <param name="repository">A repository.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="projection">A LINQ function used to transform entities from repository entity type to projection entity type.</param>
        public static IQueryable<TProjection> GetFilteredEntities<TEntity, TProjection>(this IQueryable<TEntity> repository, Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IQueryable<TProjection>> projection) where TEntity : class
        {
            var filtered = predicate != null ? repository.Where(predicate) : repository;
            return projection != null ? projection(filtered) : (IQueryable<TProjection>)filtered;
        }

        /// <summary>
        /// Returns IQuerable representing sequence of entities from repository filtered by the given predicate.
        /// 返回IQuerable代表序列的实体从存储库由给定谓词过滤
        /// </summary>
        /// <typeparam name="TEntity">A repository entity type.</typeparam>
        /// <param name="repository">A repository.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        public static IQueryable<TEntity> GetFilteredEntities<TEntity>(this IQueryable<TEntity> repository, Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return repository.GetFilteredEntities(predicate, x => x);
        }
    }


  
}
