using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BookFinder.Model
{
    public class Author
    {
        [Key]
        public int Id { get; set; } 
        public string Name { get; set; }

        [JsonIgnore]
        public ICollection<Book> Books { get; set; }
    }
}
