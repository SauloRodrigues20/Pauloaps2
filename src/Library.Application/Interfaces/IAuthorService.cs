using Library.Application.ViewModels;

namespace Library.Application.Interfaces
{
    public interface IAuthorService
    {
        Task<IEnumerable<AuthorViewModel>> GetAllAsync();

        Task<AuthorViewModel?> GetByIdAsync(Guid id);

        Task<AuthorViewModel> CreateAsync(AuthorViewModel authorViewModel);

        Task<bool> UpdateAsync(AuthorViewModel authorViewModel);

        Task<bool> DeleteAsync(Guid id);

        Task<bool> CanDeleteAsync(Guid id);
    }
}
