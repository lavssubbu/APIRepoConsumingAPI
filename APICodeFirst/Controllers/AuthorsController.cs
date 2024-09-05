using APICodeFirst.DTO;
using APICodeFirst.Models;
using APICodeFirst.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICodeFirst.Controllers
{
  
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthorsController : ControllerBase
    {
        //  private readonly BookDbContext _context;
        private readonly BookAuthorService _service;
        public AuthorsController(BookAuthorService service)
        {
            _service = service;
        }

        [HttpGet]
        public IEnumerable<Author> GetAllAuthor()
        {
            return _service.GetAuthors();
        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
        public ActionResult<Author> GetAuthor(int id)
        {
            var author = _service.GetAuthorById(new IdDTO { ID = id });

            if (author == null)
            {
                return NotFound();
            }

            return Ok(author);
        }

        // POST: api/Authors
        [HttpPost]
        public ActionResult<Author> AddAuthor(Author author)
        {
            var createdAuthor = _service.AddAuthor(author);
            return CreatedAtAction(nameof(GetAuthor), new { id = createdAuthor.AuthorId }, createdAuthor);
        }

        // PUT: api/Authors/5
        [HttpPut("{id}")]
        public IActionResult UpdateAuthor(int id, Author author)
        {
            if (id != author.AuthorId)
            {
                return BadRequest();
            }

            var updatedAuthor = _service.UpdateAuthor(author);

            if (updatedAuthor == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        public IActionResult DeleteAuthor(int id)
        {
            var deletedAuthor = _service.DeleteAuthor(new IdDTO { ID = id });

            if (deletedAuthor == null)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}
