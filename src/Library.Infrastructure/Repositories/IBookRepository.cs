using Library.Domain.Entities;

namespace Library.Infrastructure.Repositories
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllAsync();

        Task<Book?> GetByIdAsync(Guid id);

        Task AddAsync(Book book);

        void Update(Book book);

        void Remove(Book book);

        Task<bool> SaveChangesAsync();

        Task<IEnumerable<Book>> SearchByTitleAsync(string title);
    }
}
