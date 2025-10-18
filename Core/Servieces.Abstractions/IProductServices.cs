using Shared;
using Shared.DTOs;
using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servieces.Abstractions
{
    public interface IProductServices
    {
        //Get All Products
        Task<IEnumerable<ProductDto>> GetAllProductsAsync(ProcductQueryParams queryParams);
        //Get Product By Id
        Task<ProductDto?> GetProductByIdAsync(int id);
        // Get All Types
        Task<IEnumerable<TypeDto>> GetAllTypesAsync();
        //Get All Brands
        Task<IEnumerable<BrandDto>> GetAllBrandsAsync();
    }
}
