using BookFinder.Model;
using BookFinder.Repository;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookFinder.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthorController : Controller
    {
        private ILogger<AuthorController> Logger { get; set; }
        private IAuthorRepository _authorRepository;

        public AuthorController(ILogger<AuthorController> logger, IAuthorRepository authorRepository)
        {
            Logger = logger;
            _authorRepository = authorRepository;
        }

        [HttpGet("GetAuthorByCount/{bookCount:int}")]
        [ProducesResponseType(typeof(IEnumerable<Author>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IList<ValidationFailure>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAuthorByCount(int bookCount)
        {
            var result = await _authorRepository.GetAuthorsByNumberBooks(bookCount);

            return result.ValidationResult.IsValid
                ? Ok(result.Value)
                : BadRequest(result.ValidationResult.Errors); 
        }

        [HttpGet("GetBookCountByAuthorName/{authorName}")]
        [ProducesResponseType(typeof(IEnumerable<Book>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IList<ValidationFailure>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetBookCountByAuthorName(string authorName)
        {
            var result = await _authorRepository.GetBooksByAuthorName(authorName);

            return result.ValidationResult.IsValid
                ? Ok(result.Value.Count())
                : BadRequest(result.ValidationResult.Errors);
        }

        [HttpGet("GetBooksByAuthorName/{bookName}")]
        [ProducesResponseType(typeof(IEnumerable<Book>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IList<ValidationFailure>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetBooksByAuthorName(string bookName)
        {
            var result = await _authorRepository.GetBooksByAuthorName(bookName);

            return result.ValidationResult.IsValid
                ? Ok(result.Value)
                : BadRequest(result.ValidationResult.Errors);
        }
    }
}
