using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoFun.GenericRepository.Toolbox.Pagination
{
    public class PaginatedList<T> 
        where T: class
    {
        public PaginatedList(List<T> items, long totalItems, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            TotalItems = totalItems;
            Items = new List<T>(pageSize);
            Items.AddRange(items);
        }

        // This is for serialization.
        private PaginatedList()
        {
        }

        public int PageIndex { get; }

        public int PageSize { get; }

        public int TotalPages { get; }

        public long TotalItems { get; }

        public List<T> Items { get; }
    }
}
