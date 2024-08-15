using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Exercise02.Context;
using Exercise02.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exercise02.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VariantValueController : ControllerBase
    {
        private readonly Exercise02Context _context;

        public VariantValueController(Exercise02Context context)
        {
            _context = context;
        }

        // GET: api/VariantValue
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VariantValue>>> GetVariantValues()
        {
            return await _context.VariantValues.ToListAsync();
        }

        // GET: api/VariantValue/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VariantValue>> GetVariantValue(Guid id)
        {
            var variantValue = await _context.VariantValues.FindAsync(id);

            if (variantValue == null)
            {
                return NotFound();
            }

            return variantValue;
        }

        // POST: api/VariantValue
        [HttpPost]
        public async Task<ActionResult<VariantValue>> PostVariantValue(VariantValue variantValue)
        {
            _context.VariantValues.Add(variantValue);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetVariantValue), new { id = variantValue.Id }, variantValue);
        }

        // PUT: api/VariantValue/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVariantValue(Guid id, VariantValue variantValue)
        {
            if (id != variantValue.Id)
            {
                return BadRequest();
            }

            _context.Entry(variantValue).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VariantValueExists(id))
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

        // DELETE: api/VariantValue/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVariantValue(Guid id)
        {
            var variantValue = await _context.VariantValues.FindAsync(id);
            if (variantValue == null)
            {
                return NotFound();
            }

            _context.VariantValues.Remove(variantValue);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VariantValueExists(Guid id)
        {
            return _context.VariantValues.Any(e => e.Id == id);
        }
    }
}
