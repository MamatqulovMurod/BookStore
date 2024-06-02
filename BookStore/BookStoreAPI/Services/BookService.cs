using System.Linq;
using BookStore.API.Data;
using BookStore.API.Models.Authors;
using BookStore.API.Models.Books;
using BookStore.API.Models.DTOs.Books;
using BookStore.API.Services.Interfaces;
using BookStoreAPI.Models.Authtors;
using BookStoreAPI.Models.Books;
using BookStoreAPI.Models.DTOs.Books;
using Microsoft.EntityFrameworkCore;

namespace BookStore.API.Services
{
    public class BookService(
        BookStoreDbContext dbContext,
        IWebHostEnvironment webHost) : IBookService
    {
        public async Task<BookViewDto> CreateBookAsync(BookAddDto bookAddDto)
        {
            var dbAuthorIds = await dbContext.Authors
                .Where(a => bookAddDto.AuthorIds.Contains(a.Id))
                .Select(a => a.Id)
                .ToListAsync();
            if (bookAddDto.AuthorIds.Except(dbAuthorIds).ToList().Any())
                throw new KeyNotFoundException(nameof(Author));
            var book = new Book()
            {
                Title = bookAddDto.Title,
                Price = bookAddDto.Price,
                CategoryId = bookAddDto.CategoryId,
            };
            book.BookAuthors = bookAddDto.AuthorIds
                .Select(ai => new Models.BooksAuthors.BookAuthor
                {
                    AuthorId = ai
                }).ToList();
            book = (await dbContext.Books.AddAsync(book)).Entity;
            await dbContext.SaveChangesAsync();

            return new BookViewDto
            {
                Id = book.Id,
                Title = book.Title,
                Price = book.Price,
                CategoryId = book.CategoryId,
                Authors = book.BookAuthors.Select(ba => ba.Author).ToList()
            };
        }

        public Task<BookViewDto> DeleteBookByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<BookViewDto>> GetAllBooksAsync()
        {
            var books = GetBooks();
            return await books.ToListAsync();
        }
        private IQueryable<BookViewDto> GetBooks()
            => dbContext.Books
                 .Include(b => b.BookAuthors)
                 .ThenInclude(ba => ba.Author)
                 .Include(b => b.Category)
                 .Select(b => new BookViewDto
                 {
                     Id = b.Id,
                     Title = b.Title,
                     CategoryId = b.CategoryId,
                     Category = b.Category,
                     Authors = b.BookAuthors.Select(ba => ba.Author),
                     Price = b.Price,
                     ImageUrl = b.ImageUrl,
                 });

        public async Task<BookViewDto> GetBookByIdAsync(Guid id)
        {
            var book = await GetBooks()
                .FirstOrDefaultAsync(b => b.Id == id);
            if (book is null)
                throw new KeyNotFoundException(nameof(book));
            return book;
        }

        public Task<BookViewDto> UpdateBookAsync(BookUpdateDto bookUpdateDto)
        {
            throw new NotImplementedException();
        }

        public async Task<BookViewDto> UpdateBookImageByIdAsync(Guid id, IFormFile file)
        {
            var book = await dbContext.Books
                .FirstOrDefaultAsync(b => b.Id == id);
            if (book is null)
                throw new KeyNotFoundException(nameof(book));
            if (file is null)
                throw new ArgumentNullException(nameof(file));
            string[] allowedFileExtensions = [".jpg", ".png", ".jpeg"];
            var fileExtension = Path.GetExtension(file.FileName)?.ToLower();
            if (!allowedFileExtensions.Contains(fileExtension))
                throw new FileLoadException(nameof(fileExtension));
            var filePath = Path.Combine("Images", file.Name + "_" + Guid.NewGuid() + fileExtension);
            var fullPath = Path.Combine(webHost.WebRootPath, filePath);
            using var stream = System.IO.File.Open(fullPath, FileMode.OpenOrCreate);
            await file.CopyToAsync(stream);
            book.ImageUrl = filePath;
            dbContext.Books.Update(book);
            await dbContext.SaveChangesAsync();
            return await GetBookByIdAsync(book.Id);
        }
    }
}
