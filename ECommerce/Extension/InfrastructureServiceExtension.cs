using Domain.Contracts;
using Domain.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Presistance;
using Presistance.Data;
using Presistance.Identity;
using Presistance.Repositories;
using StackExchange.Redis;

namespace ECommerce.Extension
{
    public static class InfrastructureServiceExtension
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IDataSeeding, DataSeeding>();
            services.AddScoped<IUntiOfWork, UnitOfWork>();
            services.AddDbContext<StoreDbContext>(op => op.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<IdentityStoreDbContext>(op => op.UseSqlServer(configuration.GetConnectionString("IdentityConnection")));
           
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;


            })
                .AddEntityFrameworkStores<IdentityStoreDbContext>();
            services.AddSingleton<IConnectionMultiplexer>((_) =>
            {
                return ConnectionMultiplexer.Connect(configuration.GetConnectionString("RedisConnection"));
            });
            return services;
        }
    }
}
