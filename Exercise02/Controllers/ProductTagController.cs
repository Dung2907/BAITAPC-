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
    public class ProductTagsController : ControllerBase
    {
        private readonly Exercise02Context _context;

        public ProductTagsController(Exercise02Context context)
        {
            _context = context;
        }

        // GET: api/ProductTags
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductTag>>> GetProductTags()
        {
            var productTags = await _context.ProductTags
                .Include(pt => pt.Tag)
                .Include(pt => pt.Product)
                .ToListAsync();
            return Ok(productTags);
        }

        // GET: api/ProductTags/5
        [HttpGet("{tagId}/{productId}")]
        public async Task<ActionResult<ProductTag>> GetProductTag(Guid tagId, Guid productId)
        {
            if (tagId == Guid.Empty || productId == Guid.Empty)
            {
                return BadRequest("Invalid ID.");
            }

            var productTag = await _context.ProductTags
                .Include(pt => pt.Tag)
                .Include(pt => pt.Product)
                .FirstOrDefaultAsync(pt => pt.TagId == tagId && pt.ProductId == productId);

            if (productTag == null)
            {
                return NotFound();
            }

            return Ok(productTag);
        }

        // POST: api/ProductTags
        [HttpPost]
        public async Task<ActionResult<ProductTag>> CreateProductTag([FromBody] ProductTag productTag)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Ensure unique combination of TagId and ProductId
            if (await _context.ProductTags.AnyAsync(pt => pt.TagId == productTag.TagId && pt.ProductId == productTag.ProductId))
            {
                return Conflict("ProductTag already exists.");
            }

            _context.ProductTags.Add(productTag);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProductTag), new { tagId = productTag.TagId, productId = productTag.ProductId }, productTag);
        }

        // PUT: api/ProductTags/5
        [HttpPut("{tagId}/{productId}")]
        public async Task<IActionResult> UpdateProductTag(Guid tagId, Guid productId, [FromBody] ProductTag productTag)
        {
            if (tagId != productTag.TagId || productId != productTag.ProductId)
            {
                return BadRequest("ID mismatch.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _context.Entry(productTag).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductTagExists(tagId, productId))
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

        // DELETE: api/ProductTags/5
        [HttpDelete("{tagId}/{productId}")]
        public async Task<IActionResult> DeleteProductTag(Guid tagId, Guid productId)
        {
            var productTag = await _context.ProductTags
                .FindAsync(tagId, productId);

            if (productTag == null)
            {
                return NotFound();
            }

            _context.ProductTags.Remove(productTag);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductTagExists(Guid tagId, Guid productId)
        {
            return _context.ProductTags.Any(e => e.TagId == tagId && e.ProductId == productId);
        }
    }
}
