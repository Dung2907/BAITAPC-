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
    public class ProductAttributeValuesController : ControllerBase
    {
        private readonly Exercise02Context _context;

        public ProductAttributeValuesController(Exercise02Context context)
        {
            _context = context;
        }

        // GET: api/ProductAttributeValues
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductAttributeValue>>> GetProductAttributeValues()
        {
            var productAttributeValues = await _context.ProductAttributeValues
                .Include(p => p.ProductAttribute)
                .Include(p => p.AttributeValue)
                .ToListAsync();
            return Ok(productAttributeValues);
        }

        // GET: api/ProductAttributeValues/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductAttributeValue>> GetProductAttributeValue(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid ID.");
            }

            var productAttributeValue = await _context.ProductAttributeValues
                .Include(p => p.ProductAttribute)
                .Include(p => p.AttributeValue)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (productAttributeValue == null)
            {
                return NotFound();
            }

            return Ok(productAttributeValue);
        }

        // POST: api/ProductAttributeValues
        [HttpPost]
        public async Task<ActionResult<ProductAttributeValue>> CreateProductAttributeValue([FromBody] ProductAttributeValue productAttributeValue)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            productAttributeValue.Id = Guid.NewGuid(); // Ensure that the productAttributeValue has a unique Id
            _context.ProductAttributeValues.Add(productAttributeValue);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProductAttributeValue), new { id = productAttributeValue.Id }, productAttributeValue);
        }

        // PUT: api/ProductAttributeValues/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProductAttributeValue(Guid id, [FromBody] ProductAttributeValue productAttributeValue)
        {
            if (id != productAttributeValue.Id)
            {
                return BadRequest("ID mismatch.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _context.Entry(productAttributeValue).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductAttributeValueExists(id))
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

        // DELETE: api/ProductAttributeValues/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductAttributeValue(Guid id)
        {
            var productAttributeValue = await _context.ProductAttributeValues.FindAsync(id);
            if (productAttributeValue == null)
            {
                return NotFound();
            }

            _context.ProductAttributeValues.Remove(productAttributeValue);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductAttributeValueExists(Guid id)
        {
            return _context.ProductAttributeValues.Any(e => e.Id == id);
        }
    }
}
