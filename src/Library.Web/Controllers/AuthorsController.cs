using Microsoft.AspNetCore.Mvc;
using Library.Application.Interfaces;
using Library.Application.ViewModels;

namespace Library.Web.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly IAuthorService _authorService;
        private readonly ILogger<AuthorsController> _logger;

        public AuthorsController(
            IAuthorService authorService,
            ILogger<AuthorsController> logger)
        {
            _authorService = authorService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var authors = await _authorService.GetAllAsync();
                return View(authors);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading authors");
                TempData["Error"] = "Erro ao carregar autores";
                return View(new List<AuthorViewModel>());
            }
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var author = await _authorService.GetByIdAsync(id.Value);
                if (author == null)
                {
                    return NotFound();
                }

                return View(author);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading author details");
                TempData["Error"] = "Erro ao carregar detalhes do autor";
                return RedirectToAction(nameof(Index));
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AuthorViewModel authorViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _authorService.CreateAsync(authorViewModel);
                    TempData["Success"] = "Autor criado com sucesso";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error creating author");
                    ModelState.AddModelError("", "Erro ao criar autor");
                }
            }

            return View(authorViewModel);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var author = await _authorService.GetByIdAsync(id.Value);
                if (author == null)
                {
                    return NotFound();
                }

                return View(author);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading author for edit");
                TempData["Error"] = "Erro ao carregar autor para edição";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, AuthorViewModel authorViewModel)
        {
            if (id != authorViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _authorService.UpdateAsync(authorViewModel);
                    if (result)
                    {
                        TempData["Success"] = "Autor atualizado com sucesso";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError("", "Autor não encontrado");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error updating author");
                    ModelState.AddModelError("", "Erro ao atualizar autor");
                }
            }

            return View(authorViewModel);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var author = await _authorService.GetByIdAsync(id.Value);
                if (author == null)
                {
                    return NotFound();
                }

                var canDelete = await _authorService.CanDeleteAsync(id.Value);
                ViewBag.CanDelete = canDelete;

                return View(author);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading author for delete");
                TempData["Error"] = "Erro ao carregar autor";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            try
            {
                var canDelete = await _authorService.CanDeleteAsync(id);
                if (!canDelete)
                {
                    TempData["Error"] = "Não é possível excluir o autor. Existem livros associados";
                    return RedirectToAction(nameof(Index));
                }

                var result = await _authorService.DeleteAsync(id);
                if (result)
                {
                    TempData["Success"] = "Autor excluído com sucesso";
                }
                else
                {
                    TempData["Error"] = "Erro ao excluir autor";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting author");
                TempData["Error"] = "Erro ao excluir autor";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
