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
    public class ProductAttributesController : ControllerBase
    {
        private readonly Exercise02Context _context;

        public ProductAttributesController(Exercise02Context context)
        {
            _context = context;
        }

        // GET: api/ProductAttributes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductAttribute>>> GetProductAttributes()
        {
            var productAttributes = await _context.ProductAttributes
                .Include(p => p.Product)
                .Include(p => p.Attribute)
                .ToListAsync();
            return Ok(productAttributes);
        }

        // GET: api/ProductAttributes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductAttribute>> GetProductAttribute(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid ID.");
            }

            var productAttribute = await _context.ProductAttributes
                .Include(p => p.Product)
                .Include(p => p.Attribute)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (productAttribute == null)
            {
                return NotFound();
            }

            return Ok(productAttribute);
        }

        // POST: api/ProductAttributes
        [HttpPost]
        public async Task<ActionResult<ProductAttribute>> CreateProductAttribute([FromBody] ProductAttribute productAttribute)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            productAttribute.Id = Guid.NewGuid(); // Ensure that the productAttribute has a unique Id
            _context.ProductAttributes.Add(productAttribute);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProductAttribute), new { id = productAttribute.Id }, productAttribute);
        }

        // PUT: api/ProductAttributes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProductAttribute(Guid id, [FromBody] ProductAttribute productAttribute)
        {
            if (id != productAttribute.Id)
            {
                return BadRequest("ID mismatch.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _context.Entry(productAttribute).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductAttributeExists(id))
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

        // DELETE: api/ProductAttributes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductAttribute(Guid id)
        {
            var productAttribute = await _context.ProductAttributes.FindAsync(id);
            if (productAttribute == null)
            {
                return NotFound();
            }

            _context.ProductAttributes.Remove(productAttribute);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductAttributeExists(Guid id)
        {
            return _context.ProductAttributes.Any(e => e.Id == id);
        }
    }
}
