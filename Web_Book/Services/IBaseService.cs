using Web_Book.Models;

namespace Web_Book.Services
{
	public interface IBaseService : IDisposable
	{
		ResponseDTO responseModel { get; set; }
		Task<T> SendAsync<T>(ApiRequest apiRequest);
	}
}
