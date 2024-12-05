using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoRestaurante.DB.Context;
using ProyectoRestaurante.DB.Entities;

namespace ProyectoRestaurante.Client.MVC.Controllers
{
    public class IngredienteController : Controller
    {
        private readonly RestauranteDbContext _context;

        public IngredienteController(RestauranteDbContext context)
        {
            _context = context;
        }

        // GET: Ingrediente
        public async Task<IActionResult> Index()
        {
            return View(await _context.Ingrediente.ToListAsync());
        }

        // GET: Ingrediente/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingrediente = await _context.Ingrediente
                .FirstOrDefaultAsync(m => m.IdIngrediente == id);
            if (ingrediente == null)
            {
                return NotFound();
            }

            return View(ingrediente);
        }

        // GET: Ingrediente/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ingrediente/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdIngrediente,DscIngrediente")] Ingrediente ingrediente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ingrediente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ingrediente);
        }

        // GET: Ingrediente/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingrediente = await _context.Ingrediente.FindAsync(id);
            if (ingrediente == null)
            {
                return NotFound();
            }
            return View(ingrediente);
        }

        // POST: Ingrediente/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdIngrediente,DscIngrediente")] Ingrediente ingrediente)
        {
            if (id != ingrediente.IdIngrediente)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ingrediente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IngredienteExists(ingrediente.IdIngrediente))
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
            return View(ingrediente);
        }

        // GET: Ingrediente/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingrediente = await _context.Ingrediente
                .FirstOrDefaultAsync(m => m.IdIngrediente == id);
            if (ingrediente == null)
            {
                return NotFound();
            }

            return View(ingrediente);
        }

        // POST: Ingrediente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ingrediente = await _context.Ingrediente.FindAsync(id);
            if (ingrediente != null)
            {
                _context.Ingrediente.Remove(ingrediente);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IngredienteExists(int id)
        {
            return _context.Ingrediente.Any(e => e.IdIngrediente == id);
        }
    }
}
