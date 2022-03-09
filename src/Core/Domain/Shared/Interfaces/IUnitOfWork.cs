using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Actions.Core.Domain.Shared.Interfaces
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();

        void Insert<TEntity>(TEntity entity) where TEntity : class;

        void InsertRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;

        void Update<TEntity>(TEntity entity) where TEntity : class;

        void Update<TEntity>(TEntity entity, params Expression<Func<TEntity, object>>[] properties) where TEntity : class;

        void Update<TEntity>(TEntity entity, params string[] properties) where TEntity : class;

        void UpdateRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;

        void Delete<TEntity>(TEntity entity) where TEntity : class;

        void DeleteRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;

        Task<int> ExecuteSQLCommand(string sql);
    }
}
