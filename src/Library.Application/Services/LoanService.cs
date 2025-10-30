using Library.Application.Interfaces;
using Library.Application.ViewModels;
using Library.Domain.Entities;
using Library.Infrastructure.Repositories;

namespace Library.Application.Services
{
    /// <summary>
    /// Service implementation for loan operations
    /// </summary>
    public class LoanService : ILoanService
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IBookRepository _bookRepository;

        public LoanService(ILoanRepository loanRepository, IBookRepository bookRepository)
        {
            _loanRepository = loanRepository;
            _bookRepository = bookRepository;
        }

        public async Task<IEnumerable<LoanViewModel>> GetAllAsync()
        {
            var loans = await _loanRepository.GetAllAsync();
            return loans.Select(MapToViewModel);
        }

        public async Task<LoanViewModel?> GetByIdAsync(Guid id)
        {
            var loan = await _loanRepository.GetByIdAsync(id);
            return loan != null ? MapToViewModel(loan) : null;
        }

        public async Task<LoanViewModel?> CreateAsync(LoanViewModel loanViewModel)
        {
            // Get the book and check availability
            var book = await _bookRepository.GetByIdAsync(loanViewModel.BookId);
            if (book == null || !book.BorrowCopy())
            {
                return null; // Book not available
            }

            var loan = new Loan
            {
                Id = Guid.NewGuid(),
                BookId = loanViewModel.BookId,
                MemberName = loanViewModel.MemberName,
                MemberEmail = loanViewModel.MemberEmail,
                LoanDate = loanViewModel.LoanDate,
                DueDate = loanViewModel.DueDate,
                Status = LoanStatus.Active
            };

            await _loanRepository.AddAsync(loan);
            _bookRepository.Update(book);
            
            await _loanRepository.SaveChangesAsync();

            loanViewModel.Id = loan.Id;
            loanViewModel.Status = loan.Status.ToString();
            return loanViewModel;
        }

        public async Task<bool> ReturnLoanAsync(Guid id)
        {
            var loan = await _loanRepository.GetByIdAsync(id);
            if (loan == null || loan.Status == LoanStatus.Returned)
            {
                return false;
            }

            var book = await _bookRepository.GetByIdAsync(loan.BookId);
            if (book == null || !book.ReturnCopy())
            {
                return false;
            }

            loan.MarkAsReturned();
            
            _loanRepository.Update(loan);
            _bookRepository.Update(book);
            
            return await _loanRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<LoanViewModel>> GetActiveLoansAsync()
        {
            var loans = await _loanRepository.GetActiveLoansAsync();
            return loans.Select(MapToViewModel);
        }

        public async Task<IEnumerable<LoanViewModel>> GetOverdueLoansAsync()
        {
            var loans = await _loanRepository.GetActiveLoansAsync();
            return loans.Where(l => l.IsOverdue()).Select(MapToViewModel);
        }

        public async Task<IEnumerable<LoanViewModel>> GetByBookIdAsync(Guid bookId)
        {
            var loans = await _loanRepository.GetByBookIdAsync(bookId);
            return loans.Select(MapToViewModel);
        }

        private LoanViewModel MapToViewModel(Loan loan)
        {
            return new LoanViewModel
            {
                Id = loan.Id,
                BookId = loan.BookId,
                BookTitle = loan.Book?.Title,
                MemberName = loan.MemberName,
                MemberEmail = loan.MemberEmail,
                LoanDate = loan.LoanDate,
                DueDate = loan.DueDate,
                ReturnDate = loan.ReturnDate,
                Status = loan.Status.ToString(),
                IsOverdue = loan.IsOverdue()
            };
        }
    }
}
