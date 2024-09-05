using APICodeFirst.DTO;
using APICodeFirst.Interface;
using APICodeFirst.Models;
using APICodeFirst.Repository;

namespace APICodeFirst.Services
{
    public class BookAuthorService
    {
        private readonly IBookAuthor<Book,IdDTO> _bookRepo;
        private readonly IBookAuthor<Author,IdDTO> _authorRepo;

        public BookAuthorService(IBookAuthor<Book,IdDTO> bookser,IBookAuthor<Author,IdDTO> authser)
        {
            _bookRepo = bookser;
            _authorRepo = authser;
        }

        public List<Book> GetBooks()
        {
           var books = _bookRepo.GetAll().ToList();
            return books;
        }

        public List<Author> GetAuthors()
        {
           var authors = _authorRepo.GetAll().ToList();
            return authors;
        }
        public Book GetBookById(IdDTO id)
        {
            return _bookRepo.GetValue(id);
        }

        public Author GetAuthorById(IdDTO id)
        {
            return _authorRepo.GetValue(id);
        }

        public Book AddBook(Book book)
        {
            return _bookRepo.Add(book);
        }

        public Author AddAuthor(Author author)
        {
            return _authorRepo.Add(author);
        }

        public Book UpdateBook(Book book)
        {
            return _bookRepo.Update(book);
        }

        public Author UpdateAuthor(Author author)
        {
            return _authorRepo.Update(author);
        }

        public Book DeleteBook(IdDTO id)
        {
            return _bookRepo.Delete(id);
        }

        public Author DeleteAuthor(IdDTO id)
        {
            return _authorRepo.Delete(id);
        }
    }
}
