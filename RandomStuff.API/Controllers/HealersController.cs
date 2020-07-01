using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RandomStuff.Lib.Model;
using RandomStuff.Lib.Services;

namespace RandomStuff.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealersController : ControllerBase
    {
        private readonly MedicDbContext _context;

        public HealersController(MedicDbContext context)
        {
            _context = context;
        }

        // GET: api/Healers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Healer>>> GetHealers()
        {
            return await _context.Healers.ToListAsync();
        }

        // GET: api/Healers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Healer>> GetHealer(int id)
        {
            var healer = await _context.Healers.FindAsync(id);

            if (healer == null)
            {
                return NotFound();
            }

            return healer;
        }

        // PUT: api/Healers/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHealer(int id, Healer healer)
        {
            if (id != healer.Id)
            {
                return BadRequest();
            }

            _context.Entry(healer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HealerExists(id))
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

        // POST: api/Healers
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Healer>> PostHealer(Healer healer)
        {
            _context.Healers.Add(healer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHealer", new { id = healer.Id }, healer);
        }

        // DELETE: api/Healers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Healer>> DeleteHealer(int id)
        {
            var healer = await _context.Healers.FindAsync(id);
            if (healer == null)
            {
                return NotFound();
            }

            _context.Healers.Remove(healer);
            await _context.SaveChangesAsync();

            return healer;
        }

        private bool HealerExists(int id)
        {
            return _context.Healers.Any(e => e.Id == id);
        }
    }
}
