using System.ComponentModel.DataAnnotations;

namespace newBibl.Models
{
    public class Edition
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Название издания обязательно")]
        [MaxLength(150, ErrorMessage = "Название слишком длинное")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Описание обязательно")]
        [MaxLength(1000, ErrorMessage = "Описание слишком длинное")]
        public string Description { get; set; } = string.Empty;

        public ICollection<Author> Authors { get; set; } = new List<Author>();
    }
}
