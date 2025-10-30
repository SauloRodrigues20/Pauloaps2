using System.ComponentModel.DataAnnotations;

namespace Library.Domain.Entities
{
    /// <summary>
    /// Represents a book loan in the library system
    /// </summary>
    public class Loan
    {
        /// <summary>
        /// Unique identifier for the loan
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Book identifier
        /// </summary>
        [Required(ErrorMessage = "O livro é obrigatório")]
        public Guid BookId { get; set; }

        /// <summary>
        /// Navigation property to the book
        /// </summary>
        public virtual Book? Book { get; set; }

        /// <summary>
        /// Name of the member borrowing the book
        /// </summary>
        [Required(ErrorMessage = "O nome do membro é obrigatório")]
        [StringLength(200, ErrorMessage = "O nome do membro não pode ter mais de 200 caracteres")]
        public string MemberName { get; set; } = string.Empty;

        /// <summary>
        /// Email of the member borrowing the book
        /// </summary>
        [Required(ErrorMessage = "O email do membro é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        [StringLength(200, ErrorMessage = "O email não pode ter mais de 200 caracteres")]
        public string MemberEmail { get; set; } = string.Empty;

        /// <summary>
        /// Date the loan was made
        /// </summary>
        [Required(ErrorMessage = "A data do empréstimo é obrigatória")]
        [DataType(DataType.Date)]
        [Display(Name = "Data do Empréstimo")]
        public DateTime LoanDate { get; set; }

        /// <summary>
        /// Date the book is due to be returned
        /// </summary>
        [Required(ErrorMessage = "A data de devolução é obrigatória")]
        [DataType(DataType.Date)]
        [Display(Name = "Data de Devolução")]
        public DateTime DueDate { get; set; }

        /// <summary>
        /// Date the book was actually returned (null if not yet returned)
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "Data de Retorno")]
        public DateTime? ReturnDate { get; set; }

        /// <summary>
        /// Current status of the loan
        /// </summary>
        public LoanStatus Status { get; set; }

        /// <summary>
        /// Marks the loan as returned
        /// </summary>
        public void MarkAsReturned()
        {
            ReturnDate = DateTime.Now;
            Status = LoanStatus.Returned;
        }

        /// <summary>
        /// Checks if the loan is overdue
        /// </summary>
        /// <returns>True if overdue, false otherwise</returns>
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
