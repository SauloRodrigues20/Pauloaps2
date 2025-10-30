using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Library.Application.Interfaces;
using Library.Application.ViewModels;

namespace Library.Web.Controllers
{
    /// <summary>
    /// Controller for book management operations
    /// </summary>
    public class BooksController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IAuthorService _authorService;
        private readonly ILogger<BooksController> _logger;

        public BooksController(
            IBookService bookService,
            IAuthorService authorService,
            ILogger<BooksController> logger)
        {
            _bookService = bookService;
            _authorService = authorService;
            _logger = logger;
        }

        /// <summary>
        /// Lists all books
        /// </summary>
        public async Task<IActionResult> Index(string searchString)
        {
            try
            {
                IEnumerable<BookViewModel> books;

                if (!string.IsNullOrEmpty(searchString))
                {
                    books = await _bookService.SearchByTitleAsync(searchString);
                    ViewBag.SearchString = searchString;
                }
                else
                {
                    books = await _bookService.GetAllAsync();
                }

                return View(books);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading books");
                TempData["Error"] = "Erro ao carregar livros";
                return View(new List<BookViewModel>());
            }
        }

        /// <summary>
        /// Shows book details
        /// </summary>
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var book = await _bookService.GetByIdAsync(id.Value);
                if (book == null)
                {
                    return NotFound();
                }

                return View(book);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading book details");
                TempData["Error"] = "Erro ao carregar detalhes do livro";
                return RedirectToAction(nameof(Index));
            }
        }

        /// <summary>
        /// Shows create form
        /// </summary>
        public async Task<IActionResult> Create()
        {
            await LoadAuthorsSelectList();
            return View();
        }

        /// <summary>
        /// Handles create form submission
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookViewModel bookViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _bookService.CreateAsync(bookViewModel);
                    TempData["Success"] = "Livro criado com sucesso";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error creating book");
                    ModelState.AddModelError("", "Erro ao criar livro");
                }
            }

            await LoadAuthorsSelectList();
            return View(bookViewModel);
        }

        /// <summary>
        /// Shows edit form
        /// </summary>
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var book = await _bookService.GetByIdAsync(id.Value);
                if (book == null)
                {
                    return NotFound();
                }

                await LoadAuthorsSelectList();
                return View(book);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading book for edit");
                TempData["Error"] = "Erro ao carregar livro para edição";
                return RedirectToAction(nameof(Index));
            }
        }

        /// <summary>
        /// Handles edit form submission
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, BookViewModel bookViewModel)
        {
            if (id != bookViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _bookService.UpdateAsync(bookViewModel);
                    if (result)
                    {
                        TempData["Success"] = "Livro atualizado com sucesso";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError("", "Livro não encontrado");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error updating book");
                    ModelState.AddModelError("", "Erro ao atualizar livro");
                }
            }

            await LoadAuthorsSelectList();
            return View(bookViewModel);
        }

        /// <summary>
        /// Shows delete confirmation
        /// </summary>
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var book = await _bookService.GetByIdAsync(id.Value);
                if (book == null)
                {
                    return NotFound();
                }

                var canDelete = await _bookService.CanDeleteAsync(id.Value);
                ViewBag.CanDelete = canDelete;

                return View(book);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading book for delete");
                TempData["Error"] = "Erro ao carregar livro";
                return RedirectToAction(nameof(Index));
            }
        }

        /// <summary>
        /// Handles delete confirmation
        /// </summary>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            try
            {
                var canDelete = await _bookService.CanDeleteAsync(id);
                if (!canDelete)
                {
                    TempData["Error"] = "Não é possível excluir o livro. Existem empréstimos ativos";
                    return RedirectToAction(nameof(Index));
                }

                var result = await _bookService.DeleteAsync(id);
                if (result)
                {
                    TempData["Success"] = "Livro excluído com sucesso";
                }
                else
                {
                    TempData["Error"] = "Erro ao excluir livro";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting book");
                TempData["Error"] = "Erro ao excluir livro";
            }

            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Loads authors for dropdown
        /// </summary>
        private async Task LoadAuthorsSelectList()
        {
            var authors = await _authorService.GetAllAsync();
            ViewBag.Authors = new SelectList(authors, "Id", "FullName");
        }
    }
}
