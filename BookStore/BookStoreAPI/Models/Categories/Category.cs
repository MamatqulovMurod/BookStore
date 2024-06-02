using BookStoreAPI.Models.Books;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookStoreAPI.Models.Categories
{
    public class Category
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [MaxLength(255), Required]
        public string Name { get; set; }
        public List<Book> Books { get; set; }

    }
}
