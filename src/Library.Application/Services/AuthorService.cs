using Library.Application.Interfaces;
using Library.Application.ViewModels;
using Library.Domain.Entities;
using Library.Infrastructure.Repositories;

namespace Library.Application.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<IEnumerable<AuthorViewModel>> GetAllAsync()
        {
            var authors = await _authorRepository.GetAllAsync();
            return authors.Select(MapToViewModel);
        }

        public async Task<AuthorViewModel?> GetByIdAsync(Guid id)
        {
            var author = await _authorRepository.GetByIdAsync(id);
            return author != null ? MapToViewModel(author) : null;
        }

        public async Task<AuthorViewModel> CreateAsync(AuthorViewModel authorViewModel)
        {
            var author = new Author
            {
                Id = Guid.NewGuid(),
                FirstName = authorViewModel.FirstName,
                LastName = authorViewModel.LastName,
                BirthDate = authorViewModel.BirthDate,
                Biography = authorViewModel.Biography
            };

            await _authorRepository.AddAsync(author);
            await _authorRepository.SaveChangesAsync();

            authorViewModel.Id = author.Id;
            return authorViewModel;
        }

        public async Task<bool> UpdateAsync(AuthorViewModel authorViewModel)
        {
            var author = await _authorRepository.GetByIdAsync(authorViewModel.Id);
            if (author == null)
            {
                return false;
            }

            author.FirstName = authorViewModel.FirstName;
            author.LastName = authorViewModel.LastName;
            author.BirthDate = authorViewModel.BirthDate;
            author.Biography = authorViewModel.Biography;

            _authorRepository.Update(author);
            return await _authorRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var author = await _authorRepository.GetByIdAsync(id);
            if (author == null)
            {
                return false;
            }

            _authorRepository.Remove(author);
            return await _authorRepository.SaveChangesAsync();
        }

        public async Task<bool> CanDeleteAsync(Guid id)
        {
            var author = await _authorRepository.GetByIdAsync(id);
            return author != null && !author.Books.Any();
        }

        private AuthorViewModel MapToViewModel(Author author)
        {
            return new AuthorViewModel
            {
                Id = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName,
                BirthDate = author.BirthDate,
                Biography = author.Biography,
                FullName = author.GetFullName(),
                BooksCount = author.Books.Count
            };
        }
    }
}
