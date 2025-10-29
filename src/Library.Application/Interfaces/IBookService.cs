using Library.Application.ViewModels;

namespace Library.Application.Interfaces
{
    /// <summary>
    /// Service interface for book operations
    /// </summary>
    public interface IBookService
    {
        /// <summary>
        /// Gets all books
        /// </summary>
        Task<IEnumerable<BookViewModel>> GetAllAsync();

        /// <summary>
        /// Gets a book by ID
        /// </summary>
        Task<BookViewModel?> GetByIdAsync(Guid id);

        /// <summary>
        /// Creates a new book
        /// </summary>
        Task<BookViewModel> CreateAsync(BookViewModel bookViewModel);

        /// <summary>
        /// Updates an existing book
        /// </summary>
        Task<bool> UpdateAsync(BookViewModel bookViewModel);

        /// <summary>
        /// Deletes a book
        /// </summary>
        Task<bool> DeleteAsync(Guid id);

        /// <summary>
        /// Checks if a book can be deleted (no active loans)
        /// </summary>
        Task<bool> CanDeleteAsync(Guid id);

        /// <summary>
        /// Searches books by title
        /// </summary>
        Task<IEnumerable<BookViewModel>> SearchByTitleAsync(string title);
    }
}
