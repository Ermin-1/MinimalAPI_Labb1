using Library_API.Models;

namespace Library_API.Services
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllAsync();
        Task<Book> GetAsync(int id);
        Task<Book> GetAsync(string bookName);
        Task CreateAsync(Book book);
        Task UpdateAsync(Book book);
        Task DeleteAsync(Book book);
        Task SaveAsync();
    }
}
