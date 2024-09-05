using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APICodeFirst.Models;
using APICodeFirst.DTO;
using APICodeFirst.Services;
using Microsoft.AspNetCore.Authorization;

namespace APICodeFirst.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        // private readonly BookDbContext _context;
        private readonly BookAuthorService _service;
        public BooksController(BookAuthorService service)
        {
            _service = service;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            var res = _service.GetBooks();
            return res;
        }
        // GET: api/Books/5
        [HttpGet("{id}")]
        public ActionResult<Book> GetBook(int id)
        {
            var book = _service.GetBookById(new IdDTO { ID = id });

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        // POST: api/Books
        [HttpPost]
        public ActionResult<Book> AddBook(Book book)
        {
            var createdBook = _service.AddBook(book);
            return CreatedAtAction(nameof(GetBook), new { id = createdBook.BookId }, createdBook);
        }

        // PUT: api/Books/5
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, Book book)
        {
            if (id != book.BookId)
            {
                return BadRequest();
            }

            var updatedBook = _service.UpdateBook(book);

            if (updatedBook == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var deletedBook = _service.DeleteBook(new IdDTO { ID = id });

            if (deletedBook == null)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}
