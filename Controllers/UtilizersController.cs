using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinalAPI.Models;
using FinalAPI.Classes;
using NuGet.Protocol.Plugins;

namespace FinalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilizersController : ControllerBase
    {
        private readonly FinalDbContext _context;

        public UtilizersController(FinalDbContext context)
        {
            _context = context;
        }

        // GET: api/Utilizers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Utilizer>>> GetUtilizers()
        {
            if (_context.Utilizers == null)
            {
                return NotFound();
            }
            return await _context.Utilizers.ToListAsync();
        }

        // GET: api/Utilizers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Utilizer>> GetUtilizer(int id)
        {
            if (_context.Utilizers == null)
            {
                return NotFound();
            }
            var utilizer = await _context.Utilizers.FindAsync(id);

            if (utilizer == null)
            {
                return NotFound();
            }

            return utilizer;
        }

        // PUT: api/Utilizers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUtilizer(int id, Utilizer utilizer)
        {
            if (id != utilizer.Id)
            {
                return BadRequest();
            }

            _context.Entry(utilizer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UtilizerExists(id))
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

        // POST: api/Utilizers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Utilizer>> PostUtilizer(Utilizer utilizer)
        {
            if (_context.Utilizers == null)
            {
                return Problem("Entity set 'FinalDbContext.Utilizers'  is null.");
            }
            _context.Utilizers.Add(utilizer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUtilizer", new { id = utilizer.Id }, utilizer);
        }

        // DELETE: api/Utilizers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUtilizer(int id)
        {
            if (_context.Utilizers == null)
            {
                return NotFound();
            }
            var utilizer = await _context.Utilizers.FindAsync(id);
            if (utilizer == null)
            {
                return NotFound();
            }

            _context.Utilizers.Remove(utilizer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("name={Name}")]
        public async Task<ActionResult<IEnumerable<UtilizerService>>> GetUtilizerOrderServices(string name)
        {
            var OrderServices = _context.OrderServices
                .Include(x => x.StatusNavigation)
                .Include(x => x.Result)
                .Include(x => x.ServiceNavigation)
                .Where(x => x.UtilizerNavigation.Name == name && x.Status != 4)
                .ToList()
                .ConvertAll(x => new UtilizerService(x));

            return OrderServices;
        }

        private bool UtilizerExists(int id)
        {
            return (_context.Utilizers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
