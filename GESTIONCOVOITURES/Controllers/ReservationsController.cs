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
    public class ReservationsController : Controller
    {
        private readonly GestioncovoituragesContext _context;

        public ReservationsController(GestioncovoituragesContext context)
        {
            _context = context;
        }

        // GET: Reservations
        public async Task<IActionResult> Index()
        {
            var gestioncovoituragesContext = _context.Reservations.Include(r => r.Passager).Include(r => r.Trajet);
            return View(await gestioncovoituragesContext.ToListAsync());
        }

        // GET: Reservations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .Include(r => r.Passager)
                .Include(r => r.Trajet)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // GET: Reservations/Create
        public IActionResult Create()
        {
            ViewData["PassagerId"] = new SelectList(_context.Utilisateurs, "Id", "Id");
            ViewData["TrajetId"] = new SelectList(_context.Trajets, "Id", "Id");
            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TrajetId,PassagerId,DateReservation")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    Console.WriteLine($"Erreur : {error.ErrorMessage}");
                    if (error.Exception != null)
                    {
                        Console.WriteLine($"Exception : {error.Exception.Message}");
                    }
                }

                // Vous pouvez aussi ajouter les erreurs à la vue pour les afficher dans l'interface utilisateur
                ViewBag.Errors = errors;

                _context.Add(reservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PassagerId"] = new SelectList(_context.Utilisateurs, "Id", "Id", reservation.PassagerId);
            ViewData["TrajetId"] = new SelectList(_context.Trajets, "Id", "Id", reservation.TrajetId);
            return View(reservation);
        }

        // GET: Reservations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            ViewData["PassagerId"] = new SelectList(_context.Utilisateurs, "Id", "Id", reservation.PassagerId);
            ViewData["TrajetId"] = new SelectList(_context.Trajets, "Id", "Id", reservation.TrajetId);
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TrajetId,PassagerId,DateReservation")] Reservation reservation)
        {
            if (id != reservation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(reservation.Id))
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
            ViewData["PassagerId"] = new SelectList(_context.Utilisateurs, "Id", "Id", reservation.PassagerId);
            ViewData["TrajetId"] = new SelectList(_context.Trajets, "Id", "Id", reservation.TrajetId);
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .Include(r => r.Passager)
                .Include(r => r.Trajet)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation != null)
            {
                _context.Reservations.Remove(reservation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationExists(int id)
        {
            return _context.Reservations.Any(e => e.Id == id);
        }
    }
}
