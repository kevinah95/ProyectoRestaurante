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
    public class MenuController : ControllerBase
    {
        private readonly RestauranteDbContext _context;

        public MenuController(RestauranteDbContext context)
        {
            _context = context;
        }

        // GET: api/Menu
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Menu>>> GetMenu()
        {
            return await _context.Menu.ToListAsync();
        }

        // GET: api/Menu/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Menu>> GetMenu(int id)
        {
            var menu = await _context.Menu.FindAsync(id);

            if (menu == null)
            {
                return NotFound();
            }

            return menu;
        }

        // PUT: api/Menu/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMenu(int id, Menu menu)
        {
            if (id != menu.IdMenu)
            {
                return BadRequest();
            }

            _context.Entry(menu).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MenuExists(id))
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

        // POST: api/Menu
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Menu>> PostMenu(Menu menu)
        {
            _context.Menu.Add(menu);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MenuExists(menu.IdMenu))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetMenu", new { id = menu.IdMenu }, menu);
        }

        // DELETE: api/Menu/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenu(int id)
        {
            var menu = await _context.Menu.FindAsync(id);
            if (menu == null)
            {
                return NotFound();
            }

            _context.Menu.Remove(menu);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MenuExists(int id)
        {
            return _context.Menu.Any(e => e.IdMenu == id);
        }
    }
}
