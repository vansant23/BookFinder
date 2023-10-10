using BookFinder.Controllers;
using BookFinder.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication1.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        [BindProperty]
        public string Criteria1Text { get; set; }
        [BindProperty]
        public string Criteria2Text { get; set; }
        [BindProperty]
        public string Criteria3Text { get; set; }
        [BindProperty]
        public string Criteria4Text { get; set; }
        [BindProperty]
        public string Criteria5Text { get; set; }


        private IAuthorRepository _authorRepository { get; set; }
        public IBookRepository _bookRepository { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IAuthorRepository authorRepository, IBookRepository bookRepository)
        {
            _logger = logger;
            _authorRepository = authorRepository;
            _bookRepository = bookRepository;
        }

        public async void OnGet()
        {
            //Acceptance Criteria 1:  Find the Books written by 'Silver Couple' 
            var resultCriteria1 = await _authorRepository.GetBooksByAuthorName("Silver Couple");
            Criteria1Text = String.Join(", ", resultCriteria1.Value.Select(r => r.Name)); 

            //Acceptance Criteria 2:  Find the Authors who wrote 'City Crime' 
            var resultCriteria2 = await _bookRepository.GetAuthorsByBookName("City Crime");
            Criteria2Text = String.Join(", ", resultCriteria2.Value.Select(r => r.Name));

            //Acceptance Criteria 3:  Find the Authors who have not written any books 
            var resultCriteria3 = await _authorRepository.GetAuthorsByNumberBooks(0);
            Criteria3Text = String.Join(", ", resultCriteria3.Value.Select(r => r.Name));

            //Acceptance Criteria 4:  Find how many books has been writen by an author
            var resultCriteria4 = await _authorRepository.GetBooksByAuthorName("Silver Couple");
            Criteria4Text = resultCriteria4.Value.Count().ToString();

            //Acceptance Criteria 5:  Find all the books which is more than $100
            var resultCriteria5 = await _bookRepository.GetBooksByMinimumPrice(100);
            Criteria5Text = String.Join(", ", resultCriteria5.Value.Select(r => r.Name));




        }
    }
}