using System.ComponentModel.DataAnnotations;

namespace Library.Application.ViewModels
{
    /// <summary>
    /// View model for displaying and editing loan information
    /// </summary>
    public class LoanViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O livro é obrigatório")]
        [Display(Name = "Livro")]
        public Guid BookId { get; set; }

        [Display(Name = "Título do Livro")]
        public string? BookTitle { get; set; }

        [Required(ErrorMessage = "O nome do membro é obrigatório")]
        [StringLength(200, ErrorMessage = "O nome do membro não pode ter mais de 200 caracteres")]
        [Display(Name = "Nome do Membro")]
        public string MemberName { get; set; } = string.Empty;

        [Required(ErrorMessage = "O email do membro é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        [StringLength(200, ErrorMessage = "O email não pode ter mais de 200 caracteres")]
        [Display(Name = "Email do Membro")]
        public string MemberEmail { get; set; } = string.Empty;

        [Required(ErrorMessage = "A data do empréstimo é obrigatória")]
        [DataType(DataType.Date)]
        [Display(Name = "Data do Empréstimo")]
        public DateTime LoanDate { get; set; }

        [Required(ErrorMessage = "A data de devolução é obrigatória")]
        [DataType(DataType.Date)]
        [Display(Name = "Data de Devolução")]
        public DateTime DueDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data de Retorno")]
        public DateTime? ReturnDate { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; } = string.Empty;

        [Display(Name = "Atrasado")]
        public bool IsOverdue { get; set; }
    }
}
