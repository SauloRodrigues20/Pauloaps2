using Library.Application.ViewModels;

namespace Library.Application.Interfaces
{
    /// <summary>
    /// Service interface for author operations
    /// </summary>
    public interface IAuthorService
    {
        /// <summary>
        /// Gets all authors
        /// </summary>
        Task<IEnumerable<AuthorViewModel>> GetAllAsync();

        /// <summary>
        /// Gets an author by ID
        /// </summary>
        Task<AuthorViewModel?> GetByIdAsync(Guid id);

        /// <summary>
        /// Creates a new author
        /// </summary>
        Task<AuthorViewModel> CreateAsync(AuthorViewModel authorViewModel);

        /// <summary>
        /// Updates an existing author
        /// </summary>
        Task<bool> UpdateAsync(AuthorViewModel authorViewModel);

        /// <summary>
        /// Deletes an author
        /// </summary>
        Task<bool> DeleteAsync(Guid id);

        /// <summary>
        /// Checks if an author can be deleted (no associated books)
        /// </summary>
        Task<bool> CanDeleteAsync(Guid id);
    }
}
