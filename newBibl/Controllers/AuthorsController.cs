
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using newBibl.ViewModels;
using newBibl.Models;
using newBibl.Data;

public class AuthorsController : Controller
{
    private readonly ApplicationDbContext _context;

    public AuthorsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: AUTHORS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Authors.ToListAsync());
    }

    // GET: AUTHORS/Details/5
    public async Task<IActionResult> Details(System.Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var author = await _context.Authors
            .FirstOrDefaultAsync(m => m.Id == id);
        if (author == null)
        {
            return NotFound();
        }

        return View(author);
    }

    // GET: AUTHORS/Create
    public IActionResult Create()
    {
        var model = new AuthorCreateViewModel
        {
            Editions = _context.Editions
                .Select(e => new EditionCheckboxItem
                {
                    Id = e.Id,
                    Name = e.Name
                }).ToList(),

            Books = _context.Books
                .Select(b => new BookCheckboxItem
                {
                    Id = b.Id,
                    Name = b.Name
                }).ToList()
        };

        return View(model);
    }

    // POST: AUTHORS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(AuthorCreateViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        if (!model.Editions.Any(e => e.IsSelected))
        {
            ModelState.AddModelError("", "Необходимо выбрать минимум одно издание.");
            return View(model);
        }

        var author = new Author
        {
            Id = Guid.NewGuid(),
            Name = model.Name,
            Biography = model.Biography,
            Editions = new List<Edition>(),
            Books = new List<Book>()
        };

        var selectedEditions = model.Editions
            .Where(e => e.IsSelected)
            .Select(e => e.Id);

        foreach (var id in selectedEditions)
        {
            var edition = await _context.Editions.FindAsync(id);
            if (edition != null)
                author.Editions.Add(edition);
        }

        var selectedBooks = model.Books
            .Where(b => b.IsSelected)
            .Select(b => b.Id);

        foreach (var id in selectedBooks)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
                author.Books.Add(book);
        }

        _context.Authors.Add(author);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch
        {
            ModelState.AddModelError("", "Ошибка сохранения данных.");
            return View(model);
        }

        TempData["Success"] = "Успешно добавлено!";
        return RedirectToAction("Index", "Home");
    }

    // GET: AUTHORS/Edit/5
    public async Task<IActionResult> Edit(System.Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var author = await _context.Authors.FindAsync(id);
        if (author == null)
        {
            return NotFound();
        }
        return View(author);
    }

    // POST: AUTHORS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(System.Guid? id, [Bind("Id,Name,Biography,Editions,Books")] Author author)
    {
        if (id != author.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(author);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorExists(author.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(author);
    }

    // GET: AUTHORS/Delete/5
    public async Task<IActionResult> Delete(System.Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var author = await _context.Authors
            .FirstOrDefaultAsync(m => m.Id == id);
        if (author == null)
        {
            return NotFound();
        }

        return View(author);
    }

    // POST: AUTHORS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(System.Guid? id)
    {
        var author = await _context.Authors.FindAsync(id);
        if (author != null)
        {
            _context.Authors.Remove(author);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool AuthorExists(System.Guid? id)
    {
        return _context.Authors.Any(e => e.Id == id);
    }
}
