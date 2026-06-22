using System.ComponentModel.DataAnnotations;

namespace newBibl.Models
{
    public class Author
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Имя автора обязательно")]
        [MaxLength(150, ErrorMessage = "Имя слишком длинное")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Биография обязательна")]
        [MaxLength(1000, ErrorMessage = "Биография слишком длинная")]
        public string Biography { get; set; } = string.Empty;

        public ICollection<Edition> Editions { get; set; } = new List<Edition>();

        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
