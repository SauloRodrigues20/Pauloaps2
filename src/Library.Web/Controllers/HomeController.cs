using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Library.Web.Models;
using Library.Application.Interfaces;

namespace Library.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IBookService _bookService;
    private readonly IAuthorService _authorService;
    private readonly ILoanService _loanService;

    public HomeController(
        ILogger<HomeController> logger,
        IBookService bookService,
        IAuthorService authorService,
        ILoanService loanService)
    {
        _logger = logger;
        _bookService = bookService;
        _authorService = authorService;
        _loanService = loanService;
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            var books = await _bookService.GetAllAsync();
            var authors = await _authorService.GetAllAsync();
            var activeLoans = await _loanService.GetActiveLoansAsync();
            var overdueLoans = await _loanService.GetOverdueLoansAsync();

            ViewBag.TotalBooks = books.Count();
            ViewBag.TotalAuthors = authors.Count();
            ViewBag.ActiveLoans = activeLoans.Count();
            ViewBag.OverdueLoans = overdueLoans.Count();

            return View();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading dashboard");
            ViewBag.TotalBooks = 0;
            ViewBag.TotalAuthors = 0;
            ViewBag.ActiveLoans = 0;
            ViewBag.OverdueLoans = 0;
            return View();
        }
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
