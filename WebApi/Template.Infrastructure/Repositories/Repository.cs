using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Linq.Expressions;
using System.Reflection;
using Template.Domain.Base;
using Template.Domain.Repositories;
using Template.Domain.Utilities;
using Template.Infrastructure.EF;
using Template.Infrastructure.Utilities;

namespace Template.Infrastructure.Repositories
{
    public class Repository<TEntity> where TEntity : BaseEntity<int>, new()
    {
        private DbSet<TEntity> _dbSet;

        protected Repository(ApplicationDbContext1 db)
        {
            _dbSet = db.Set<TEntity>();

        }

        public async Task<bool> GetAnyAsync(Expression<Func<TEntity, bool>> expression) => await _dbSet.AnyAsync(expression);



        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression, IEnumerable<(Expression<Func<TEntity, object>> NavigationProperty, string[] ChildProperties)>? includes = null)
        {
            var query = _dbSet.Where(expression).AsQueryable();

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include.NavigationProperty);

                    foreach (var childProperty in include.ChildProperties)
                    {
                        var memberExpression = (MemberExpression)include.NavigationProperty.Body;
                        var navigationPropertyName = memberExpression.Member.Name;

                        query = query.Include($"{navigationPropertyName}.{childProperty}");
                    }
                }
            }
            return await query.FirstOrDefaultAsync();

        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);

            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);

            _dbSet.Remove(entity);

        }



        public async Task<IPagedList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? expression = null, IEnumerable<(Expression<Func<TEntity, object>> NavigationProperty, string[] ChildProperties)>? includes = null, int page = 1, int pageSize = 5)
        {
            var query = expression != null ? _dbSet.Where(expression).AsQueryable() : _dbSet.AsQueryable();

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include.NavigationProperty);

                    foreach (var childProperty in include.ChildProperties)
                    {
                        var memberExpression = (MemberExpression)include.NavigationProperty.Body;
                        var navigationPropertyName = memberExpression.Member.Name;

                        query = query.Include($"{navigationPropertyName}.{childProperty}");
                    }
                }
            }

            var pagedList = await PagedList<TEntity>.Create(query, page, pageSize);

            return pagedList;


        }
    }
}
