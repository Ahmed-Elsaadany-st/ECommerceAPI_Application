using Domain.Models;
using Shared;

namespace Servieces.Specifications
{ //تتم البصمجة والله
    internal class ProductCountSpecifications(ProcductQueryParams queryParams) : BaseSpecifications<Product,int>
        (P =>
            (!queryParams.BrandId.HasValue || P.BrandId == queryParams.BrandId)
            && (!queryParams.TypeId.HasValue || P.TypeId == queryParams.TypeId)
            && (string.IsNullOrWhiteSpace(queryParams.SearchValue) || P.Name.Contains(queryParams.SearchValue.ToLower())))
    {




    }
}