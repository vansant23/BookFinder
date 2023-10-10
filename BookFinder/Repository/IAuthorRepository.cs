using BookFinder.Model;

namespace BookFinder.Repository
{
    public interface IAuthorRepository
    {
        public Task<Result<IEnumerable<Book>>> GetBooksByAuthorName(string name);

        public Task<Result<IEnumerable<Author>>> GetAuthorsByNumberBooks(int count);
    }
}
