using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GESTIONCOVOITURES.Models;

namespace GESTIONCOVOITURES.Controllers
{
    public class TrajetsController : Controller
    {
        private readonly GestioncovoituragesContext _context;

        public TrajetsController(GestioncovoituragesContext context)
        {
            _context = context;
        }

        // GET: Trajets
        public async Task<IActionResult> Index()
        {
            return View(await _context.Trajets.ToListAsync());
        }

        // GET: Trajets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trajet = await _context.Trajets
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trajet == null)
            {
                return NotFound();
            }

            return View(trajet);
        }

        // GET: Trajets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Trajets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,VilleDepart,VilleArrivee,Date,Heure,Nombreplace,Nombreplacereserve,Montant")] Trajet trajet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trajet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(trajet);
        }

        // GET: Trajets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trajet = await _context.Trajets.FindAsync(id);
            if (trajet == null)
            {
                return NotFound();
            }
            return View(trajet);
        }

        // POST: Trajets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,VilleDepart,VilleArrivee,Date,Heure,Nombreplace,Nombreplacereserve,Montant")] Trajet trajet)
        {
            if (id != trajet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trajet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrajetExists(trajet.Id))
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
            return View(trajet);
        }

        // GET: Trajets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trajet = await _context.Trajets
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trajet == null)
            {
                return NotFound();
            }

            return View(trajet);
        }

        // POST: Trajets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trajet = await _context.Trajets.FindAsync(id);
            if (trajet != null)
            {
                _context.Trajets.Remove(trajet);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrajetExists(int id)
        {
            return _context.Trajets.Any(e => e.Id == id);
        }
    }
}
