using Employee.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using AutoMapper; // Import the AutoMapper namespace
using System;
using System.IO;
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;

namespace Employee
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string txt = "hi";

            var builder = WebApplication.CreateBuilder(args);
            builder.Configuration.AddJsonFile("appsettings.json");

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<Context>(options => options.UseSqlServer(connectionString));

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(txt,
                builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                });
            });

            // Register AutoMapper
            builder.Services.AddAutoMapper(typeof(AutoMapperProfile)); 

            builder.Services.AddControllers();

            // Add Swagger/OpenAPI documentation.
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Your API Name",
                    Version = "v1"
                });

                var xmlFile = "Employee.xml"; 
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                if (File.Exists(xmlPath))
                {
                    c.IncludeXmlComments(xmlPath);
                }
                else
                {
                    Console.WriteLine($"Warning: XML documentation file '{xmlFile}' not found.");
                }
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API Name V1");
                });
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.UseCors(txt);

            app.MapControllers();

            app.Run();
        }
    }
}
