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
    public class ShippingZoneController : ControllerBase
    {
        private readonly Exercise02Context _context;

        public ShippingZoneController(Exercise02Context context)
        {
            _context = context;
        }

        // GET: api/ShippingZone
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShippingZone>>> GetShippingZones()
        {
            return await _context.ShippingZones.ToListAsync();
        }

        // GET: api/ShippingZone/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ShippingZone>> GetShippingZone(int id)
        {
            var shippingZone = await _context.ShippingZones.FindAsync(id);

            if (shippingZone == null)
            {
                return NotFound();
            }

            return shippingZone;
        }

        // POST: api/ShippingZone
        [HttpPost]
        public async Task<ActionResult<ShippingZone>> PostShippingZone(ShippingZone shippingZone)
        {
            _context.ShippingZones.Add(shippingZone);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetShippingZone), new { id = shippingZone.Id }, shippingZone);
        }

        // PUT: api/ShippingZone/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShippingZone(int id, ShippingZone shippingZone)
        {
            if (id != shippingZone.Id)
            {
                return BadRequest();
            }

            _context.Entry(shippingZone).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShippingZoneExists(id))
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

        // DELETE: api/ShippingZone/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShippingZone(int id)
        {
            var shippingZone = await _context.ShippingZones.FindAsync(id);
            if (shippingZone == null)
            {
                return NotFound();
            }

            _context.ShippingZones.Remove(shippingZone);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ShippingZoneExists(int id)
        {
            return _context.ShippingZones.Any(e => e.Id == id);
        }
    }
}
