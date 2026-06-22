using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using newBibl.ViewModels;
using newBibl.Data;
using newBibl.Models;

public class BooksController : Controller
{
    private readonly ApplicationDbContext _context;

    public BooksController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: BOOKS
    public async Task<IActionResult> Index()    
    {
        var books = _context.Books
         .Include(b => b.Author)
         .ThenInclude(a => a.Editions);
        return View(await books.ToListAsync());
    }

    // GET: BOOKS/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
            return NotFound();

        var book = await _context.Books
            .Include(b => b.Author)
            .ThenInclude(a => a.Editions)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (book == null)
            return NotFound();

        return View(book);
    }

    // GET: BOOKS/Create
    public IActionResult Create()
    {
        var model = new BookCreateViewModel
        {
            Authors = _context.Authors
                .Select(a => new AuthorSelectItem
                {
                    Id = a.Id,
                    Name = a.Name
                }).ToList()
        };

        return View(model);
    }

    // POST: BOOKS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(BookCreateViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Authors = _context.Authors
                .Select(a => new AuthorSelectItem
                {
                    Id = a.Id,
                    Name = a.Name
                }).ToList();

            return View(model);
        }

        var book = new Book
        {
            Id = Guid.NewGuid(),
            Name = model.Name,
            PageCount = model.PageCount,
            AuthorId = model.AuthorId
        };

        _context.Books.Add(book);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch
        {
            ModelState.AddModelError("", "Ошибка сохранения.");
            return View(model);
        }
        TempData["Success"] = "Успешно добавлено!";
        return RedirectToAction("Index", "Home");
    }
    // GET: BOOKS/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
            return NotFound();

        var book = await _context.Books.FindAsync(id);

        if (book == null)
            return NotFound();

        ViewBag.Authors = _context.Authors
            .Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.Name
            }).ToList();

        return View(book);
    }
    // POST: BOOKS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, Book book)
    {
        if (id != book.Id)
            return NotFound();

        if (!ModelState.IsValid)
        {
            ViewBag.Authors = _context.Authors
                .Select(a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.Name
                }).ToList();

            return View(book);
        }

        try
        {
            _context.Update(book);
            await _context.SaveChangesAsync();
        }
        catch
        {
            ModelState.AddModelError("", "Ошибка сохранения.");
            return View(book);
        }

        TempData["Success"] = "Книга успешно обновлена!";
        return RedirectToAction("Index", "Home");
    }
    // GET: BOOKS/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
            return NotFound();

        var book = await _context.Books
            .Include(b => b.Author)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (book == null)
            return NotFound();

        return View(book);
    }

    // POST: BOOKS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(System.Guid? id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book != null)
        {
            _context.Books.Remove(book);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool BookExists(System.Guid? id)
    {
        return _context.Books.Any(e => e.Id == id);
    }
}
