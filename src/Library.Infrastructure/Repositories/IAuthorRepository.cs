using Library.Domain.Entities;

namespace Library.Infrastructure.Repositories
{
    /// <summary>
    /// Repository interface for author operations
    /// </summary>
    public interface IAuthorRepository
    {
        /// <summary>
        /// Gets all authors including books count
        /// </summary>
        Task<IEnumerable<Author>> GetAllAsync();

        /// <summary>
        /// Gets an author by ID including books
        /// </summary>
        Task<Author?> GetByIdAsync(Guid id);

        /// <summary>
        /// Adds a new author
        /// </summary>
        Task AddAsync(Author author);

        /// <summary>
        /// Updates an existing author
        /// </summary>
        void Update(Author author);

        /// <summary>
        /// Removes an author
        /// </summary>
        void Remove(Author author);

        /// <summary>
        /// Saves changes to the database
        /// </summary>
        Task<bool> SaveChangesAsync();
    }
}
