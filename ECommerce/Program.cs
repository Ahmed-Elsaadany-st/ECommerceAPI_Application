
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Presistance;
using Presistance.Data;
using Presistance.Repositories;
using Servieces;
using Servieces.Abstractions;
using System.Threading.Tasks;

namespace ECommerce
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAuthorization();
            builder.Services.AddControllers()
           .AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly);


            #region Configure Services
            builder.Services.AddDbContext<StoreDbContext>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped<IDataSeeding,DataSeeding>();
            builder.Services.AddScoped<IUntiOfWork, UnitOfWork>();
            builder.Services.AddAutoMapper(p => p.AddProfile(new MappingProfiles()));
            builder.Services.AddScoped<IServiecManager, ServiecManager>();
            #endregion

            var app = builder.Build();
            await InitailizeDbAsync(app);
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
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
