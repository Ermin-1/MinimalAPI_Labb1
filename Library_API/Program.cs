using AutoMapper;
using FluentValidation;
using Library_API.Data;
using Library_API.EndPoint;
using Library_API.Models.DTOs;
using Library_API.Models.Validations;
using Library_API.Services;
using Microsoft.EntityFrameworkCore;

namespace Library_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Register AutoMapper and validation
            builder.Services.AddAutoMapper(typeof(MappingConfig));
            builder.Services.AddValidatorsFromAssemblyContaining<Program>();
            builder.Services.AddScoped<IValidator<UpdateBookDTO>, BookUpdateValidation>();


            // Register repository interface
            builder.Services.AddScoped<IBookRepository, BookRepository>();

            // Register database
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("Connection")));

            // Add CORS service
            builder.Services.AddCors((setup) => setup.AddPolicy("default", (options) =>
            {
                options.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
            }));


            var app = builder.Build();  // Build the app

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Use CORS after building the app
            app.UseCors("AllowAll");

            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.ConfigurationBookEndPoints();

            app.Run();
        }
    }
}
