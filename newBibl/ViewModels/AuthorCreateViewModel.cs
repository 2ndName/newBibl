using System.ComponentModel.DataAnnotations;

namespace newBibl.ViewModels
{
    public class AuthorCreateViewModel
    {
        [Required(ErrorMessage = "Имя автора обязательно")]
        [MaxLength(150, ErrorMessage = "Имя слишком длинное")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Биография обязательна")]
        [MaxLength(1000, ErrorMessage = "Биография слишком длинная")]
        public string Biography { get; set; } = string.Empty;

        // Editions (обязательно минимум 1)
        public List<EditionCheckboxItem> Editions { get; set; } = new();

        // Books (необязательно)
        public List<BookCheckboxItem> Books { get; set; } = new();
    }

    public class EditionCheckboxItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsSelected { get; set; }
    }

    public class BookCheckboxItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsSelected { get; set; }
    }
}