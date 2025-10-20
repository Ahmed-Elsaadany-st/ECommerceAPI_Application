using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class PaginatedResult<TEntity>
    {
        public PaginatedResult(int pageSize, int pageIndex, int toltalCount, IEnumerable<TEntity> data)
        {
            PageIndex = pageIndex;
            ToltalCount = toltalCount;
            PageSize = pageSize;
            Data = data;
        }

        public int PageIndex { get; set; }
        public int ToltalCount { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<TEntity>Data {get;set;}
    }
}
