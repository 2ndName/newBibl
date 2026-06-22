using System.ComponentModel.DataAnnotations;

namespace newBibl.ViewModels
{
    public class BookCreateViewModel
    {
        [Required(ErrorMessage = "Название книги обязательно")]
        [MaxLength(150, ErrorMessage = "Название слишком длинное")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Количество страниц обязательно")]
        [Range(1, 10000, ErrorMessage = "Страниц должно быть больше 0")]
        public int PageCount { get; set; }

        [Required(ErrorMessage = "Выберите автора")]
        public Guid AuthorId { get; set; }

        public List<AuthorSelectItem> Authors { get; set; } = new();
    }

    public class AuthorSelectItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}