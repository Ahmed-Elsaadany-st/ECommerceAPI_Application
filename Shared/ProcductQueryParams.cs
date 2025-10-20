using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class ProcductQueryParams
    {
        private const int DefaultPageSize = 5;
        private const int MaxPageSize = 10;
        public int? TypeId { get; set; }
        public int? BrandId { get; set; }
        public ProductSortingOptions sortingOptions { get; set; }
        public string? SearchValue {get; set; }

        public int PageIndex { get; set; } = 1;

        private int pagesize=DefaultPageSize;

        public int PageSize
        {
            get { return pagesize; }
            set { pagesize=value>MaxPageSize?MaxPageSize:value; }
        }


    }
}
