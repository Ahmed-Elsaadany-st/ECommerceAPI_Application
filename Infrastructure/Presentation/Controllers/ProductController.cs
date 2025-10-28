using Microsoft.AspNetCore.Mvc;
using Servieces.Abstractions;
using Shared;
using Shared.DTOs;
using Shared.Enums;
using Shared.ErrorModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController(IServiecManager _serviecManager) : ControllerBase
    {
        //Get All Products
        [HttpGet]
        public async Task<ActionResult<PaginatedResult<ProductDto>>> GelAllProducts([FromQuery]ProcductQueryParams queryParams)
        {
            var Products= await _serviecManager.ProductServices.GetAllProductsAsync(queryParams);
            return Ok(Products);

        }
        [ProducesResponseType(typeof(ErrorToReturn),(int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorToReturn),(int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ErrorToReturn),(int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProductDto),(int)HttpStatusCode.OK)]
        [HttpGet("{Id}")]
        public async Task<ActionResult<ProductDto>> GetById(int Id)
        {
            var Product = await _serviecManager.ProductServices.GetProductByIdAsync(Id);
            return Ok(Product);
        }
        //Get All Types

        [HttpGet("Types")]
        public async Task<ActionResult<IEnumerable<TypeDto>>> GetTypes()
        {
            var Types= await _serviecManager.ProductServices.GetAllTypesAsync();
            return Ok(Types);
        }

        /// <summary>
        /// This Action Get All Product Brands
        /// </summary>
        /// <returns></returns>
        [HttpGet("Brands")]
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetBrands()
        {
            var Brands=await _serviecManager.ProductServices.GetAllBrandsAsync();
            return Ok(Brands);
        }


       

    }   
}
