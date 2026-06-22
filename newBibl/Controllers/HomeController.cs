using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using newBibl.Data;
using newBibl.Models;
using newBibl.ViewModels;
using System.Diagnostics;

namespace newBibl.Controllers
{
    public class HomeController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger,
                              ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> ViewAll(string sortOrder)
        {
            ViewData["BookSort"] = string.IsNullOrEmpty(sortOrder) ? "book_desc" : "";
            ViewData["AuthorSort"] = sortOrder == "author" ? "author_desc" : "author";
            ViewData["PagesSort"] = sortOrder == "pages" ? "pages_desc" : "pages";

            var booksQuery = _context.Books
                .Include(b => b.Author)
                    .ThenInclude(a => a.Editions)
                .Select(b => new BookFullViewModel
                {
                    BookName = b.Name,
                    PageCount = b.PageCount,
                    AuthorName = b.Author != null ? b.Author.Name : "",

                    Editions = b.Author != null
                        ? string.Join(", ", b.Author.Editions.Select(e => e.Name))
                        : ""
                });

            booksQuery = sortOrder switch
            {
                "book_desc" => booksQuery.OrderByDescending(b => b.BookName),
                "author" => booksQuery.OrderBy(b => b.AuthorName),
                "author_desc" => booksQuery.OrderByDescending(b => b.AuthorName),
                "pages" => booksQuery.OrderBy(b => b.PageCount),
                "pages_desc" => booksQuery.OrderByDescending(b => b.PageCount),
                _ => booksQuery.OrderBy(b => b.BookName),
            };

            return View(await booksQuery.ToListAsync());
        }

        public IActionResult AddMenu()
        {
            return View();
        }

        public IActionResult DeleteMenu()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
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
}
