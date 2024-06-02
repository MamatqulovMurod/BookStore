using BookStoreAPI.Models.Authtors;
using BookStoreAPI.Models.Categories;

namespace BookStoreAPI.Models.DTOs.Books
{
    public class BookViewDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public IEnumerable<Author> Authors { get; set; }
        public string? ImageUrl { get; set; }
    }
}
