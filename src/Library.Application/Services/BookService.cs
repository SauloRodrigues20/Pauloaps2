using Library.Application.Interfaces;
using Library.Application.ViewModels;
using Library.Domain.Entities;
using Library.Infrastructure.Repositories;

namespace Library.Application.Services
{
    /// <summary>
    /// Service implementation for book operations
    /// </summary>
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly ILoanRepository _loanRepository;

        public BookService(IBookRepository bookRepository, ILoanRepository loanRepository)
        {
            _bookRepository = bookRepository;
            _loanRepository = loanRepository;
        }

        public async Task<IEnumerable<BookViewModel>> GetAllAsync()
        {
            var books = await _bookRepository.GetAllAsync();
            return books.Select(MapToViewModel);
        }

        public async Task<BookViewModel?> GetByIdAsync(Guid id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            return book != null ? MapToViewModel(book) : null;
        }

        public async Task<BookViewModel> CreateAsync(BookViewModel bookViewModel)
        {
            var book = new Book
            {
                Id = Guid.NewGuid(),
                Title = bookViewModel.Title,
                ISBN = bookViewModel.ISBN,
                PublicationYear = bookViewModel.PublicationYear,
                AvailableCopies = bookViewModel.AvailableCopies,
                TotalCopies = bookViewModel.TotalCopies,
                AuthorId = bookViewModel.AuthorId
            };

            await _bookRepository.AddAsync(book);
            await _bookRepository.SaveChangesAsync();

            bookViewModel.Id = book.Id;
            return bookViewModel;
        }

        public async Task<bool> UpdateAsync(BookViewModel bookViewModel)
        {
            var book = await _bookRepository.GetByIdAsync(bookViewModel.Id);
            if (book == null)
            {
                return false;
            }

            book.Title = bookViewModel.Title;
            book.ISBN = bookViewModel.ISBN;
            book.PublicationYear = bookViewModel.PublicationYear;
            book.AvailableCopies = bookViewModel.AvailableCopies;
            book.TotalCopies = bookViewModel.TotalCopies;
            book.AuthorId = bookViewModel.AuthorId;

            _bookRepository.Update(book);
            return await _bookRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
            {
                return false;
            }

            _bookRepository.Remove(book);
            return await _bookRepository.SaveChangesAsync();
        }

        public async Task<bool> CanDeleteAsync(Guid id)
        {
            var loans = await _loanRepository.GetByBookIdAsync(id);
            return !loans.Any(l => l.Status == LoanStatus.Active || l.Status == LoanStatus.Overdue);
        }

        public async Task<IEnumerable<BookViewModel>> SearchByTitleAsync(string title)
        {
            var books = await _bookRepository.SearchByTitleAsync(title);
            return books.Select(MapToViewModel);
        }

        private BookViewModel MapToViewModel(Book book)
        {
            return new BookViewModel
            {
                Id = book.Id,
                Title = book.Title,
                ISBN = book.ISBN,
                PublicationYear = book.PublicationYear,
                AvailableCopies = book.AvailableCopies,
                TotalCopies = book.TotalCopies,
                AuthorId = book.AuthorId,
                AuthorName = book.Author?.GetFullName()
            };
        }
    }
}
