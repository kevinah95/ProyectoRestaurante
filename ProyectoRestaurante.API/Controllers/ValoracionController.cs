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
    public class ValoracionController : ControllerBase
    {
        private readonly RestauranteDbContext _context;

        public ValoracionController(RestauranteDbContext context)
        {
            _context = context;
        }

        // GET: api/Valoracion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Valoracion>>> GetValoracion()
        {
            return await _context.Valoracion.ToListAsync();
        }

        // GET: api/Valoracion/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Valoracion>> GetValoracion(int id)
        {
            var valoracion = await _context.Valoracion.FindAsync(id);

            if (valoracion == null)
            {
                return NotFound();
            }

            return valoracion;
        }

        // PUT: api/Valoracion/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutValoracion(int id, Valoracion valoracion)
        {
            if (id != valoracion.IdValoracion)
            {
                return BadRequest();
            }

            _context.Entry(valoracion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ValoracionExists(id))
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

        // POST: api/Valoracion
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Valoracion>> PostValoracion(Valoracion valoracion)
        {
            _context.Valoracion.Add(valoracion);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ValoracionExists(valoracion.IdValoracion))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetValoracion", new { id = valoracion.IdValoracion }, valoracion);
        }

        // DELETE: api/Valoracion/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteValoracion(int id)
        {
            var valoracion = await _context.Valoracion.FindAsync(id);
            if (valoracion == null)
            {
                return NotFound();
            }

            _context.Valoracion.Remove(valoracion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ValoracionExists(int id)
        {
            return _context.Valoracion.Any(e => e.IdValoracion == id);
        }
    }
}
