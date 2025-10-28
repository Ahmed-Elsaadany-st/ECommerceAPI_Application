using Domain.Contracts;
using Presistance.Repositories;
using Servieces;
using Servieces.Abstractions;

namespace ECommerce.Extension
{
    public static class CoreServiceExtension
    {
        public static void AddCoreServices(this IServiceCollection services)
        {
            services.AddAutoMapper(p => p.AddProfile(new MappingProfiles()));
            services.AddScoped<IServiecManager, ServiecManager>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<IUntiOfWork,UnitOfWork>();

        }
    }
}
