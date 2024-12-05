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
    public class ReservacionController : Controller
    {
        private readonly RestauranteDbContext _context;

        public ReservacionController(RestauranteDbContext context)
        {
            _context = context;
        }

        // GET: Reservacion
        public async Task<IActionResult> Index()
        {
            var restauranteDbContext = _context.Reservacion.Include(r => r.IdClienteNavigation).Include(r => r.IdMenuNavigation).Include(r => r.IdMesaNavigation);
            return View(await restauranteDbContext.ToListAsync());
        }

        // GET: Reservacion/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservacion = await _context.Reservacion
                .Include(r => r.IdClienteNavigation)
                .Include(r => r.IdMenuNavigation)
                .Include(r => r.IdMesaNavigation)
                .FirstOrDefaultAsync(m => m.NumReservacion == id);
            if (reservacion == null)
            {
                return NotFound();
            }

            return View(reservacion);
        }

        // GET: Reservacion/Create
        public IActionResult Create()
        {
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "Ap1");
            ViewData["IdMenu"] = new SelectList(_context.Menu, "IdMenu", "DscMenu");
            ViewData["IdMesa"] = new SelectList(_context.Mesas, "IdMesa", "DscMesa");
            return View();
        }

        // POST: Reservacion/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NumReservacion,IdCliente,IdMesa,IdMenu,Cantidad,FecReserva")] Reservacion reservacion)
        {
            reservacion.IdMenuNavigation = await _context.Menu.FindAsync(reservacion.IdMenu);
            reservacion.IdClienteNavigation = await _context.Clientes.FindAsync(reservacion.IdCliente);
            reservacion.IdMesaNavigation = await _context.Mesas.FindAsync(reservacion.IdMesa);
            if (ModelState.IsValid)
            {
                _context.Add(reservacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "Ap1", reservacion.IdCliente);
            ViewData["IdMenu"] = new SelectList(_context.Menu, "IdMenu", "DscMenu", reservacion.IdMenu);
            ViewData["IdMesa"] = new SelectList(_context.Mesas, "IdMesa", "DscMesa", reservacion.IdMesa);
            return View(reservacion);
        }

        // GET: Reservacion/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservacion = await _context.Reservacion.FindAsync(id);
            if (reservacion == null)
            {
                return NotFound();
            }
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "Ap1", reservacion.IdCliente);
            ViewData["IdMenu"] = new SelectList(_context.Menu, "IdMenu", "DscMenu", reservacion.IdMenu);
            ViewData["IdMesa"] = new SelectList(_context.Mesas, "IdMesa", "DscMesa", reservacion.IdMesa);
            return View(reservacion);
        }

        // POST: Reservacion/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NumReservacion,IdCliente,IdMesa,IdMenu,Cantidad,FecReserva")] Reservacion reservacion)
        {
            if (id != reservacion.NumReservacion)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservacionExists(reservacion.NumReservacion))
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
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "Ap1", reservacion.IdCliente);
            ViewData["IdMenu"] = new SelectList(_context.Menu, "IdMenu", "DscMenu", reservacion.IdMenu);
            ViewData["IdMesa"] = new SelectList(_context.Mesas, "IdMesa", "DscMesa", reservacion.IdMesa);
            return View(reservacion);
        }

        // GET: Reservacion/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservacion = await _context.Reservacion
                .Include(r => r.IdClienteNavigation)
                .Include(r => r.IdMenuNavigation)
                .Include(r => r.IdMesaNavigation)
                .FirstOrDefaultAsync(m => m.NumReservacion == id);
            if (reservacion == null)
            {
                return NotFound();
            }

            return View(reservacion);
        }

        // POST: Reservacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservacion = await _context.Reservacion.FindAsync(id);
            if (reservacion != null)
            {
                _context.Reservacion.Remove(reservacion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservacionExists(int id)
        {
            return _context.Reservacion.Any(e => e.NumReservacion == id);
        }
    }
}
