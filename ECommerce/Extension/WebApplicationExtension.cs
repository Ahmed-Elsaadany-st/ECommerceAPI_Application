using Domain.Contracts;
using ECommerce.CustomeMiddlewares;

namespace ECommerce.Extension
{
    public static class WebApplicationExtension
    {
        public static async Task<WebApplication> SeedDbAsync(this WebApplication application)
        {
            using var scope = application.Services.CreateScope();

            var dbInitializer = scope.ServiceProvider.GetRequiredService<IDataSeeding>();

            await dbInitializer.SeedAsync();
            await dbInitializer.SeedIdentityDataAsync();
            return application;
        }
        public static  WebApplication UseCustomeMiddleWareException(this WebApplication app)
        {
            app.UseMiddleware<CustomeExceptionHandlerMiddleware>();
            return app;

        }
    }
}
