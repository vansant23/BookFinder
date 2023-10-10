using BookFinder.Context;
using BookFinder.Model;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace BookFinder.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookFinderContext _context;

        public BookRepository(BookFinderContext context)
        {
            _context = context;
        }

        #region Initialize In-Memory Database
        public void InitInMemoryDb()
        {
            var authors = new List<Author>
                {
                new Author
                {
                    Id = 1,
                    Name = "John Doe"
                },
                new Author
                {
                    Id = 2,
                    Name = "Owen Jones"
                },
                new Author
                {
                    Id = 3,
                    Name = "Silver Couple"
                },
                new Author
                {
                    Id = 4,
                    Name = "Tom Smith"
                }
                };

            var books = new List<Book>
                {
                new Book
                {
                    Id = 1,
                    Name = "City Crime",
                    Price = 100
                },
                new Book
                {
                    Id = 2,
                    Name = "Comedy Blast",
                    Price = 110
                },
                new Book
                {
                    Id = 3,
                    Name = "Horror Terror",
                    Price = 100
                },
                new Book
                {
                    Id = 4,
                    Name = "Mysterious Path",
                    Price = 110
                }
                };

            var bookAuthorAssociations = new List<BookAuthorAssociation>
                {
                new BookAuthorAssociation
                {
                    Id = 1,
                    BookId = 1,
                    AuthorId = 1
                },
                new BookAuthorAssociation
                {
                    Id = 2,
                    BookId = 1,
                    AuthorId = 2
                },
                new BookAuthorAssociation
                {
                    Id = 3,
                    BookId = 2,
                    AuthorId = 1
                },
                new BookAuthorAssociation
                {
                    Id = 4,
                    BookId = 3,
                    AuthorId = 3
                },
                new BookAuthorAssociation
                {
                    Id = 5,
                    BookId = 4,
                    AuthorId = 3
                }
                };

            _context.Authors.AddRange(authors);
            _context.Books.AddRange(books);
            _context.BookAuthorAssociations.AddRange(bookAuthorAssociations);

            _context.SaveChanges();
        }
        #endregion

        public async Task<Result<IEnumerable<Author>>> GetAuthorsByBookName(string name)
        {
            var result = new Result<IEnumerable<Author>>();

            if (string.IsNullOrEmpty(name))
            {
                result.ValidationResult.Errors.Add(new ValidationFailure(nameof(name), "Name cannot be null or empty."));
                return result;
            }

            result.Value = await _context.Books.Where(b => b.Name == name).SelectMany(b => b.Authors).ToListAsync();
            return result;
        }

        public async Task<Result<IEnumerable<Book>>> GetBooksByMinimumPrice(decimal price)
        {
            var result = new Result<IEnumerable<Book>>();

            if (price < 0)
            {
                result.ValidationResult.Errors.Add(new ValidationFailure(nameof(price), "Price cannot be less than 0."));
                return result;
            }

            result.Value = await _context.Books.Where(b => b.Price > price).ToListAsync();
            return result;
        }
    }
}