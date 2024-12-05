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
    public class MesasController : ControllerBase
    {
        private readonly RestauranteDbContext _context;

        public MesasController(RestauranteDbContext context)
        {
            _context = context;
        }

        // GET: api/Mesas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mesas>>> GetMesas()
        {
            return await _context.Mesas.ToListAsync();
        }

        // GET: api/Mesas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Mesas>> GetMesas(int id)
        {
            var mesas = await _context.Mesas.FindAsync(id);

            if (mesas == null)
            {
                return NotFound();
            }

            return mesas;
        }

        // PUT: api/Mesas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMesas(int id, Mesas mesas)
        {
            if (id != mesas.IdMesa)
            {
                return BadRequest();
            }

            _context.Entry(mesas).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MesasExists(id))
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

        // POST: api/Mesas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Mesas>> PostMesas(Mesas mesas)
        {
            _context.Mesas.Add(mesas);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MesasExists(mesas.IdMesa))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetMesas", new { id = mesas.IdMesa }, mesas);
        }

        // DELETE: api/Mesas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMesas(int id)
        {
            var mesas = await _context.Mesas.FindAsync(id);
            if (mesas == null)
            {
                return NotFound();
            }

            _context.Mesas.Remove(mesas);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MesasExists(int id)
        {
            return _context.Mesas.Any(e => e.IdMesa == id);
        }
    }
}
