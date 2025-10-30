using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Library.Application.Interfaces;
using Library.Application.ViewModels;

namespace Library.Web.Controllers
{
    /// <summary>
    /// Controller for loan management operations
    /// </summary>
    public class LoansController : Controller
    {
        private readonly ILoanService _loanService;
        private readonly IBookService _bookService;
        private readonly ILogger<LoansController> _logger;

        public LoansController(
            ILoanService loanService,
            IBookService bookService,
            ILogger<LoansController> logger)
        {
            _loanService = loanService;
            _bookService = bookService;
            _logger = logger;
        }

        /// <summary>
        /// Lists all loans
        /// </summary>
        public async Task<IActionResult> Index()
        {
            try
            {
                var loans = await _loanService.GetAllAsync();
                return View(loans);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading loans");
                TempData["Error"] = "Erro ao carregar empréstimos";
                return View(new List<LoanViewModel>());
            }
        }

        /// <summary>
        /// Shows loan details
        /// </summary>
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var loan = await _loanService.GetByIdAsync(id.Value);
                if (loan == null)
                {
                    return NotFound();
                }

                return View(loan);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading loan details");
                TempData["Error"] = "Erro ao carregar detalhes do empréstimo";
                return RedirectToAction(nameof(Index));
            }
        }

        /// <summary>
        /// Shows create loan form
        /// </summary>
        public async Task<IActionResult> Create()
        {
            await LoadBooksSelectList();
            
            // Set default dates
            var model = new LoanViewModel
            {
                LoanDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(14) // Default 14 days loan period
            };
            
            return View(model);
        }

        /// <summary>
        /// Handles create loan form submission
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LoanViewModel loanViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _loanService.CreateAsync(loanViewModel);
                    if (result != null)
                    {
                        TempData["Success"] = "Empréstimo realizado com sucesso";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError("", "Livro não disponível para empréstimo");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error creating loan");
                    ModelState.AddModelError("", "Erro ao realizar empréstimo");
                }
            }

            await LoadBooksSelectList();
            return View(loanViewModel);
        }

        /// <summary>
        /// Shows return loan form
        /// </summary>
        public async Task<IActionResult> Return(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var loan = await _loanService.GetByIdAsync(id.Value);
                if (loan == null)
                {
                    return NotFound();
                }

                if (loan.Status == "Returned")
                {
                    TempData["Error"] = "Este empréstimo já foi devolvido";
                    return RedirectToAction(nameof(Index));
                }

                return View(loan);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading loan for return");
                TempData["Error"] = "Erro ao carregar empréstimo";
                return RedirectToAction(nameof(Index));
            }
        }

        /// <summary>
        /// Handles return loan confirmation
        /// </summary>
        [HttpPost, ActionName("Return")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReturnConfirmed(Guid id)
        {
            try
            {
                var result = await _loanService.ReturnLoanAsync(id);
                if (result)
                {
                    TempData["Success"] = "Livro devolvido com sucesso";
                }
                else
                {
                    TempData["Error"] = "Erro ao devolver livro";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error returning loan");
                TempData["Error"] = "Erro ao devolver livro";
            }

            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Shows active loans
        /// </summary>
        public async Task<IActionResult> Active()
        {
            try
            {
                var loans = await _loanService.GetActiveLoansAsync();
                return View("Index", loans);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading active loans");
                TempData["Error"] = "Erro ao carregar empréstimos ativos";
                return View("Index", new List<LoanViewModel>());
            }
        }

        /// <summary>
        /// Shows overdue loans
        /// </summary>
        public async Task<IActionResult> Overdue()
        {
            try
            {
                var loans = await _loanService.GetOverdueLoansAsync();
                return View("Index", loans);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading overdue loans");
                TempData["Error"] = "Erro ao carregar empréstimos atrasados";
                return View("Index", new List<LoanViewModel>());
            }
        }

        /// <summary>
        /// Loads books for dropdown
        /// </summary>
        private async Task LoadBooksSelectList()
        {
            var books = await _bookService.GetAllAsync();
            ViewBag.Books = new SelectList(books.Where(b => b.AvailableCopies > 0), "Id", "Title");
        }
    }
}
