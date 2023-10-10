using BookFinder.Context;
using BookFinder.Model;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;

namespace BookFinder.Repository
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly BookFinderContext _context;

        public AuthorRepository(BookFinderContext context)
        {
            _context = context;
        }

        public async Task<Result<IEnumerable<Book>>> GetBooksByAuthorName(string name)
        {
            var result = new Result<IEnumerable<Book>>();

            try
            {
                if (string.IsNullOrEmpty(name))
                {
                    result.ValidationResult.Errors.Add(new ValidationFailure(nameof(name), "Name cannot be null or empty."));
                    return result;
                }

                result.Value = await _context.Authors.Where(b => b.Name == name).SelectMany(b => b.Books).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                return result;
            }

        }

        public async Task<Result<IEnumerable<Author>>> GetAuthorsByNumberBooks(int count)
        {
            var result = new Result<IEnumerable<Author>>();

            if (count < 0)
            {
                result.ValidationResult.Errors.Add(new ValidationFailure(nameof(count), "Price cannot be less than 0."));
                return result;
            }

            result.Value = await _context.Authors.Where(b => b.Books.Count == count).ToListAsync();

            return result;
        }
    }
}