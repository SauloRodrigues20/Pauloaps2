using System.ComponentModel.DataAnnotations;

namespace Library.Domain.Entities
{
    /// <summary>
    /// Represents an author in the library system
    /// </summary>
    public class Author
    {
        /// <summary>
        /// Unique identifier for the author
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Author's first name
        /// </summary>
        [Required(ErrorMessage = "O primeiro nome é obrigatório")]
        [StringLength(100, ErrorMessage = "O primeiro nome não pode ter mais de 100 caracteres")]
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Author's last name
        /// </summary>
        [Required(ErrorMessage = "O sobrenome é obrigatório")]
        [StringLength(100, ErrorMessage = "O sobrenome não pode ter mais de 100 caracteres")]
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Author's birth date
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "Data de Nascimento")]
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// Author's biography
        /// </summary>
        [StringLength(2000, ErrorMessage = "A biografia não pode ter mais de 2000 caracteres")]
        public string? Biography { get; set; }

        /// <summary>
        /// Collection of books written by this author
        /// </summary>
        public virtual ICollection<Book> Books { get; set; } = new List<Book>();

        /// <summary>
        /// Gets the full name of the author
        /// </summary>
        /// <returns>Full name (FirstName + LastName)</returns>
        public string GetFullName()
        {
            return $"{FirstName} {LastName}";
        }
    }
}
