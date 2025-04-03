using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Domain.Base;

namespace Template.Domain.Repositories
{
    public interface IDeleteRepository<TEntity> where TEntity : BaseEntity<int>, new()
    {
        Task DeleteAsync(int id);
    }
}
