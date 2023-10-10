using BookFinder.Model;

namespace BookFinder.Repository
{
    public interface IBookRepository
    {
        public void InitInMemoryDb();

        public Task<Result<IEnumerable<Author>>> GetAuthorsByBookName(string name);

        public Task<Result<IEnumerable<Book>>> GetBooksByMinimumPrice(decimal price);
    }
}
