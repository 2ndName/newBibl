using System.ComponentModel.DataAnnotations;

namespace newBibl.ViewModels
{
    public class EditionCreateViewModel
    {
        [Required(ErrorMessage = "Название издания обязательно")]
        [MaxLength(150, ErrorMessage = "Название слишком длинное")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Описание обязательно")]
        [MaxLength(1000, ErrorMessage = "Описание слишком длинное")]
        public string Description { get; set; } = string.Empty;

        public List<AuthorCheckboxItem> Authors { get; set; } = new();
    }

    public class AuthorCheckboxItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsSelected { get; set; }
    }
}