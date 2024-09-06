
using AutoMapper;
using Azure;
using FluentValidation;
using Library_API.Data;
using Library_API.EndPoint;
using Library_API.Models;
using Library_API.Models.DTOs;
using Library_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using static Azure.Core.HttpHeader;

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

            ////Get all books
            //app.MapGet("/api/books", () =>
            //{
            //    APIResponse response = new APIResponse();

            //    response.Result = BookShelf.bookList;
            //    response.IsSuccess = true;
            //    response.Statuscode = System.Net.HttpStatusCode.OK;

            //    return Results.Ok(response);

            //}).WithName("GetBooks").Produces(200);


            ////Get book by Id
            //app.MapGet("/api/book/{id:int}", (int id) =>
            //{
            //    APIResponse response = new APIResponse();

            //    response.Result = BookShelf.bookList.FirstOrDefault(b => b.Id == id);
            //    response.IsSuccess = true;
            //    response.Statuscode = System.Net.HttpStatusCode.OK;

            //    return Results.Ok(response);
            //}).WithName("GetBook").Produces(200);


   //         //Post books
			//app.MapPost("/api/book", async (
			//  CreateBookDTO bookDTO,
			//  IMapper _mapper,
			//  IValidator<CreateBookDTO> validator) =>
   //         {
   //             APIResponse response = new APIResponse { IsSuccess = false, Statuscode = System.Net.HttpStatusCode.BadRequest };

   //             var validatorResult = await validator.ValidateAsync(bookDTO);

   //             if (!validatorResult.IsValid)
   //             {
   //                 return Results.BadRequest(response);
   //             }
   //             if (BookShelf.bookList.FirstOrDefault(b => b.Title.ToLower() == bookDTO.Title.ToLower()) != null)
   //             {
   //                 response.ErrorMessages.Add("Book name already exists.");
   //                 return Results.BadRequest(response);
   //             }

   //             Book bookA = _mapper.Map<Book>(bookDTO);
   //             bookA.Id = BookShelf.bookList.OrderByDescending(b => b.Id).FirstOrDefault().Id + 1;
   //             BookShelf.bookList.Add(bookA);

   //             BookDTO bookDto = _mapper.Map<BookDTO>(bookA);

   //             response.Result = bookDto;
   //             response.IsSuccess = true;
   //             response.Statuscode = System.Net.HttpStatusCode.Created;

   //             return Results.Ok(response);
   //             }).WithName("CreateBook").Produces<CreateBookDTO>(201).Accepts<APIResponse>("application/json").Produces(400);

            //Update book
            app.MapPut("/api/book", async (IMapper _mapper, IValidator<UpdateBookDTO> _validator, UpdateBookDTO book_U_DTO) =>

            {
				APIResponse response = new() { IsSuccess = false, Statuscode = System.Net.HttpStatusCode.BadRequest };

                //add validation
                var validateResult = await _validator.ValidateAsync(book_U_DTO);
                if(!validateResult.IsValid)
                {
					response.ErrorMessages.Add(validateResult.Errors.FirstOrDefault().ToString());
				}

                Book bookShelf = BookShelf.bookList.FirstOrDefault(b => b.Id == book_U_DTO.Id);
                bookShelf.IsAvalible = book_U_DTO.IsAvalible;
                bookShelf.Title = book_U_DTO.Title;
                bookShelf.Author = book_U_DTO.Author;
                bookShelf.Description = book_U_DTO.Description;
                bookShelf.Genre = book_U_DTO.Genre;

                //AUTOMAPPER

                Book book = _mapper.Map<Book>(book_U_DTO);
                response.Result = _mapper.Map<BookDTO>(bookShelf);
                response.IsSuccess = true;
                response.Statuscode = System.Net.HttpStatusCode.OK;
			}).WithName("UpdateBook").Accepts<UpdateBookDTO>("application/json").Produces<UpdateBookDTO>(400);

			//app.MapDelete("/api/book/{id:int}", (int id) =>
			//{
			//	APIResponse response = new() { IsSuccess = false, Statuscode = System.Net.HttpStatusCode.BadRequest };

			//	Book bookShelf = BookShelf.bookList.FirstOrDefault(b => b.Id == id);

			//	if (bookShelf != null)
			//	{
			//		BookShelf.bookList.Remove(bookShelf);
			//		response.IsSuccess = true;
			//		response.Statuscode = System.Net.HttpStatusCode.NoContent;
			//		return Results.Ok(response);
			//	}
			//	else
			//	{
			//		response.ErrorMessages.Add("Invalid ID");
			//		return Results.BadRequest(response);
			//	}
			//}).WithName("DeleteBook");

		    app.ConfigurationBookEndPoints();
			app.Run();
        }
    }
}
