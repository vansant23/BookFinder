using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;

namespace BookFinder.Model
{
    public class BookAuthorAssociation
    {
        [Key]
        public int Id { get; set; }
        public int BookId { get; set; }
        public int AuthorId { get; set; }

        public Author Author { get; set; }
        public Book Book { get; set; }
    }
}
