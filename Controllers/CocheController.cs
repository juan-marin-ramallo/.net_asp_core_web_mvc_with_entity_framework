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
    public class CocheController : Controller
    {
        private readonly ReservaCochesContext _context;

        public CocheController(ReservaCochesContext context)
        {
            _context = context;
        }

        // GET: Coche
        public async Task<IActionResult> Index()
        {
            return View(await _context.Coches.ToListAsync());
        }

        // GET: Coche/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coche = await _context.Coches
                .FirstOrDefaultAsync(m => m.Placa == id);
            if (coche == null)
            {
                return NotFound();
            }

            return View(coche);
        }

        // GET: Coche/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Coche/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Placa,Marca,Modelo,Color,PrecioHoraAlquiler")] Coche coche)
        {
            if (ModelState.IsValid)
            {
                _context.Add(coche);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(coche);
        }

        // GET: Coche/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coche = await _context.Coches.FindAsync(id);
            if (coche == null)
            {
                return NotFound();
            }
            return View(coche);
        }

        // POST: Coche/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Placa,Marca,Modelo,Color,PrecioHoraAlquiler")] Coche coche)
        {
            if (id != coche.Placa)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(coche);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CocheExists(coche.Placa))
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
            return View(coche);
        }

        // GET: Coche/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coche = await _context.Coches
                .FirstOrDefaultAsync(m => m.Placa == id);
            if (coche == null)
            {
                return NotFound();
            }

            return View(coche);
        }

        // POST: Coche/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var coche = await _context.Coches.FindAsync(id);
            _context.Coches.Remove(coche);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CocheExists(string id)
        {
            return _context.Coches.Any(e => e.Placa == id);
        }
    }
}
