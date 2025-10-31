using Library.Domain.Entities;

namespace Library.Infrastructure.Repositories
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<Author>> GetAllAsync();

        Task<Author?> GetByIdAsync(Guid id);

        Task AddAsync(Author author);

        void Update(Author author);

        void Remove(Author author);

        Task<bool> SaveChangesAsync();
    }
}
