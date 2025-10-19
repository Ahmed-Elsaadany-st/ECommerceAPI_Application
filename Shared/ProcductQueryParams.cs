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
        public int? TypeId { get; set; }
        public int? BrandId { get; set; }
        public ProductSortingOptions sortingOptions { get; set; }
        public string? SearchValue {get; set; }

    }
}
