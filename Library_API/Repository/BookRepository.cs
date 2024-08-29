using Library_API.Data;
using Library_API.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Library_API.Services
{
    public class BookRepository : IBookRepository
    {
        private AppDbContext _context;
        public BookRepository(AppDbContext context)
        {
            _context = context;
        }

		public async Task CreateAsync(Book book)
		{
			_context.book.AddAsync(book);
		}

		public async Task DeleteAsync(Book book)
		{
			_context.book.Remove(book);
		}

		public async Task<IEnumerable<Book>> GetAllAsync()
		{
			return await _context.book.ToListAsync();
		}

		public async Task<Book> GetAsync(int id)
		{
			return await _context.book.FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task<Book> GetAsync(string bookTitle)
		{
			return await _context.book.FirstOrDefaultAsync(c=> c.Title == bookTitle.ToLower());
		}

		public async Task SaveAsync()
		{
			await _context.SaveChangesAsync();
		}

		public async Task UpdateAsync(Book book)
		{
			_context.Update(book);
		}


	}
}
