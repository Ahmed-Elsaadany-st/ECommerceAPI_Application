
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Presistance;
using Presistance.Data;
using System.Threading.Tasks;

namespace ECommerce
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            #region Configure Services
            builder.Services.AddDbContext<StoreDbContext>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped<IDataSeeding,DataSeeding>();
            #endregion

            var app = builder.Build();
            await InitailizeDbAsync(app);
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
           

           async Task InitailizeDbAsync(WebApplication application)
            {
                //Create an object from type that implements the IDataSeendign Interface
                using var scope=application.Services.CreateScope();
                var DbInitialzer=scope.ServiceProvider.GetRequiredService<IDataSeeding>();
                await DbInitialzer.SeedAsync();
            }
        }
    }
}
