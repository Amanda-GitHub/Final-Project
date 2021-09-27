using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api.Data;
using Projeto_CLOUD_45_2021.Models;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EncomendasController : ControllerBase
    {
        private readonly DataBaseContext _context;

        public EncomendasController(DataBaseContext context)
        {
            _context = context;
        }

        // GET: api/Encomendas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Encomenda>>> GetEncomendas()
        {
            var encomendas = _context.Encomendas.Include(e => e.Produto).Include(e => e.Utilizador);
            return await encomendas.ToListAsync();
        }

        // GET: api/Encomendas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Encomenda>> GetEncomenda(int id)
        {
            var encomenda = await _context.Encomendas.Include(e => e.Produto).Include(e => e.Utilizador).FirstOrDefaultAsync(m => m.EncomendaId == id);

            if (encomenda == null)
            {
                return NotFound();
            }

            return encomenda;
        }

        // PUT: api/Encomendas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEncomenda(int id, Encomenda encomenda)
        {
            if (id != encomenda.EncomendaId)
            {
                return BadRequest();
            }

            _context.Entry(encomenda).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EncomendaExists(id))
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

        // POST: api/Encomendas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Encomenda>> PostEncomenda(Encomenda encomenda)
        {
            _context.Encomendas.Add(encomenda);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEncomenda", new { id = encomenda.EncomendaId }, encomenda);
        }

        // DELETE: api/Encomendas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEncomenda(int id)
        {
            var encomenda = await _context.Encomendas.FindAsync(id);
            if (encomenda == null)
            {
                return NotFound();
            }

            _context.Encomendas.Remove(encomenda);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EncomendaExists(int id)
        {
            return _context.Encomendas.Any(e => e.EncomendaId == id);
        }
    }
}
