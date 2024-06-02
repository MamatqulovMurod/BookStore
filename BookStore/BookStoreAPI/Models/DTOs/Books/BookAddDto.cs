namespace BookStoreAPI.Models.DTOs.Books
{
    public class BookAddDto
    {
        public string Title { get; set; }
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; }
        public IList<Guid> AuthorIds { get; set; }
    }
}
