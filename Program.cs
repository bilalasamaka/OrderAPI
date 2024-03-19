
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OrderAPI.Data;
using OrderAPI.Services.Interfaces;
using OrderAPI.Services;
using FluentValidation;
using OrderAPI.Models;
using OrderAPI.Validators;

namespace OrderAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var services = builder.Services;

            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddDbContext<IOrderDBContext, OrderDBContext>(options =>
                options.UseInMemoryDatabase(databaseName: "OrderDB")
            );
            services.AddScoped<IOrderDBContext, OrderDBContext>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddTransient<IValidator<Order>, OrderValidator>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
