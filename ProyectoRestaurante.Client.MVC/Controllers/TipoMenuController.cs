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
    public class TipoMenuController : Controller
    {
        private readonly RestauranteDbContext _context;

        public TipoMenuController(RestauranteDbContext context)
        {
            _context = context;
        }

        // GET: TipoMenu
        public async Task<IActionResult> Index()
        {
            return View(await _context.TipoMenu.ToListAsync());
        }

        // GET: TipoMenu/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoMenu = await _context.TipoMenu
                .FirstOrDefaultAsync(m => m.IdTipoMenu == id);
            if (tipoMenu == null)
            {
                return NotFound();
            }

            return View(tipoMenu);
        }

        // GET: TipoMenu/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoMenu/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTipoMenu,DscTipoMenu")] TipoMenu tipoMenu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoMenu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoMenu);
        }

        // GET: TipoMenu/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoMenu = await _context.TipoMenu.FindAsync(id);
            if (tipoMenu == null)
            {
                return NotFound();
            }
            return View(tipoMenu);
        }

        // POST: TipoMenu/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTipoMenu,DscTipoMenu")] TipoMenu tipoMenu)
        {
            if (id != tipoMenu.IdTipoMenu)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoMenu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoMenuExists(tipoMenu.IdTipoMenu))
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
            return View(tipoMenu);
        }

        // GET: TipoMenu/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoMenu = await _context.TipoMenu
                .FirstOrDefaultAsync(m => m.IdTipoMenu == id);
            if (tipoMenu == null)
            {
                return NotFound();
            }

            return View(tipoMenu);
        }

        // POST: TipoMenu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipoMenu = await _context.TipoMenu.FindAsync(id);
            if (tipoMenu != null)
            {
                _context.TipoMenu.Remove(tipoMenu);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoMenuExists(int id)
        {
            return _context.TipoMenu.Any(e => e.IdTipoMenu == id);
        }
    }
}
