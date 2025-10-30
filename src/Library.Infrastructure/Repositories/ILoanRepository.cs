using Library.Domain.Entities;

namespace Library.Infrastructure.Repositories
{
    /// <summary>
    /// Repository interface for loan operations
    /// </summary>
    public interface ILoanRepository
    {
        /// <summary>
        /// Gets all loans including book information
        /// </summary>
        Task<IEnumerable<Loan>> GetAllAsync();

        /// <summary>
        /// Gets a loan by ID including book information
        /// </summary>
        Task<Loan?> GetByIdAsync(Guid id);

        /// <summary>
        /// Adds a new loan
        /// </summary>
        Task AddAsync(Loan loan);

        /// <summary>
        /// Updates an existing loan
        /// </summary>
        void Update(Loan loan);

        /// <summary>
        /// Removes a loan
        /// </summary>
        void Remove(Loan loan);

        /// <summary>
        /// Saves changes to the database
        /// </summary>
        Task<bool> SaveChangesAsync();

        /// <summary>
        /// Gets all active loans
        /// </summary>
        Task<IEnumerable<Loan>> GetActiveLoansAsync();

        /// <summary>
        /// Gets loans by book ID
        /// </summary>
        Task<IEnumerable<Loan>> GetByBookIdAsync(Guid bookId);
    }
}
