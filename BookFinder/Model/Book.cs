using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BookFinder.Model
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        [JsonIgnore]
        public virtual ICollection<Author> Authors { get; set; }
    }
}
