#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplicationMVC.Models;

namespace WebApplicationMVC.Controllers
{
    public class ReservaCocheController : Controller
    {
        private readonly ReservaCochesContext _context;

        public ReservaCocheController(ReservaCochesContext context)
        {
            _context = context;
        }

        // GET: ReservaCoche
        public async Task<IActionResult> Index()
        {
            var reservaCochesContext = _context.ReservaCoches.Include(r => r.NumeroReservaNavigation).Include(r => r.PlacaNavigation);
            return View(await reservaCochesContext.ToListAsync());
        }

        // GET: ReservaCoche/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservaCoche = await _context.ReservaCoches
                .Include(r => r.NumeroReservaNavigation)
                .Include(r => r.PlacaNavigation)
                .FirstOrDefaultAsync(m => m.NumeroReserva == id);
            if (reservaCoche == null)
            {
                return NotFound();
            }

            return View(reservaCoche);
        }

        // GET: ReservaCoche/Create
        public IActionResult Create()
        {
            ViewData["NumeroReserva"] = new SelectList(_context.Reservas, "NumeroReserva", "NumeroReserva");
            ViewData["Placa"] = new SelectList(_context.Coches, "Placa", "Placa");
            return View();
        }

        // POST: ReservaCoche/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NumeroReserva,Placa,GalonesGasolina")] ReservaCoche reservaCoche)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reservaCoche);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["NumeroReserva"] = new SelectList(_context.Reservas, "NumeroReserva", "NumeroReserva", reservaCoche.NumeroReserva);
            ViewData["Placa"] = new SelectList(_context.Coches, "Placa", "Placa", reservaCoche.Placa);
            return View(reservaCoche);
        }

        // GET: ReservaCoche/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservaCoche = await _context.ReservaCoches.FindAsync(id);
            if (reservaCoche == null)
            {
                return NotFound();
            }
            ViewData["NumeroReserva"] = new SelectList(_context.Reservas, "NumeroReserva", "NumeroReserva", reservaCoche.NumeroReserva);
            ViewData["Placa"] = new SelectList(_context.Coches, "Placa", "Placa", reservaCoche.Placa);
            return View(reservaCoche);
        }

        // POST: ReservaCoche/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NumeroReserva,Placa,GalonesGasolina")] ReservaCoche reservaCoche)
        {
            if (id != reservaCoche.NumeroReserva)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservaCoche);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservaCocheExists(reservaCoche.NumeroReserva))
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
            ViewData["NumeroReserva"] = new SelectList(_context.Reservas, "NumeroReserva", "NumeroReserva", reservaCoche.NumeroReserva);
            ViewData["Placa"] = new SelectList(_context.Coches, "Placa", "Placa", reservaCoche.Placa);
            return View(reservaCoche);
        }

        // GET: ReservaCoche/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservaCoche = await _context.ReservaCoches
                .Include(r => r.NumeroReservaNavigation)
                .Include(r => r.PlacaNavigation)
                .FirstOrDefaultAsync(m => m.NumeroReserva == id);
            if (reservaCoche == null)
            {
                return NotFound();
            }

            return View(reservaCoche);
        }

        // POST: ReservaCoche/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservaCoche = await _context.ReservaCoches.FindAsync(id);
            _context.ReservaCoches.Remove(reservaCoche);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservaCocheExists(int id)
        {
            return _context.ReservaCoches.Any(e => e.NumeroReserva == id);
        }
    }
}
