using AutoMapper;
using FluentValidation;
using Library_API.Models;
using Library_API.Models.DTOs;
using Library_API.Services;
using static Azure.Core.HttpHeader;

namespace Library_API.EndPoint
{
	public static class BookEndPoints
	{
		public static void ConfigurationBookEndPoints(this WebApplication app)
		{
            app.MapGet("/api/books", GetAllBooks).WithName("GetBooks").Produces<APIResponse>();
            app.MapGet("/api/book/{id:int}", GetBook).WithDisplayName("GetBook").Produces<APIResponse>();
            app.MapPost("/api/book", CreateBook).WithName("CreateBook").Accepts<CreateBookDTO>("application/json").Produces(201).Produces(400);
            app.MapDelete("/api/book/{id:int}", DeleteBook).WithName("DeleteBook");
            app.MapPut("/api/book", UpdateBook).WithName("UpdateBook").Accepts<UpdateBookDTO>("application/json").Produces<UpdateBookDTO>(200).Produces(400);

        }

        public async static Task<IResult> GetAllBooks (IBookRepository bookRepository)
        {
            APIResponse response = new() { IsSuccess = false, Statuscode = System.Net.HttpStatusCode.BadRequest };


            response.Result = await bookRepository.GetAllAsync();
            response.IsSuccess = true;
            response.Statuscode = System.Net.HttpStatusCode.OK;

            return Results.Ok(response);
        }

        private async static Task<IResult> GetBook(IBookRepository bookRepository, int id)
        {
            APIResponse response = new APIResponse();

            response.Result = await bookRepository.GetAsync(id);
            response.IsSuccess = true;
            response.Statuscode = System.Net.HttpStatusCode.OK;

            return Results.Ok(response);
        }

        private async static Task<IResult> CreateBook(IBookRepository bookRepository, IMapper _mapper, CreateBookDTO bookCreate)
        {
            APIResponse response = new() { IsSuccess = false, Statuscode = System.Net.HttpStatusCode.BadRequest };
            if (bookRepository.GetAsync(bookCreate.Title).GetAwaiter().GetResult() != null)
            {
                response.ErrorMessages.Add("Book already exists");
                return Results.BadRequest(response);
            }

            Book book = _mapper.Map<Book>(bookCreate);
            await bookRepository.CreateAsync(book);
            await bookRepository.SaveAsync();
            BookDTO bookDTO = _mapper.Map<BookDTO>(book);


            response.Result = bookDTO;
            response.IsSuccess = true;
            response.Statuscode = System.Net.HttpStatusCode.Created;
            return Results.Ok(response);
        }


        public async static Task<IResult> DeleteBook(IBookRepository _bookRepo, int id)
		{
            APIResponse response = new() { IsSuccess = false, Statuscode = System.Net.HttpStatusCode.BadRequest };
            
            Book bookFromDB = await _bookRepo.GetAsync(id);

			if(bookFromDB != null)
			{
				await _bookRepo.DeleteAsync(bookFromDB);
				await _bookRepo.SaveAsync();
                response.IsSuccess = true;
                response.Statuscode = System.Net.HttpStatusCode.NoContent;
                return Results.Ok(response);
            }
            else
            {
                response.IsSuccess = false;
                response.Statuscode = System.Net.HttpStatusCode.NotFound;
                return Results.NotFound(response);
            }
        }

        public async static Task<IResult> UpdateBook(IBookRepository _bookRepo, IMapper _mapper, UpdateBookDTO bookUpdate)
        {
            APIResponse response = new() { IsSuccess = false, Statuscode = System.Net.HttpStatusCode.BadRequest };

            // Mappa och uppdatera boken
            await _bookRepo.UpdateAsync(_mapper.Map<Book>(bookUpdate));
            await _bookRepo.SaveAsync();

            // Returnera uppdaterad bok
            response.Result = _mapper.Map<UpdateBookDTO>(await _bookRepo.GetAsync(bookUpdate.Id));
            response.IsSuccess = true;
            response.Statuscode = System.Net.HttpStatusCode.OK;

            return Results.Ok(response);
        }





    }
}
