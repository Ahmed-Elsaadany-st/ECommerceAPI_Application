using Domain.Models;
using Shared;
using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servieces.Specifications
{
    internal class ProductWithBrandAndTypeSpecifications:BaseSpecifications<Product,int>
    {
        //Get All Products With Brands And types
        public ProductWithBrandAndTypeSpecifications(ProcductQueryParams queryParams) :
            base (P=>
            (!queryParams.BrandId.HasValue|| P.BrandId==queryParams.BrandId)
            &&(!queryParams.TypeId.HasValue|| P.TypeId==queryParams.TypeId)
            && (string.IsNullOrWhiteSpace(queryParams.SearchValue)||P.Name.Contains(queryParams.SearchValue.ToLower())))
            
        {
            AddInclude(p=>p.ProductBrand);
            AddInclude(p=>p.ProductType);
            #region Sorting
            switch (queryParams.sortingOptions)
            {
                case ProductSortingOptions.NameAsc: AddOrderBy(p=>p.Name); break;

                case ProductSortingOptions.NameDesc: AddOrderByDesceding(p=>p.Name); break;
                
                case ProductSortingOptions.PriceAsc: AddOrderBy(p=>p.Price); break;

                case ProductSortingOptions.PriceDesc: AddOrderByDesceding(p=>p.Price); break;

                default:
                    break;
            }

            #endregion
        }
        //Get Product By Id
        public ProductWithBrandAndTypeSpecifications(int id):base(p=>p.Id==id)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }
    }
}
