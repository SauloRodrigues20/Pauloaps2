using System.ComponentModel.DataAnnotations;

namespace Library.Application.ViewModels
{
    /// <summary>
    /// View model for displaying and editing author information
    /// </summary>
    public class AuthorViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O primeiro nome é obrigatório")]
        [StringLength(100, ErrorMessage = "O primeiro nome não pode ter mais de 100 caracteres")]
        [Display(Name = "Primeiro Nome")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "O sobrenome é obrigatório")]
        [StringLength(100, ErrorMessage = "O sobrenome não pode ter mais de 100 caracteres")]
        [Display(Name = "Sobrenome")]
        public string LastName { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        [Display(Name = "Data de Nascimento")]
        public DateTime? BirthDate { get; set; }

        [StringLength(2000, ErrorMessage = "A biografia não pode ter mais de 2000 caracteres")]
        [Display(Name = "Biografia")]
        [DataType(DataType.MultilineText)]
        public string? Biography { get; set; }

        [Display(Name = "Nome Completo")]
        public string? FullName { get; set; }

        [Display(Name = "Número de Livros")]
        public int BooksCount { get; set; }
    }
}
