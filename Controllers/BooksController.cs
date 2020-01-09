
using LibraryApi.Domain;
using LibraryApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApi.Controllers
{
    public class BooksController : Controller
    {
        LibraryDataContext Context;

        public BooksController(LibraryDataContext context)
        {
            Context = context;
        }

        [HttpPost("/books")]
        public async Task<IActionResult> AddABook([FromBody] PostBookRequest bookToAdd)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var book = new Book
            {
                Title = bookToAdd.Title,
                Author = bookToAdd.Author,
                Genre = bookToAdd.Genre ?? "Unknown"
            };
            Context.Books.Add(book); // why the error?
            await Context.SaveChangesAsync();


            var bookToReturn = new GetBookResponseDocument
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                Genre = book.Genre
            };
            return CreatedAtRoute("books#getabook", new { id = book.Id }, bookToReturn);
        }

        // GET /books/{id}
        [HttpGet("/books/{id:int}", Name ="books#getabook")]
        public async Task<IActionResult> GetABook(int id)
        {
            var result = await Context.Books
                .Select(b => new GetBookResponseDocument
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author,
                    Genre = b.Genre
                }).SingleOrDefaultAsync(b => b.Id == id);

            if(result == null)
            {
                return NotFound("That book isn't in our library");
            } else
            {
                return Ok(result);
            }
        }


        [HttpGet("/books")]
        public async Task<IActionResult> GetAllBooks([FromQuery] string genre ="all")
        {
            var response = new GetBooksResponseCollection();
            var allBooks = Context.Books.Select(b => new BookSummaryItem
            {
                Id = b.Id,
                Title = b.Title,
                Author = b.Author,
                Genre = b.Genre
            });
            if(genre != "all")
            {
                allBooks = allBooks.Where(b => b.Genre == genre);
            }
            response.Books = await allBooks.ToListAsync();
            response.GenreFilter = genre;
            
            return Ok(response);
        }
    }
}
