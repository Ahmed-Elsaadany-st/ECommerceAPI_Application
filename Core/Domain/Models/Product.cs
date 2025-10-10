using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Product:BaseEntity<int>
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string PictureUrl { get; set; } = null!;
        public decimal Price { get; set; }
        #region Relation With ProductBrand
        public int BrandId { get; set; }
        public ProductBrand ProductBrand { get; set; } = null!;
        #endregion
        #region Relation With Product Type
        public int TypeId { get; set; }
        public ProductType ProductType { get; set;} = null!;
        #endregion


    }
}
