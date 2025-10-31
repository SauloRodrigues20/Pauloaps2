using System.ComponentModel.DataAnnotations;

namespace Library.Domain.Entities
{
    public class Loan
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O livro é obrigatório")]
        public Guid BookId { get; set; }

        public virtual Book? Book { get; set; }

        [Required(ErrorMessage = "O nome do membro é obrigatório")]
        [StringLength(200, ErrorMessage = "O nome do membro não pode ter mais de 200 caracteres")]
        public string MemberName { get; set; } = string.Empty;

        [Required(ErrorMessage = "O email do membro é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        [StringLength(200, ErrorMessage = "O email não pode ter mais de 200 caracteres")]
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

        public LoanStatus Status { get; set; }

        public void MarkAsReturned()
        {
            ReturnDate = DateTime.Now;
            Status = LoanStatus.Returned;
        }

        public bool IsOverdue()
        {
            if (Status == LoanStatus.Returned)
            {
                return false;
            }

            return DateTime.Now > DueDate;
        }
    }
}
