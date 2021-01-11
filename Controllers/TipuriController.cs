using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PharmacyModel.Data;
using PharmacyModel.Models;
using Pharmacy_PatrascuAndreeaLarisa.Models.PharmacyViewModels;
using PharmacyModel.Models.PharmacyViewModels;

namespace Pharmacy_PatrascuAndreeaLarisa.Controllers
{
    public class TipuriController : Controller
    {
        private readonly PharmacyContext _context;

        public TipuriController(PharmacyContext context)
        {
            _context = context;
        }

        // GET: Tipuri
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Tipuri.ToListAsync());
        //}
        public async Task<IActionResult> Index(int? id, int? produsID)
        {
            var viewModel = new TipIndexData();
            viewModel.Tipuri = await _context.Tipuri
            .Include(i => i.FormaProduse)
            .ThenInclude(i => i.Produs)
            .ThenInclude(i => i.Furnizor)
            //.ThenInclude(i => i.Categorie)
            .AsNoTracking()
            .OrderBy(i => i.TipMedicament)
            .ToListAsync();
           
            if (id != null)
            {
                ViewData["TipID"] = id.Value;
                Tip tip = viewModel.Tipuri.Where(
                i => i.TipID == id.Value).Single();
                viewModel.Produse = tip.FormaProduse.Select(s => s.Produs);
            }
            if (produsID != null)
            {
                ViewData["ProdusID"] = produsID.Value;
                viewModel.Furnizori = viewModel.Produse.Where(x => x.ProdusID == produsID).Select(s => s.Furnizor);
            }


            return View(viewModel);

        }

        // GET: Tipuri/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tip = await _context.Tipuri
                .FirstOrDefaultAsync(m => m.TipID == id);
            if (tip == null)
            {
                return NotFound();
            }

            return View(tip);
        }

        // GET: Tipuri/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tipuri/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TipID,TipMedicament")] Tip tip)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tip);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tip);
        }

        // GET: Tipuri/Edit/5

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var tip = await _context.Tipuri
            .Include(i => i.FormaProduse).ThenInclude(i => i.Produs)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.TipID == id);
            if (tip == null)
            {
                return NotFound();
            }
            PopulateFormaProduseData(tip);
            return View(tip);

        }
        private void PopulateFormaProduseData(Tip tip)
        {
            var allProduse = _context.Produse;
            var formaProduse = new HashSet<int>(tip.FormaProduse.Select(c => c.ProdusID));
            var viewModel = new List<FormaProdusData>();
            foreach (var produs in allProduse)
            {
                viewModel.Add(new FormaProdusData
                {
                    ProdusID = produs.ProdusID,
                    NumeProdus = produs.NumeMedicament,
                    SubCeFormaSeRegaseste = formaProduse.Contains(produs.ProdusID)
                });
            }
            ViewData["Produse"] = viewModel;
        }

        // POST: Tipuri/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, string[] selectedProduse)
        {
            if (id == null)
            {
                return NotFound();
            }
            var tipToUpdate = await _context.Tipuri
            .Include(i => i.FormaProduse)
            .ThenInclude(i => i.Produs)
            .FirstOrDefaultAsync(m => m.TipID == id);
            if (await TryUpdateModelAsync<Tip>(tipToUpdate,"",i => i.TipMedicament))
            {
                UpdateFormaProduse(selectedProduse, tipToUpdate);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException /* ex */)
                {

                    ModelState.AddModelError("", "Unable to save changes. " + "Try again, and if the problem persists, ");
                }
                return RedirectToAction(nameof(Index));
            }
            UpdateFormaProduse(selectedProduse, tipToUpdate);
            PopulateFormaProduseData(tipToUpdate);
            return View(tipToUpdate);
        }
        private void UpdateFormaProduse(string[] selectedProduse, Tip tipToUpdate)
        {
            if (selectedProduse == null)
            {
                tipToUpdate.FormaProduse = new List<FormaProdus>();
                return;
            }
            var selectedProduseHS = new HashSet<string>(selectedProduse);
            var tipProduse = new HashSet<int>
            (tipToUpdate.FormaProduse.Select(c => c.Produs.ProdusID));
            foreach (var produs in _context.Produse)
            {
                if (selectedProduseHS.Contains(produs.ProdusID.ToString()))
                {
                    if (!tipProduse.Contains(produs.ProdusID))
                    {
                        tipToUpdate.FormaProduse.Add(new FormaProdus
                        {
                            TipID =tipToUpdate.TipID,
                            ProdusID = produs.ProdusID
                        });
                    }
                }
                else
                {
                    if (tipProduse.Contains(produs.ProdusID))
                    {
                        FormaProdus produsToRemove = tipToUpdate.FormaProduse.FirstOrDefault(i
                       => i.ProdusID == produs.ProdusID);
                        _context.Remove(produsToRemove);
                    }
                }
            }
        }

        // GET: Tipuri/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tip = await _context.Tipuri
                .FirstOrDefaultAsync(m => m.TipID == id);
            if (tip == null)
            {
                return NotFound();
            }

            return View(tip);
        }

        // POST: Tipuri/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tip = await _context.Tipuri.FindAsync(id);
            _context.Tipuri.Remove(tip);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipExists(int id)
        {
            return _context.Tipuri.Any(e => e.TipID == id);
        }
    }
}
