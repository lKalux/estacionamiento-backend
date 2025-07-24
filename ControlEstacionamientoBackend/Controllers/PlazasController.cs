using ControlEstacionamientoBackend.Data;
using ControlEstacionamientoBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ControlEstacionamientoBackend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PlazasController : ControllerBase
    {
        private readonly EstacionamientoContext _context;

        public PlazasController(EstacionamientoContext
            context)
        {
            _context = context;
        }

        // GET: api/Plazas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Plaza>>> GetPlazas(string? filtro)
        {
            var query = _context.Plazas.Include(p => p.Cliente).AsQueryable();
            if (!string.IsNullOrEmpty(filtro))
            {
                query = query.Where(p => p.NumeroPlaza.Contains(filtro) || p.Estado.Contains(filtro));
            }
            return await query.ToListAsync();
        }

        // GET: api/Plazas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Plaza>> GetPlaza(int id)
        {
            var plaza = await _context.Plazas.Include(p => p.Cliente).FirstOrDefaultAsync(p => p.Id == id);
            if (plaza == null)
            {
                return NotFound();
            }
            return plaza;
        }

        // POST: api/Plazas
        [HttpPost]
        public async Task<ActionResult<Plaza>> PostPlaza(Plaza plaza)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Plazas.Add(plaza);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPlaza), new { id = plaza.Id }, plaza);
        }

        // PUT: api/Plazas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlaza(int id, Plaza plaza)
        {
            if (id != plaza.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Entry(plaza).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Plazas.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                throw;
            }
            return NoContent();
        }

        // DELETE: api/Plazas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlaza(int id)
        {
            var plaza = await _context.Plazas.FindAsync(id);
            if (plaza == null)
            {
                return NotFound();
            }
            _context.Plazas.Remove(plaza);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // POST: api/Plazas/5/Revocar
        [HttpPost("{id}/Revocar")]
        public async Task<IActionResult> RevocarPlaza(int id)
        {
            var plaza = await _context.Plazas.FindAsync(id);
            if (plaza == null)
            {
                return NotFound();
            }
            plaza.ClienteId = null;
            plaza.Estado = "Libre";
            plaza.FechaRevocacion = DateTime.Now;
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
