using BookFinder.Model;
using BookFinder.Repository;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace BookFinder.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BookController : Controller
    {
        private ILogger<BookController> Logger { get; set; }
        private IBookRepository _bookRepository;

        public BookController(ILogger<BookController> logger, IBookRepository bookRepository)
        {
            Logger = logger;
            _bookRepository = bookRepository;
        }

        [HttpGet("GetAuthorsByBookName/{bookName}")]
        [ProducesResponseType(typeof(IEnumerable<Author>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IList<ValidationFailure>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAuthorsByBookName(string bookName)
        {
            var result = await _bookRepository.GetAuthorsByBookName(bookName);

            return result.ValidationResult.IsValid
                ? Ok(result.Value)
                : BadRequest(result.ValidationResult.Errors);
        }

        [HttpGet("GetBooksAbovePrice/{price}")]
        [ProducesResponseType(typeof(IEnumerable<Book>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IList<ValidationFailure>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetBooksAbovePrice([FromRoute] decimal price)
        {
            var result = await _bookRepository.GetBooksByMinimumPrice(price);

            return result.ValidationResult.IsValid
                ? Ok(result.Value)
                : BadRequest(result.ValidationResult.Errors);
        }
    }
}
