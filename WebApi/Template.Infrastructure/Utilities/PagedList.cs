using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Domain.Utilities;

namespace Template.Infrastructure.Utilities
{
    public class PagedList<T> : IPagedList<T>
    {
        public PagedList(List<T> items, int page, int pageSize, int totalCount, bool hasNextPage, bool hasPreviousPage, int pageCount)
        {
            this.Items = items;
            this.Page = page;
            this.PageSize = pageSize;
            this.TotalItemCount = totalCount;
            this.HasNextPage = hasNextPage;
            this.HasPreviousPage = hasPreviousPage;
            this.PageCount = pageCount;
        }

        public List<T> Items { get; }

        public int Page { get; }

        public int PageSize { get; }

        public int TotalItemCount { get; }

        public bool HasNextPage { get; set; }

        public bool HasPreviousPage { get; set; }

        public int PageCount { get; set; }

        public static async Task<IPagedList<T>> Create(IQueryable<T> query, int page, int pageSize)
        {
            int totalCount = await query.CountAsync();
            var items = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var hasNextPage = page * pageSize < totalCount;
            var hasPreviousPage = page > 1;
            var pageCount = totalCount / pageSize + 1;

            return new PagedList<T>(items, page, pageSize, totalCount, hasNextPage, hasPreviousPage, pageCount);
        }
    }
}
