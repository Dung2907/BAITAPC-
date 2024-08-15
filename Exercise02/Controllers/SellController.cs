using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Exercise02.Context;
using Exercise02.Models;

namespace Exercise02.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellsController : ControllerBase
    {
        private readonly Exercise02Context _context;

        public SellsController(Exercise02Context context)
        {
            _context = context;
        }

        // GET: api/Sells
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sell>>> GetSells()
        {
            var sells = await _context.Sells
                .Include(s => s.Product)
                .ToListAsync();
            return Ok(sells);
        }

        // GET: api/Sells/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sell>> GetSell(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid ID.");
            }

            var sell = await _context.Sells
                .Include(s => s.Product)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (sell == null)
            {
                return NotFound();
            }

            return Ok(sell);
        }

        // POST: api/Sells
        [HttpPost]
        public async Task<ActionResult<Sell>> CreateSell([FromBody] Sell sell)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Sells.Add(sell);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSell), new { id = sell.Id }, sell);
        }

        // PUT: api/Sells/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSell(int id, [FromBody] Sell sell)
        {
            if (id != sell.Id)
            {
                return BadRequest("ID mismatch.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _context.Entry(sell).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SellExists(id))
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

        // DELETE: api/Sells/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSell(int id)
        {
            var sell = await _context.Sells.FindAsync(id);

            if (sell == null)
            {
                return NotFound();
            }

            _context.Sells.Remove(sell);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SellExists(int id)
        {
            return _context.Sells.Any(e => e.Id == id);
        }
    }
}
