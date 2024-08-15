using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Exercise02.Context;
using Exercise02.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Exercise02.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShippingRateController : ControllerBase
    {
        private readonly Exercise02Context _context;

        public ShippingRateController(Exercise02Context context)
        {
            _context = context;
        }

        // GET: api/ShippingRate
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShippingRate>>> GetShippingRates()
        {
            return await _context.ShippingRates.ToListAsync();
        }

        // GET: api/ShippingRate/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ShippingRate>> GetShippingRate(Guid id)
        {
            var shippingRate = await _context.ShippingRates.FindAsync(id);

            if (shippingRate == null)
            {
                return NotFound();
            }

            return shippingRate;
        }

        // POST: api/ShippingRate
        [HttpPost]
        public async Task<ActionResult<ShippingRate>> PostShippingRate(ShippingRate shippingRate)
        {
            _context.ShippingRates.Add(shippingRate);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetShippingRate), new { id = shippingRate.Id }, shippingRate);
        }

        // PUT: api/ShippingRate/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShippingRate(Guid id, ShippingRate shippingRate)
        {
            if (id != shippingRate.Id)
            {
                return BadRequest();
            }

            _context.Entry(shippingRate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShippingRateExists(id))
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

        // DELETE: api/ShippingRate/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShippingRate(Guid id)
        {
            var shippingRate = await _context.ShippingRates.FindAsync(id);
            if (shippingRate == null)
            {
                return NotFound();
            }

            _context.ShippingRates.Remove(shippingRate);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ShippingRateExists(Guid id)
        {
            return _context.ShippingRates.Any(e => e.Id == id);
        }
    }
}
