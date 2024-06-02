using BookStoreAPI.Models.DTOs.Books;

namespace BookStore.API.Services.Interfaces
{
    public interface IBookService
    {
        Task<BookViewDto> GetBookByIdAsync(Guid id);
        Task<IEnumerable<BookViewDto>> GetAllBooksAsync();
        Task<BookViewDto> CreateBookAsync(BookAddDto bookAddDto);
        Task<BookViewDto> UpdateBookAsync(BookUpdateDto bookUpdateDto);
        Task<BookViewDto> DeleteBookByIdAsync(Guid id);
        Task<BookViewDto> UpdateBookImageByIdAsync(Guid id, IFormFile file);
    }
}

}
