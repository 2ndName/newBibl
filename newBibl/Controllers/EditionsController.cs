using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using newBibl.ViewModels;
using newBibl.Models;
using newBibl.Data;

public class EditionsController : Controller
{
    private readonly ApplicationDbContext _context;

    public EditionsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: EDITIONS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Editions.ToListAsync());
    }

    // GET: EDITIONS/Details/5
    public async Task<IActionResult> Details(System.Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var edition = await _context.Editions
            .FirstOrDefaultAsync(m => m.Id == id);
        if (edition == null)
        {
            return NotFound();
        }

        return View(edition);
    }

    // GET: EDITIONS/Create
    public IActionResult Create()
    {
        var model = new EditionCreateViewModel
        {
            Authors = _context.Authors
                .Select(a => new AuthorCheckboxItem
                {
                    Id = a.Id,
                    Name = a.Name,
                    IsSelected = false
                }).ToList()
        };

        return View(model);
    }
    // POST: EDITIONS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(EditionCreateViewModel model)
    {
        if (ModelState.IsValid)
        {
            var edition = new Edition
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                Description = model.Description,
                Authors = new List<Author>()
            };

            var selectedAuthors = model.Authors
                .Where(a => a.IsSelected)
                .Select(a => a.Id)
                .ToList();

            foreach (var authorId in selectedAuthors)
            {
                var author = await _context.Authors.FindAsync(authorId);
                if (author != null)
                {
                    edition.Authors.Add(author);
                }
            }

            _context.Editions.Add(edition);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        return View(model);
    }

    // GET: EDITIONS/Edit/5
    public async Task<IActionResult> Edit(System.Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var edition = await _context.Editions.FindAsync(id);
        if (edition == null)
        {
            return NotFound();
        }
        return View(edition);
    }

    // POST: EDITIONS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(System.Guid? id, [Bind("Id,Name,Description,Authors")] Edition edition)
    {
        if (id != edition.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(edition);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EditionExists(edition.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            TempData["Success"] = "Успешно добавлено!";
            return RedirectToAction("Index", "Home");
        }

        return View(edition);
    }

    // GET: EDITIONS/Delete/5
    public async Task<IActionResult> Delete(System.Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var edition = await _context.Editions
            .FirstOrDefaultAsync(m => m.Id == id);
        if (edition == null)
        {
            return NotFound();
        }

        return View(edition);
    }

    // POST: EDITIONS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(System.Guid? id)
    {
        var edition = await _context.Editions.FindAsync(id);
        if (edition != null)
        {
            _context.Editions.Remove(edition);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool EditionExists(System.Guid? id)
    {
        return _context.Editions.Any(e => e.Id == id);
    }
}
