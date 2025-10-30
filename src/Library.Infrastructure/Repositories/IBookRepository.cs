using Library.Domain.Entities;

namespace Library.Infrastructure.Repositories
{
    /// <summary>
    /// Repository interface for book operations
    /// </summary>
    public interface IBookRepository
    {
        /// <summary>
        /// Gets all books including author information
        /// </summary>
        Task<IEnumerable<Book>> GetAllAsync();

        /// <summary>
        /// Gets a book by ID including author information
        /// </summary>
        Task<Book?> GetByIdAsync(Guid id);

        /// <summary>
        /// Adds a new book
        /// </summary>
        Task AddAsync(Book book);

        /// <summary>
        /// Updates an existing book
        /// </summary>
        void Update(Book book);

        /// <summary>
        /// Removes a book
        /// </summary>
        void Remove(Book book);

        /// <summary>
        /// Saves changes to the database
        /// </summary>
        Task<bool> SaveChangesAsync();

        /// <summary>
        /// Searches books by title
        /// </summary>
        Task<IEnumerable<Book>> SearchByTitleAsync(string title);
    }
}
