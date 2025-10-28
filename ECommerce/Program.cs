
using Domain.Contracts;
using ECommerce.CustomeMiddlewares;
using ECommerce.Extension;
using ECommerce.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Presistance;
using Presistance.Data;
using Presistance.Repositories;
using Servieces;
using Servieces.Abstractions;
using Shared.ErrorModels;
using StackExchange.Redis;
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
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "ECommerce System API",
                    Version = "v1",
                    Description = "This API handles ECommerce management and Sales.",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact
                    {
                        Name = "Ahmed El-Saadany",
                        Email = "ahmed.s.elsaadany2003@gmail.com"
                    },
                    License = new Microsoft.OpenApi.Models.OpenApiLicense
                    {
                        Name = "MIT License"
                    }
                });
            });
            builder.Services.AddAuthorization();

            builder.Services.AddControllers()
           .AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly);


            #region Configure Services
            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddCoreServices();
            builder.Services.AddCustomApiBehavior();
            #endregion

            var app = builder.Build();
             app.UseCustomeMiddleWareException();
            await app.SeedDbAsync();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();
            app.MapGet("/", () => Results.Redirect("/swagger"));


            app.Run();
           

        }
    }
}
