using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Library.Domain.Entities
{
    public class Book
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O título é obrigatório")]
        [StringLength(200, ErrorMessage = "O título não pode ter mais de 200 caracteres")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "O ISBN é obrigatório")]
        [StringLength(20, ErrorMessage = "O ISBN não pode ter mais de 20 caracteres")]
        public string ISBN { get; set; } = string.Empty;

        [Required(ErrorMessage = "O ano de publicação é obrigatório")]
        [Range(1000, 9999, ErrorMessage = "O ano de publicação deve estar entre 1000 e 9999")]
        public int PublicationYear { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "O número de cópias disponíveis não pode ser negativo")]
        public int AvailableCopies { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "O número total de cópias não pode ser negativo")]
        public int TotalCopies { get; set; }

        [Required(ErrorMessage = "O autor é obrigatório")]
        public Guid AuthorId { get; set; }

        public virtual Author? Author { get; set; }
        public virtual ICollection<Loan> Loans { get; set; } = new List<Loan>();

        public bool BorrowCopy()
        {
            // verifica se tem cópias disponíveis
            if (AvailableCopies <= 0)
                return false;
            
            AvailableCopies--;
            return true;
        }

        public bool ReturnCopy()
        {
            // não pode ter mais disponíveis que o total
            if (AvailableCopies >= TotalCopies)
                return false;
            
            AvailableCopies++;
            return true;
        }

        public bool ValidateISBN()
        {
            if (string.IsNullOrWhiteSpace(ISBN))
                return false;
            
            // remove espaços e hífens
            var cleanISBN = Regex.Replace(ISBN, @"[\s-]", "");
            
            // valida ISBN-10 ou ISBN-13
            if (cleanISBN.Length == 10)
                return ValidateISBN10(cleanISBN);
            else if (cleanISBN.Length == 13)
                return ValidateISBN13(cleanISBN);
            
            return false;
        }

        private bool ValidateISBN10(string isbn)
        {
            if (!Regex.IsMatch(isbn, @"^\d{9}[\dX]$")) return false;
            int sum = 0;
            for (int i = 0; i < 9; i++) sum += (isbn[i] - '0') * (10 - i);
            int lastDigit = isbn[9] == 'X' ? 10 : (isbn[9] - '0');
            sum += lastDigit;
            return sum % 11 == 0;
        }

        private bool ValidateISBN13(string isbn)
        {
            if (!Regex.IsMatch(isbn, @"^\d{13}$")) return false;
            int sum = 0;
            for (int i = 0; i < 12; i++)
            {
                int digit = isbn[i] - '0';
                sum += (i % 2 == 0) ? digit : digit * 3;
            }
            int checkDigit = (10 - (sum % 10)) % 10;
            return checkDigit == (isbn[12] - '0');
        }
    }
}
