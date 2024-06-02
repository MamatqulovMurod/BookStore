using BookStore.API.Services;
using BookStore.API.Services.Interfaces;
using BookStoreAPI.Models.DTOs.Books;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BooksController(IBookService bookService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var books = await bookService.GetAllBooksAsync();
            return Ok(books);
        }

        [HttpGet]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var book = await bookService.GetBookByIdAsync(id);
                return Ok(book);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(new
                {
                    Message = "Book not found!"
                });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = ex.Message,
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(BookAddDto bookAddDto)
        {
            try
            {
                var book = await bookService.CreateBookAsync(bookAddDto);
                return Ok(book);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(new
                {
                    Message = "Author not found!"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    ex.Message
                });
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdateImageByBookId(Guid bookId,
             IFormFile imageFile)
        {
            try
            {
                var book = await bookService.UpdateBookImageByIdAsync(bookId, imageFile);
                return Ok(book);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(new
                {
                    Message = "Book not found!"
                });
            }
            catch (ArgumentNullException e)
            {
                return BadRequest(new
                {
                    Message = "Image cannot be null!"
                });

            }
            catch (FileLoadException e)
            {
                return BadRequest(new
                {
                    Message = "Please load only jpg, jpeg and png file!"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    ex.Message
                });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBookByIdAsync(Guid id)
        {
            try
            {
                var book = await bookService.DeleteBookByIdAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new
                {
                    Message = "Book not found!"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    ex.Message
                });
            }
        }
    }
}
