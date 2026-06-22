using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace newBibl.Models
{
    public class Book
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Название книги не может быть пустым")]
        [MaxLength(150, ErrorMessage = "Название очень длинное")]
        public string Name { get; set; } = string.Empty;

        [Range(1, double.MaxValue, ErrorMessage = "Страниц должно быть больше 0")]
        public int PageCount { get; set; }

        [Required(ErrorMessage = "Книга должна иметь автора")]
        public Guid AuthorId { get; set; }

        [ForeignKey(nameof(AuthorId))]
        public Author? Author { get; set; }

    }
}
