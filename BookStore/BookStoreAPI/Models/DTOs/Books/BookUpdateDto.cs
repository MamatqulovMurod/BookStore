namespace BookStoreAPI.Models.DTOs.Books
{
    public class BookUpdateDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; }
        public IList<Guid> AuthorIds { get; set; }
    }
}
