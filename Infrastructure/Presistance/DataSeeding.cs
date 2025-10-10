using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Presistance.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Presistance
{
    public class DataSeeding(StoreDbContext _StoreContext) : IDataSeeding
    {
        public  async Task SeedAsync()
        {
            try
            {
                #region For Updating Datebase
                if (_StoreContext.Database.GetPendingMigrations().Any())
                {
                    await _StoreContext.Database.MigrateAsync(); // Create Database if does not exist and apply any pending migrations
                }
                #endregion
                #region Product Brands
                if (!_StoreContext.Brands.Any())
                {
                    var BrandsData = await File.ReadAllTextAsync(@"..\Infrastructure\Presistance\Data\Seeding\brands.json"); //Getting the data from the json file
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandsData); // Transforming the data into C# Objects
                    if (brands is not null && brands.Any())
                    {
                        await _StoreContext.Brands.AddRangeAsync(brands);
                        await _StoreContext.SaveChangesAsync();
                    }

                }
                #endregion
                #region Product Types
                if (!_StoreContext.Types.Any())
                {
                    var TypesData = await File.ReadAllTextAsync(@"..\Infrastructure\Presistance\Data\Seeding\types.json"); //Getting the data from the json file
                    var Types = JsonSerializer.Deserialize<List<ProductType>>(TypesData); // Transforming the data into C# Objects
                    if (Types is not null && Types.Any())
                    {
                        await _StoreContext.Types.AddRangeAsync(Types);
                        await _StoreContext.SaveChangesAsync();
                    }

                }

                #endregion
                #region Product
                if (!_StoreContext.Products.Any())
                {
                    var ProductsData = await File.ReadAllTextAsync(@"..\Infrastructure\Presistance\Data\Seeding\products.json"); //Getting the data from the json file
                    var products = JsonSerializer.Deserialize<List<Product>>(ProductsData); // Transforming the data into C# Objects
                    if (products is not null && products.Any())
                    {
                        await _StoreContext.Products.AddRangeAsync(products);
                        await _StoreContext.SaveChangesAsync();
                    }

                }

                #endregion
            }
            catch (Exception ex)
            {

              
            }
        }
    }
}
