using Domain.Contracts;
using Domain.Models;
using Domain.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
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
    public class DataSeeding(StoreDbContext _StoreContext
        ,RoleManager<IdentityRole> _roleManager,UserManager<ApplicationUser> _userManager) : IDataSeeding
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

        public async Task SeedIdentityDataAsync()
        {
            try
            {
                //1]Seed Roles
                if (!_roleManager.Roles.Any())
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                }
                //2]Seed Users
                if (!_userManager.Users.Any())
                {
                    var userAdmin = new ApplicationUser()
                    {
                        DisplayName = "Admin",
                        Email = "Admin@gmail.com",
                        PhoneNumber = "01022689307",
                        UserName = "Admin"
                    };

                    var resultAdmin = await _userManager.CreateAsync(userAdmin, "passwordADMIN");
                    if (!resultAdmin.Succeeded)
                    {
                        foreach (var error in resultAdmin.Errors)
                        {
                            Console.WriteLine($"Admin Error: {error.Description}");
                        }
                    }

                    var userSuperAdmin = new ApplicationUser()
                    {
                        DisplayName = "SuperAdmin",
                        Email = "SuperAdmin@gmail.com",
                        PhoneNumber = "01022689307",
                        UserName = "SuperAdmin"
                    };

                    var resultSuper = await _userManager.CreateAsync(userSuperAdmin, "passwordSUPERADMIN");
                    if (!resultSuper.Succeeded)
                    {
                        foreach (var error in resultSuper.Errors)
                        {
                            Console.WriteLine($"SuperAdmin Error: {error.Description}");
                        }
                    }

                    // Add to roles only if creation succeeded
                    if (resultAdmin.Succeeded)
                        await _userManager.AddToRoleAsync(userAdmin, "Admin");

                    if (resultSuper.Succeeded)
                        await _userManager.AddToRoleAsync(userSuperAdmin, "SuperAdmin");

                }

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
