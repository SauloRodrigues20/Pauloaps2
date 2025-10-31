using Library.Domain.Entities;

namespace Library.Infrastructure.Repositories
{
    public interface ILoanRepository
    {
        Task<IEnumerable<Loan>> GetAllAsync();

        Task<Loan?> GetByIdAsync(Guid id);

        Task AddAsync(Loan loan);

        void Update(Loan loan);

        void Remove(Loan loan);

        Task<bool> SaveChangesAsync();

        Task<IEnumerable<Loan>> GetActiveLoansAsync();

        Task<IEnumerable<Loan>> GetByBookIdAsync(Guid bookId);
    }
}
