using Library.Application.ViewModels;

namespace Library.Application.Interfaces
{
    /// <summary>
    /// Service interface for loan operations
    /// </summary>
    public interface ILoanService
    {
        /// <summary>
        /// Gets all loans
        /// </summary>
        Task<IEnumerable<LoanViewModel>> GetAllAsync();

        /// <summary>
        /// Gets a loan by ID
        /// </summary>
        Task<LoanViewModel?> GetByIdAsync(Guid id);

        /// <summary>
        /// Creates a new loan (borrows a book)
        /// </summary>
        Task<LoanViewModel?> CreateAsync(LoanViewModel loanViewModel);

        /// <summary>
        /// Returns a loaned book
        /// </summary>
        Task<bool> ReturnLoanAsync(Guid id);

        /// <summary>
        /// Gets all active loans
        /// </summary>
        Task<IEnumerable<LoanViewModel>> GetActiveLoansAsync();

        /// <summary>
        /// Gets all overdue loans
        /// </summary>
        Task<IEnumerable<LoanViewModel>> GetOverdueLoansAsync();

        /// <summary>
        /// Gets loans by book ID
        /// </summary>
        Task<IEnumerable<LoanViewModel>> GetByBookIdAsync(Guid bookId);
    }
}
