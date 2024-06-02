using BookStoreAPI.Models.BookAuthors;
using BookStoreAPI.Models.Categories;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookStoreAPI.Models.Books
{
    public class Book
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        [ForeignKey(nameof(Category))]
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public virtual ICollection<BookAuthor> BookAuthors { get; set; }

        public string? ImageUrl { get; set; }
    }
}
