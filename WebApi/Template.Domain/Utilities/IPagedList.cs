using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Domain.Utilities
{
    public interface IPagedList<T>
    {
        List<T> Items { get; }
        int Page { get; }
        int PageSize { get; }
        int TotalItemCount { get; }
        bool HasNextPage { get; set; }
        bool HasPreviousPage { get; set; }
        int PageCount { get; set; }

    }
}
