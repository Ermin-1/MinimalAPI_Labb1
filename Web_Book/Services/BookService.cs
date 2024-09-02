using Library_API.Models.DTOs;

namespace Web_Book.Services
{
	public class BookService : BaseService, IBookService
	{
		private readonly IHttpClientFactory _httpClientFactory;

        public BookService(IHttpClientFactory clientFactory) : base(clientFactory) 
		{ 
			_httpClientFactory = clientFactory;
		}

       
        public async Task<T> CreateBookAsync<T>(BookDTO bookDTO)
		{
			return await SendAsync<T>(new Models.ApiRequest
			{
				ApiType = StaticDetails.ApiType.POST,
				Data = bookDTO,
				URL = StaticDetails.BookApiBase + "/api/book",
				AccessToken = ""
			});
		}

		public async Task<T> DeleteBookAsync<T>(int id)
		{
			return await SendAsync<T>(new Models.ApiRequest
			{
				ApiType = StaticDetails.ApiType.DELETE,
				Data = id,
				URL = StaticDetails.BookApiBase + "/api/book/" + id,
				AccessToken = ""
			});
		}

		public Task<T> GetAllBooks<T>()
		{
			return SendAsync<T>(new Models.ApiRequest()
			{
				ApiType = StaticDetails.ApiType.GET,
				URL = StaticDetails.BookApiBase + "/api/books",
				AccessToken = ""
			});
		}

		public async Task<T> GetBookById<T>(int id)
		{
			return await SendAsync<T>(new Models.ApiRequest
			{
				ApiType = StaticDetails.ApiType.GET,
				URL = StaticDetails.BookApiBase + "/api/book/" + id,
				AccessToken = ""
			});
		}

		public async Task<T> UpdateBookAsync<T>(BookDTO bookDTO)
		{
			return await SendAsync<T>(new Models.ApiRequest
			{
				ApiType = StaticDetails.ApiType.PUT,
				Data = bookDTO,
				URL = StaticDetails.BookApiBase + "/api/book",
				AccessToken = ""
			});
		}
	}
}
