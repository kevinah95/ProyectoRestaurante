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
    public class ValoracionController : Controller
    {
        private readonly RestauranteDbContext _context;

        public ValoracionController(RestauranteDbContext context)
        {
            _context = context;
        }

        // GET: Valoracion
        public async Task<IActionResult> Index()
        {
            var restauranteDbContext = _context.Valoracion.Include(v => v.IdClienteNavigation).Include(v => v.IdMenuNavigation);
            return View(await restauranteDbContext.ToListAsync());
        }

        // GET: Valoracion/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var valoracion = await _context.Valoracion
                .Include(v => v.IdClienteNavigation)
                .Include(v => v.IdMenuNavigation)
                .FirstOrDefaultAsync(m => m.IdValoracion == id);
            if (valoracion == null)
            {
                return NotFound();
            }

            return View(valoracion);
        }

        // GET: Valoracion/Create
        public IActionResult Create()
        {
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "Ap1");
            ViewData["IdMenu"] = new SelectList(_context.Menu, "IdMenu", "DscMenu");
            return View();
        }

        // POST: Valoracion/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdValoracion,IdCliente,IdMenu,Comentario,Calificacion,Fecha")] Valoracion valoracion)
        {
            valoracion.IdMenuNavigation = await _context.Menu.FindAsync(valoracion.IdMenu);
            valoracion.IdClienteNavigation = await _context.Clientes.FindAsync(valoracion.IdCliente);
            if (ModelState.IsValid)
            {
                _context.Add(valoracion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "Ap1", valoracion.IdCliente);
            ViewData["IdMenu"] = new SelectList(_context.Menu, "IdMenu", "DscMenu", valoracion.IdMenu);
            return View(valoracion);
        }

        // GET: Valoracion/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var valoracion = await _context.Valoracion.FindAsync(id);
            if (valoracion == null)
            {
                return NotFound();
            }
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "Ap1", valoracion.IdCliente);
            ViewData["IdMenu"] = new SelectList(_context.Menu, "IdMenu", "DscMenu", valoracion.IdMenu);
            return View(valoracion);
        }

        // POST: Valoracion/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdValoracion,IdCliente,IdMenu,Comentario,Calificacion,Fecha")] Valoracion valoracion)
        {
            if (id != valoracion.IdValoracion)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(valoracion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ValoracionExists(valoracion.IdValoracion))
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
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "Ap1", valoracion.IdCliente);
            ViewData["IdMenu"] = new SelectList(_context.Menu, "IdMenu", "DscMenu", valoracion.IdMenu);
            return View(valoracion);
        }

        // GET: Valoracion/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var valoracion = await _context.Valoracion
                .Include(v => v.IdClienteNavigation)
                .Include(v => v.IdMenuNavigation)
                .FirstOrDefaultAsync(m => m.IdValoracion == id);
            if (valoracion == null)
            {
                return NotFound();
            }

            return View(valoracion);
        }

        // POST: Valoracion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var valoracion = await _context.Valoracion.FindAsync(id);
            if (valoracion != null)
            {
                _context.Valoracion.Remove(valoracion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ValoracionExists(int id)
        {
            return _context.Valoracion.Any(e => e.IdValoracion == id);
        }
    }
}
