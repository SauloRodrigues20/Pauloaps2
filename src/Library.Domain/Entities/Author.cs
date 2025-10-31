using System.ComponentModel.DataAnnotations;

namespace Library.Domain.Entities
{
    public class Author
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O primeiro nome é obrigatório")]
        [StringLength(100, ErrorMessage = "O primeiro nome não pode ter mais de 100 caracteres")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "O sobrenome é obrigatório")]
        [StringLength(100, ErrorMessage = "O sobrenome não pode ter mais de 100 caracteres")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "A data de nascimento é obrigatória")]
        [DataType(DataType.Date)]
        [Display(Name = "Data de Nascimento")]
        public DateTime BirthDate { get; set; }

        [StringLength(2000, ErrorMessage = "A biografia não pode ter mais de 2000 caracteres")]
        public string? Biography { get; set; }

        public virtual ICollection<Book> Books { get; set; } = new List<Book>();

        public string GetFullName()
        {
            return $"{FirstName} {LastName}";
        }
    }
}
