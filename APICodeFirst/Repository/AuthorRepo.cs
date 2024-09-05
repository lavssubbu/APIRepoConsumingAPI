using APICodeFirst.DTO;
using APICodeFirst.Interface;
using APICodeFirst.Models;
using Microsoft.EntityFrameworkCore;

namespace APICodeFirst.Repository
{
    public class AuthorRepo : IBookAuthor<Author,IdDTO>
    {
        private readonly BookDbContext _context;

        public AuthorRepo(BookDbContext context)
        {
            _context = context;
        }
        public ICollection<Author> GetAll()
        {
          var authors =  _context.Authors.Include(ba => ba.BookAuthors).ThenInclude(b => b.book).ToList();
            return authors;
        }
        public Author Add(Author author)
        {
           /* var author = new Author
            {
                AuthorName = authorDto.Name
            };*/

            _context.Authors.Add(author);
           _context.SaveChanges();

            return author;
        }

        public Author GetValue(IdDTO item)
        {
            var author = _context.Authors
       .Include(a => a.BookAuthors)
       .ThenInclude(ba => ba.book)
       .FirstOrDefault(a => a.AuthorId == item.ID);

            if (author == null)
            {
                return null;
            }

            // Optionally map to a DTO if needed
            return author;
        }


        public Author Update( Author author)
        {
           var authorup = _context.Authors.FirstOrDefault(a=>a.AuthorId==author.AuthorId);

            if (author == null)
            {
                return null;
            }

            // Apply updates
            authorup.AuthorName = author.AuthorName;

            _context.Authors.Update(author);
            _context.SaveChanges();

            return author;
        }

        public Author Delete(IdDTO id)
        {
            var author = _context.Authors.Find(id);

            if (author == null)
            {
                return null;
            }

            _context.Authors.Remove(author);
            _context.SaveChanges();

            return author;
        }
    }
}
