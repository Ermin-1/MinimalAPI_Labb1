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

        public async Task<Book> AddBook(Book book)
        {
            var ABook = await _context.book.AddAsync(book);
            await _context.SaveChangesAsync();
            return ABook.Entity;
        }

        public async Task<Book> DeleteBook(int id)
        {
            var DBook = await _context.book.FirstOrDefaultAsync (d => d.Id == id);
            if(DBook != null)
            {
                _context.book.Remove(DBook);
                await _context.SaveChangesAsync();
            }

            return null;
        }

        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            return await _context.book.ToListAsync();
        }

     
        public async Task<Book> GetById(int id)
        {
            if(_context.book.FirstOrDefault(g => g.Id == id) != null)
            {
                return await _context.book.FirstOrDefaultAsync(g => g.Id == id);
            }

            return null;
        }

        public async Task<Book> UpdateBook(Book book)
        {
            var UBook = await _context.book.FirstOrDefaultAsync (u => u.Id == book.Id);
            if ( UBook != null)
            {
                UBook.Author = book.Author;
                UBook.Description = book.Description;
                UBook.ReleaseDate = book.ReleaseDate;
                UBook.Genre = book.Genre;
                UBook.Title = book.Title;
                UBook.IsAvalible = book.IsAvalible;
                await _context.SaveChangesAsync();

                return UBook;
            }

            return null;
        }
    }
}
