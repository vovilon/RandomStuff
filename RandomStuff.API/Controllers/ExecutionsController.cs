using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RandomStuff.Lib.Model;
using RandomStuff.Lib.Services;

namespace RandomStuff.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExecutionsController : ControllerBase
    {
        private readonly MedicDbContext _context;

        public ExecutionsController(MedicDbContext context)
        {
            _context = context;
        }

        // GET: api/Executions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Execution>>> GetExecutions()
        {
            return await _context.Executions.ToListAsync();
        }
               

        // GET: api/Executions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Execution>> GetExecution(int id)
        {
            var execution = await _context.Executions.FindAsync(id);

            if (execution == null)
            {
                return NotFound();
            }

            return execution;
        }

        // PUT: api/Executions/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExecution(int id, Execution execution)
        {
            if (id != execution.Id)
            {
                return BadRequest();
            }

            _context.Entry(execution).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExecutionExists(id))
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

        // POST: api/Executions
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Execution>> PostExecution(Execution execution)
        {
            if (IsBusy(execution.HealerId, execution.ExecutionTime))
                return BadRequest();

            _context.Executions.Add(execution);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExecution", new { id = execution.Id }, execution);
        }

        // DELETE: api/Executions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Execution>> DeleteExecution(int id)
        {
            var execution = await _context.Executions.FindAsync(id);
            if (execution == null)
            {
                return NotFound();
            }

            _context.Executions.Remove(execution);
            await _context.SaveChangesAsync();

            return execution;
        }

        // GET: api/Executions
        [HttpGet("date")]
        public async Task<ActionResult<IEnumerable<Execution>>> GetExecutions([System.Web.Http.FromUri] DateTime date)
        {
            return await _context
                        .Executions
                        .Where(e => e.ExecutionTime.Date == date.Date)
                        .ToListAsync();
        }

        // GET: api/Executions
        [HttpPost("move")]
        public async Task<ActionResult<Execution>> MoveExecutionTime([System.Web.Http.FromUri] int id, [System.Web.Http.FromUri] DateTime date)
        {
            var execution = await _context.Executions.FindAsync(id);
            if (execution == null)
            {
                return NotFound();
            }

            execution.ExecutionTime = date;
            await _context.SaveChangesAsync();

            return execution;
        }

        // GET: api/Executions
        [HttpPost("signup")]
        public async Task<ActionResult<Execution>> PostExecution([System.Web.Http.FromUri] int healer, [System.Web.Http.FromUri] int victim, [System.Web.Http.FromUri] DateTime date)
        {
            if (IsBusy(healer, date))
                return BadRequest();

            var execution = new Execution 
            {
                HealerId = healer,
                VictimId = victim,
                ExecutionTime = date
            };

            _context.Executions.Add(execution);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExecution", new { id = execution.Id }, execution);
        }

        private bool IsBusy(int healerId, DateTime date)
        {
            return _context.Executions.Any(e => e.HealerId == healerId && e.ExecutionTime.Date == date.Date && e.ExecutionTime.Hour == date.Hour);
        }

        private bool ExecutionExists(int id)
        {
            return _context.Executions.Any(e => e.Id == id);
        }
    }
}
