using System.ComponentModel.DataAnnotations;

namespace Library.Application.ViewModels
{
    /// <summary>
    /// View model for displaying and editing book information
    /// </summary>
    public class BookViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O título é obrigatório")]
        [StringLength(200, ErrorMessage = "O título não pode ter mais de 200 caracteres")]
        [Display(Name = "Título")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "O ISBN é obrigatório")]
        [StringLength(20, ErrorMessage = "O ISBN não pode ter mais de 20 caracteres")]
        [Display(Name = "ISBN")]
        public string ISBN { get; set; } = string.Empty;

        [Required(ErrorMessage = "O ano de publicação é obrigatório")]
        [Range(1000, 9999, ErrorMessage = "O ano de publicação deve estar entre 1000 e 9999")]
        [Display(Name = "Ano de Publicação")]
        public int PublicationYear { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "O número de cópias disponíveis não pode ser negativo")]
        [Display(Name = "Cópias Disponíveis")]
        public int AvailableCopies { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "O número total de cópias não pode ser negativo")]
        [Display(Name = "Total de Cópias")]
        public int TotalCopies { get; set; }

        [Required(ErrorMessage = "O autor é obrigatório")]
        [Display(Name = "Autor")]
        public Guid AuthorId { get; set; }

        [Display(Name = "Nome do Autor")]
        public string? AuthorName { get; set; }
    }
}
