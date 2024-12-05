using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoRestaurante.DB.Context;
using ProyectoRestaurante.DB.Entities;

namespace ProyectoRestaurante.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoMenuController : ControllerBase
    {
        private readonly RestauranteDbContext _context;

        public TipoMenuController(RestauranteDbContext context)
        {
            _context = context;
        }

        // GET: api/TipoMenu
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoMenu>>> GetTipoMenu()
        {
            return await _context.TipoMenu.ToListAsync();
        }

        // GET: api/TipoMenu/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoMenu>> GetTipoMenu(int id)
        {
            var tipoMenu = await _context.TipoMenu.FindAsync(id);

            if (tipoMenu == null)
            {
                return NotFound();
            }

            return tipoMenu;
        }

        // PUT: api/TipoMenu/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoMenu(int id, TipoMenu tipoMenu)
        {
            if (id != tipoMenu.IdTipoMenu)
            {
                return BadRequest();
            }

            _context.Entry(tipoMenu).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoMenuExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TipoMenu
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TipoMenu>> PostTipoMenu(TipoMenu tipoMenu)
        {
            _context.TipoMenu.Add(tipoMenu);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TipoMenuExists(tipoMenu.IdTipoMenu))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTipoMenu", new { id = tipoMenu.IdTipoMenu }, tipoMenu);
        }

        // DELETE: api/TipoMenu/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoMenu(int id)
        {
            var tipoMenu = await _context.TipoMenu.FindAsync(id);
            if (tipoMenu == null)
            {
                return NotFound();
            }

            _context.TipoMenu.Remove(tipoMenu);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TipoMenuExists(int id)
        {
            return _context.TipoMenu.Any(e => e.IdTipoMenu == id);
        }
    }
}
