using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PharmacyModel.Data;
using PharmacyModel.Models;

namespace Pharmacy_PatrascuAndreeaLarisa.Controllers
{
    public class ProduseController : Controller
    {
        private readonly PharmacyContext _context;

        public ProduseController(PharmacyContext context)
        {
            _context = context;
        }

        // GET: Produse
        //public async Task<IActionResult> Index()
        //{
        //    var pharmacyContext = _context.Produse.Include(p => p.Categorie).Include(p => p.Furnizor);
        //    return View(await pharmacyContext.ToListAsync());
        //}

        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NumeSortParm"] = String.IsNullOrEmpty(sortOrder) ? "nume_desc" : "";
            ViewData["PretSortParm"] = sortOrder == "Pret" ? "pret_desc" : "Pret";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var produse = from b in _context.Produse
                          select b;

            if (!String.IsNullOrEmpty(searchString))
            {
                produse = produse.Where(s => s.NumeMedicament.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "nume_desc":
                    produse = produse.OrderByDescending(b => b.NumeMedicament);
                    break;
                case "Pret":
                    produse = produse.OrderBy(b => b.Pret);
                    break;
                case "pret_desc":
                    produse = produse.OrderByDescending(b => b.Pret);
                    break;
                default:
                    produse = produse.OrderBy(b => b.NumeMedicament);
                    break;
            }
            int pageSize = 3;
            return View(await PaginatedList<Produs>.CreateAsync(produse.AsNoTracking(), pageNumber ??1, pageSize));
        }

        // GET: Produse/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produs = await _context.Produse
                .Include(p => p.Categorie)
                .Include(p => p.Furnizor)
                .FirstOrDefaultAsync(m => m.ProdusID == id);
            if (produs == null)
            {
                return NotFound();
            }

            return View(produs);
        }

        // GET: Produse/Create
        public IActionResult Create()
        {
            ViewData["CategorieID"] = new SelectList(_context.Categorii, "CategorieID", "CategorieID");
            ViewData["FurnizorID"] = new SelectList(_context.Furnizori, "FurnizorID", "FurnizorID");
            return View();
        }

        // POST: Produse/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProdusID,NumeMedicament,CategorieID,FurnizorID,Doza,Pret,DataExpirare")] Produs produs)
        {
            if (ModelState.IsValid)
            {
                _context.Add(produs);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategorieID"] = new SelectList(_context.Categorii, "CategorieID", "CategorieID", produs.CategorieID);
            ViewData["FurnizorID"] = new SelectList(_context.Furnizori, "FurnizorID", "FurnizorID", produs.FurnizorID);
            return View(produs);
        }

        // GET: Produse/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produs = await _context.Produse.FindAsync(id);
            if (produs == null)
            {
                return NotFound();
            }
            ViewData["CategorieID"] = new SelectList(_context.Categorii, "CategorieID", "CategorieID", produs.CategorieID);
            ViewData["FurnizorID"] = new SelectList(_context.Furnizori, "FurnizorID", "FurnizorID", produs.FurnizorID);
            return View(produs);
        }

        // POST: Produse/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProdusID,NumeMedicament,CategorieID,FurnizorID,Doza,Pret,DataExpirare")] Produs produs)
        {
            if (id != produs.ProdusID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produs);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdusExists(produs.ProdusID))
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
            ViewData["CategorieID"] = new SelectList(_context.Categorii, "CategorieID", "CategorieID", produs.CategorieID);
            ViewData["FurnizorID"] = new SelectList(_context.Furnizori, "FurnizorID", "FurnizorID", produs.FurnizorID);
            return View(produs);
        }

        // GET: Produse/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produs = await _context.Produse
                .Include(p => p.Categorie)
                .Include(p => p.Furnizor)
                .FirstOrDefaultAsync(m => m.ProdusID == id);
            if (produs == null)
            {
                return NotFound();
            }

            return View(produs);
        }

        // POST: Produse/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produs = await _context.Produse.FindAsync(id);
            _context.Produse.Remove(produs);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProdusExists(int id)
        {
            return _context.Produse.Any(e => e.ProdusID == id);
        }
    }
}
