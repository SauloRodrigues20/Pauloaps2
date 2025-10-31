using Library.Application.ViewModels;

namespace Library.Application.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<BookViewModel>> GetAllAsync();

        Task<BookViewModel?> GetByIdAsync(Guid id);

        Task<BookViewModel> CreateAsync(BookViewModel bookViewModel);

        Task<bool> UpdateAsync(BookViewModel bookViewModel);

        Task<bool> DeleteAsync(Guid id);

        Task<bool> CanDeleteAsync(Guid id);

        Task<IEnumerable<BookViewModel>> SearchByTitleAsync(string title);
    }
}
