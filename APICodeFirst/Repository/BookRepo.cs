using APICodeFirst.DTO;
using APICodeFirst.Interface;
using APICodeFirst.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICodeFirst.Repository
{
    public class BookRepo : IBookAuthor<Book,IdDTO>
    {
        //connect with dbcontext
        private readonly BookDbContext _context;

        public BookRepo(BookDbContext context)
        {
            _context = context;
        }
        public ICollection<Book> GetAll()
        {
          var books = _context.Books.Include(ba=>ba.BookAuthors).ThenInclude(a=>a.author).ToList();
          if(books!=null)
                return books;

            return null;
        }
      
        public Book Add(Book book)
        {
          /*  var book = new Book
            {
                Title = bookDto.Title,
                BookAuthors = bookDto.AuthorIDs.Select(id => new BookAuthor { AuthorId = id }).ToList()
            };*/

            _context.Books.Add(book);
            _context.SaveChanges();

            return book;
        }

        public Book GetValue(IdDTO item)
        {
            var book = _context.Books
                .Include(b => b.BookAuthors)
                .ThenInclude(ba => ba.author)
                .FirstOrDefault(b => b.BookId == item.ID);

            if (book != null)
                return book;

            return null;
        }

       
        public Book Update(Book book)
        {
            // Find the existing book in the context
            var existingBook = _context.Books.FirstOrDefault(b => b.BookId == book.BookId);

            if (existingBook != null)
            {
                // Update the properties of the existing book
                existingBook.Title = book.Title;
                existingBook.Price = book.Price;

                // Save changes
                _context.SaveChanges();

                return existingBook;
            }

            return null;

            /* var book = _context.Books
                 .Include(b => b.BookAuthors)
                 .FirstOrDefault(b => b.BookId == id);

             if (book == null)
             {
                 return null;
             }

             book.Title = bookDto.Title;
             book.BookAuthors = bookDto.AuthorIds.Select(id => new BookAuthor { BookId = id }).ToList();

             _context.Books.Update(book);
             _context.SaveChanges();

             return book; // Or return the updated BookDto if needed
            */
        }

        public Book Delete(IdDTO iddto)
        {
            var book = _context.Books.Find(iddto.ID);

            if (book == null)
            {
                return null;
            }

            _context.Books.Remove(book);
            _context.SaveChanges();

            return book;
        }
    }
}
