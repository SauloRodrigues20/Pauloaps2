using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Library.Domain.Entities
{
    /// <summary>
    /// Represents a book in the library system
    /// </summary>
    public class Book
    {
        /// <summary>
        /// Unique identifier for the book
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Book title
        /// </summary>
        [Required(ErrorMessage = "O título é obrigatório")]
        [StringLength(200, ErrorMessage = "O título não pode ter mais de 200 caracteres")]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// International Standard Book Number
        /// </summary>
        [Required(ErrorMessage = "O ISBN é obrigatório")]
        [StringLength(20, ErrorMessage = "O ISBN não pode ter mais de 20 caracteres")]
        public string ISBN { get; set; } = string.Empty;

        /// <summary>
        /// Year the book was published
        /// </summary>
        [Required(ErrorMessage = "O ano de publicação é obrigatório")]
        [Range(1000, 9999, ErrorMessage = "O ano de publicação deve estar entre 1000 e 9999")]
        public int PublicationYear { get; set; }

        /// <summary>
        /// Number of copies currently available for loan
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "O número de cópias disponíveis não pode ser negativo")]
        public int AvailableCopies { get; set; }

        /// <summary>
        /// Total number of copies owned by the library
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "O número total de cópias não pode ser negativo")]
        public int TotalCopies { get; set; }

        /// <summary>
        /// Author identifier
        /// </summary>
        [Required(ErrorMessage = "O autor é obrigatório")]
        public Guid AuthorId { get; set; }

        /// <summary>
        /// Navigation property to the author
        /// </summary>
        public virtual Author? Author { get; set; }

        /// <summary>
        /// Collection of loans for this book
        /// </summary>
        public virtual ICollection<Loan> Loans { get; set; } = new List<Loan>();

        /// <summary>
        /// Borrows a copy of the book
        /// </summary>
        /// <returns>True if successful, false if no copies available</returns>
        public bool BorrowCopy()
        {
            if (AvailableCopies <= 0)
            {
                return false;
            }

            AvailableCopies--;
            return true;
        }

        /// <summary>
        /// Returns a copy of the book
        /// </summary>
        /// <returns>True if successful, false if operation would exceed total copies</returns>
        public bool ReturnCopy()
        {
            if (AvailableCopies >= TotalCopies)
            {
                return false;
            }

            AvailableCopies++;
            return true;
        }

        /// <summary>
        /// Validates the ISBN format
        /// </summary>
        /// <returns>True if valid, false otherwise</returns>
        public bool ValidateISBN()
        {
            if (string.IsNullOrWhiteSpace(ISBN))
            {
                return false;
            }

            // Remove hyphens and spaces
            var cleanISBN = Regex.Replace(ISBN, @"[\s-]", "");

            // Check if it's ISBN-10 or ISBN-13
            if (cleanISBN.Length == 10)
            {
                return ValidateISBN10(cleanISBN);
            }
            else if (cleanISBN.Length == 13)
            {
                return ValidateISBN13(cleanISBN);
            }

            return false;
        }

        private bool ValidateISBN10(string isbn)
        {
            if (!Regex.IsMatch(isbn, @"^\d{9}[\dX]$"))
            {
                return false;
            }

            int sum = 0;
            for (int i = 0; i < 9; i++)
            {
                sum += (isbn[i] - '0') * (10 - i);
            }

            // Handle 'X' as 10
            int lastDigit = isbn[9] == 'X' ? 10 : (isbn[9] - '0');
            sum += lastDigit;

            return sum % 11 == 0;
        }

        private bool ValidateISBN13(string isbn)
        {
            if (!Regex.IsMatch(isbn, @"^\d{13}$"))
            {
                return false;
            }

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
