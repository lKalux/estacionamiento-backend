using ControlEstacionamientoBackend.Data;
using ControlEstacionamientoBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ControlEstacionamientoBackend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {

        private readonly EstacionamientoContext _context;

        public ClientesController(EstacionamientoContext context)
        {
            _context = context;
        }


        // GET: api/<ClientesController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes(string? filtro)
        {
            var query = _context.Clientes.AsQueryable();
            if (!string.IsNullOrEmpty(filtro))
            {
                query = query.Where(c => c.Nombre.Contains(filtro) || c.Apellido.Contains(filtro) || c.Email.Contains(filtro));
            }
            return await query.ToListAsync();
        }

        // GET api/<ClientesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null) { 
                return NotFound();
            }
            return cliente;
        }

        // POST api/<ClientesController>
        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente(Cliente cliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCliente), new { id = cliente.Id }, cliente);
        }

        // PUT api/<ClientesController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutCliente(int id, Cliente cliente) 
        {
            if(id != cliente.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Entry(cliente).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) { 
                if(!_context.Clientes.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                throw;
            }
            return NoContent();
        }

        // DELETE api/<ClientesController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCliente(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
