using System.Linq.Expressions;
using Template.Domain.Base;
using Template.Domain.Utilities;

namespace Template.Domain.Repositories
{
    public interface IRepository<TEntity> where TEntity : BaseEntity<int>, new()
    {


        Task<IPagedList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? expression = null, IEnumerable<(Expression<Func<TEntity, object>> NavigationProperty, string[] ChildProperties)>? includes = null, int page = 1, int pageSize = 5);

        Task<bool> GetAnyAsync(Expression<Func<TEntity, bool>> expression);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression, IEnumerable<(Expression<Func<TEntity, object>> NavigationProperty, string[] ChildProperties)>? includes = null);
        Task<TEntity> CreateAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task DeleteAsync(int id);

    }
}
