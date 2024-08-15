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
    public class VariantController : ControllerBase
    {
        private readonly Exercise02Context _context;

        public VariantController(Exercise02Context context)
        {
            _context = context;
        }

        // GET: api/Variant
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Variant>>> GetVariants()
        {
            return await _context.Variants.ToListAsync();
        }

        // GET: api/Variant/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Variant>> GetVariant(Guid id)
        {
            var variant = await _context.Variants.FindAsync(id);

            if (variant == null)
            {
                return NotFound();
            }

            return variant;
        }

        // POST: api/Variant
        [HttpPost]
        public async Task<ActionResult<Variant>> PostVariant(Variant variant)
        {
            _context.Variants.Add(variant);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetVariant), new { id = variant.Id }, variant);
        }

        // PUT: api/Variant/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVariant(Guid id, Variant variant)
        {
            if (id != variant.Id)
            {
                return BadRequest();
            }

            _context.Entry(variant).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VariantExists(id))
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

        // DELETE: api/Variant/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVariant(Guid id)
        {
            var variant = await _context.Variants.FindAsync(id);
            if (variant == null)
            {
                return NotFound();
            }

            _context.Variants.Remove(variant);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VariantExists(Guid id)
        {
            return _context.Variants.Any(e => e.Id == id);
        }
    }
}
