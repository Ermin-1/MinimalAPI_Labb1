
using AutoMapper;
using FluentValidation;
using Library_API.Data;
using Library_API.Models;
using Library_API.Models.DTOs;
using Library_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace Library_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //registrera automapper och validations
            builder.Services.AddAutoMapper(typeof(MappingConfig));
            builder.Services.AddValidatorsFromAssemblyContaining<Program>();

            //Registrera Interface
            builder.Services.AddScoped<IBookRepository, BookRepository>();

            //registrera db
            builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("Connection")));


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            //Get all books
            app.MapGet("/api/book", async ([FromServices] IBookRepository _bookRepository) =>
            {
                var books = await _bookRepository.GetAllBooks();

                APIResponse respone = new APIResponse
                {
                    IsSuccess = true,
                    Result = books,
                    Statuscode = System.Net.HttpStatusCode.OK
                };

                return Results.Ok(respone);
            }).WithName("GetAllBooks").Produces<APIResponse>(200);

            //Get by ID
            app.MapGet("/api/book/{id:int}", async (int id, [FromServices] IBookRepository _bookRepository, [FromServices] IMapper _mapper) =>
            {
                var singleBook = await _bookRepository.GetById(id);

                if (singleBook != null)
                {
                    APIResponse response = new APIResponse
                    {
                        Result = _mapper.Map<BookDTO>(singleBook),  // Användning av AutoMapper
                        IsSuccess = true,
                        Statuscode = System.Net.HttpStatusCode.OK
                    };

                    return Results.Ok(response);
                }

                APIResponse notFoundResponse = new APIResponse
                {
                    IsSuccess = false,
                    Statuscode = System.Net.HttpStatusCode.NotFound,
                    ErrorMessages = new List<string> { "Book not found" }
                };

                return Results.NotFound(notFoundResponse);
            }).WithName("GetBookById").Produces<APIResponse>(200).Produces<APIResponse>(404);



            app.Run();
        }
    }
}
