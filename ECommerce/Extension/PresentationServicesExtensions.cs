using ECommerce.Factories;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Extension
{
    public static class PresentationServicesExtensions
    {
        public static void AddCustomApiBehavior(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = ApiResponseFactory.GenerateApiValidationErrorResponse;
            });
        }
    }
}
