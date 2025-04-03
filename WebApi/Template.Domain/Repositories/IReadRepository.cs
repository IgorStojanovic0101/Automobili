using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Template.Domain.Base;
using Template.Domain.Utilities;

namespace Template.Domain.Repositories
{

    public interface IReadRepository<TEntity> where TEntity : BaseEntity<int>, new()
    {
        Task<IPagedList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? expression = null, IEnumerable<(Expression<Func<TEntity, object>> NavigationProperty, string[] ChildProperties)>? includes = null, int page = 1, int pageSize = 5);

        Task<bool> GetAnyAsync(Expression<Func<TEntity, bool>> expression);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression, IEnumerable<(Expression<Func<TEntity, object>> NavigationProperty, string[] ChildProperties)>? includes = null);
    }

}
