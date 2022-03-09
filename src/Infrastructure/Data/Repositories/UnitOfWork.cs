using Actions.Core.Domain.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Actions.Infrasctructure.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public readonly ActionsContext context;
        public UnitOfWork(ActionsContext context)
        {
            this.context = context;
        }
        public void Insert<TEntity>(TEntity entity) where TEntity : class
        {
            context.Entry(entity).State = EntityState.Added;
        }

        public void InsertRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            context.Set<TEntity>().AddRange(entities);
        }

        public void Update<TEntity>(TEntity entity) where TEntity : class
        {
            if (context.Entry(entity).State == EntityState.Detached)
                context.Attach(entity);

            context.Entry(entity).State = EntityState.Modified;
        }

        public void UpdateRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            if (entities.Any())
                context.Set<TEntity>().UpdateRange(entities);
        }

        public void Update<TEntity>(TEntity entity, params Expression<Func<TEntity, object>>[] properties) where TEntity : class
        {
            string name;
            foreach (var property in properties)
            {
                if (property.Body is MemberExpression)
                {
                    name = ((MemberExpression)property.Body).Member.Name;
                }
                else
                {
                    var op = ((UnaryExpression)property.Body).Operand;
                    name = ((MemberExpression)op).Member.Name;
                }
                context.Entry(entity).Property(name).IsModified = true;
            }
        }

        public void Update<TEntity>(TEntity entity, params string[] properties) where TEntity : class
        {
            foreach (var property in properties)
                context.Entry(entity).Property(property).IsModified = true;
        }

        public void Delete<TEntity>(TEntity entity) where TEntity : class
        {
            if (context.Entry(entity).State == EntityState.Detached)
                context.Attach(entity);

            context.Entry(entity).State = EntityState.Deleted;
        }

        public void DeleteRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            if (entities.Any())
                context.Set<TEntity>().RemoveRange(entities);
        }

        async public Task<int> ExecuteSQLCommand(string sql)
        {
            return await context.Database.ExecuteSqlRawAsync(sql);
        }

        async public Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }
    }
}
