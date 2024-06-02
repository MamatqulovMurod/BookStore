using BookStoreAPI.Models.Authtors;
using BookStoreAPI.Models.BookAuthors;
using BookStoreAPI.Models.Books;
using BookStoreAPI.Models.Categories;
using Microsoft.EntityFrameworkCore;

namespace BookStore.API.Data
{
    public class BookStoreDbContext(DbContextOptions<BookStoreDbContext> options)
        : DbContext(options)
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<BookAuthor> AuthorBooks { get; set; }
        public DbSet<Category> Categories { get; set; }

    }
}


