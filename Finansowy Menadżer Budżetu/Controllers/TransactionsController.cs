using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Finansowy_Menadżer_Budżetu.Data;
using Finansowy_Menadżer_Budżetu.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;

namespace Finansowy_Menadżer_Budżetu.Controllers
{
    [Authorize]

    public class TransactionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IWebHostEnvironment _environment;

        public TransactionsController(ApplicationDbContext context, UserManager<IdentityUser> userManager, IWebHostEnvironment environment)
        {
            _context = context;
            _userManager = userManager;
            _environment = environment;
        }

        // GET: Transactions
        public async Task<IActionResult> Index()
        {
            IdentityUser użytkownik = _userManager.FindByNameAsync(User.Identity.Name).Result;

            var applicationDbContext = _context.Transactions.Include(e => e.User).Where(p => p.UserId == użytkownik.Id).Include(t => t.Category).Include(t => t.Group);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Transactions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Transactions == null)
            {
                return NotFound();
            }

            var transactions = await _context.Transactions
                .Include(t => t.Category)
                .Include(t => t.Group)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transactions == null)
            {
                return NotFound();
            }

            return View(transactions);
        }

        // GET: Transactions/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "Id", "Id");
            ViewData["GroupId"] = new SelectList(_context.Set<Group>(), "Id", "Id");
            ViewData["CategoryName"] = new SelectList(_context.Set<Category>(), "Name", "Name");
            ViewData["GroupName"] = new SelectList(_context.Set<Group>(), "Name", "Name");
            return View();
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Transactions transactions, string[] selectedUsers)
        {
            IdentityUser użytkownik = _userManager.FindByNameAsync(User.Identity.Name).Result;
            transactions.UserId = użytkownik.Id;

            if (ModelState.IsValid)
            {
                if (transactions.File != null)
                {
                    // Przykład: Zapisz plik w katalogu
                    var sciezkaDoZapisu = Path.Combine(_environment.WebRootPath, "uploads");
                    var unikalnaNazwaPliku = Guid.NewGuid().ToString() + "_" + transactions.File.FileName;
                    var sciezkaPelna = Path.Combine(sciezkaDoZapisu, unikalnaNazwaPliku);

                    using (var stream = new FileStream(sciezkaPelna, FileMode.Create))
                    {
                        await transactions.File.CopyToAsync(stream);
                    }

                    // Zapisz informacje o pliku w bazie danych za pomocą Entity Framework
                    transactions.FilePatch = unikalnaNazwaPliku; // Dodaj właściwość do modelu Produkt
                }

                _context.Add(transactions);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "Id", "Id", transactions.CategoryId);
            ViewData["GroupId"] = new SelectList(_context.Set<Group>(), "Id", "Id", transactions.GroupId);

            return View(transactions);
        }


        // GET: Transactions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Transactions == null)
            {
                return NotFound();
            }

            var transactions = await _context.Transactions.FindAsync(id);
            if (transactions == null)
            {
                return NotFound();
            }

            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "Id", "Id", transactions.CategoryId);
            ViewData["GroupId"] = new SelectList(_context.Set<Group>(), "Id", "Id", transactions.GroupId);
            return View(transactions);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,CategoryId,GroupId,Amount,SharedWith,Comment,Title,FilePatch")] Transactions transactions)
        {
            if (id != transactions.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transactions);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransactionsExists(transactions.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "Id", "Id", transactions.CategoryId);
            ViewData["GroupId"] = new SelectList(_context.Set<Group>(), "Id", "Id", transactions.GroupId);
            return View(transactions);
        }

        // GET: Transactions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Transactions == null)
            {
                return NotFound();
            }

            var transactions = await _context.Transactions
                .Include(t => t.Category)
                .Include(t => t.Group)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transactions == null)
            {
                return NotFound();
            }

            return View(transactions);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Transactions == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Transactions'  is null.");
            }
            var transactions = await _context.Transactions.FindAsync(id);
            if (transactions != null)
            {
                _context.Transactions.Remove(transactions);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransactionsExists(int id)
        {
          return (_context.Transactions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
