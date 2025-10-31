using Library.Application.ViewModels;

namespace Library.Application.Interfaces
{
    public interface ILoanService
    {
        Task<IEnumerable<LoanViewModel>> GetAllAsync();

        Task<LoanViewModel?> GetByIdAsync(Guid id);

        Task<LoanViewModel?> CreateAsync(LoanViewModel loanViewModel);

        Task<bool> ReturnLoanAsync(Guid id);

        Task<IEnumerable<LoanViewModel>> GetActiveLoansAsync();

        Task<IEnumerable<LoanViewModel>> GetOverdueLoansAsync();

        Task<IEnumerable<LoanViewModel>> GetByBookIdAsync(Guid bookId);
    }
}
