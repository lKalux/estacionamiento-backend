using ControlEstacionamientoBackend.Data;
using ControlEstacionamientoBackend.DTOs;
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
    public class AutosController : ControllerBase
    {
        private readonly EstacionamientoContext _context;

        public AutosController(EstacionamientoContext context)
        {
            _context = context;
        }

        // GET: api/Autos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Auto>>> GetAutos(string? filtro)
        {
            var query = _context.Autos.Include(a => a.Cliente).AsQueryable();
            if (!string.IsNullOrEmpty(filtro))
            {
                query = query.Where(a => a.Marca.Contains(filtro) || a.Modelo.Contains(filtro) || a.Patente.Contains(filtro));
            }
            return await query.ToListAsync();
        }

        // GET: api/Autos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Auto>> GetAuto(int id)
        {
            var auto = await _context.Autos.Include(a => a.Cliente).FirstOrDefaultAsync(a => a.Id == id);
            if (auto == null)
            {
                return NotFound();
            }
            return auto;
        }

        [HttpPost]
        public async Task<ActionResult<Auto>> PostAuto(AutoInputDTO autoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cliente = await _context.Clientes.FindAsync(autoDto.ClienteId);
            if (cliente == null)
            {
                return BadRequest(new { error = "Cliente no encontrado con el ID proporcionado." });
            }

            var auto = new Auto
            {
                ClienteId = autoDto.ClienteId,
                Marca = autoDto.Marca,
                Modelo = autoDto.Modelo,
                Patente = autoDto.Patente,
                Anio = autoDto.Anio
            };

            _context.Autos.Add(auto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAuto), new { id = auto.Id }, auto);
        }

        // PUT: api/Autos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuto(int id, AutoInputDTO autoDto)
        {
            if (id != autoDto.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var auto = await _context.Autos.Include(a => a.Cliente).FirstOrDefaultAsync(a => a.Id == id);
            if (auto == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes.FindAsync(autoDto.ClienteId);
            if (cliente == null)
            {
                return BadRequest(new { error = "Cliente no encontrado con el ID proporcionado." });
            }

            auto.ClienteId = autoDto.ClienteId;
            auto.Marca = autoDto.Marca;
            auto.Modelo = autoDto.Modelo;
            auto.Patente = autoDto.Patente;
            auto.Anio = autoDto.Anio;

            _context.Entry(auto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Autos.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/Autos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuto(int id)
        {
            var auto = await _context.Autos.FindAsync(id);
            if (auto == null)
            {
                return NotFound();
            }
            _context.Autos.Remove(auto);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
