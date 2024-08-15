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
    public class VariantOptionController : ControllerBase
    {
        private readonly Exercise02Context _context;

        public VariantOptionController(Exercise02Context context)
        {
            _context = context;
        }

        // GET: api/VariantOption
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VariantOption>>> GetVariantOptions()
        {
            return await _context.VariantOptions.ToListAsync();
        }

        // GET: api/VariantOption/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VariantOption>> GetVariantOption(Guid id)
        {
            var variantOption = await _context.VariantOptions.FindAsync(id);

            if (variantOption == null)
            {
                return NotFound();
            }

            return variantOption;
        }

        // POST: api/VariantOption
        [HttpPost]
        public async Task<ActionResult<VariantOption>> PostVariantOption(VariantOption variantOption)
        {
            _context.VariantOptions.Add(variantOption);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetVariantOption), new { id = variantOption.Id }, variantOption);
        }

        // PUT: api/VariantOption/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVariantOption(Guid id, VariantOption variantOption)
        {
            if (id != variantOption.Id)
            {
                return BadRequest();
            }

            _context.Entry(variantOption).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VariantOptionExists(id))
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

        // DELETE: api/VariantOption/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVariantOption(Guid id)
        {
            var variantOption = await _context.VariantOptions.FindAsync(id);
            if (variantOption == null)
            {
                return NotFound();
            }

            _context.VariantOptions.Remove(variantOption);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VariantOptionExists(Guid id)
        {
            return _context.VariantOptions.Any(e => e.Id == id);
        }
    }
}
