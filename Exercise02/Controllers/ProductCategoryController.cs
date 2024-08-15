using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Exercise02.Context;
using Exercise02.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exercise02.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductCategoryController : ControllerBase
    {
        private readonly Exercise02Context _context;

        public ProductCategoryController(Exercise02Context context)
        {
            _context = context;
        }

        // GET: api/ProductCategory
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductCategory>>> GetProductCategories()
        {
            return await _context.ProductCategories
                .Include(pc => pc.Product)
                .Include(pc => pc.Category)
                .ToListAsync();
        }

        // GET: api/ProductCategory/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductCategory>> GetProductCategory(Guid id)
        {
            var productCategory = await _context.ProductCategories
                .Include(pc => pc.Product)
                .Include(pc => pc.Category)
                .FirstOrDefaultAsync(pc => pc.Id == id);

            if (productCategory == null)
            {
                return NotFound();
            }

            return Ok(productCategory);
        }

        // POST: api/ProductCategory
        [HttpPost]
        public async Task<ActionResult<ProductCategory>> PostProductCategory(ProductCategory productCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ProductCategories.Add(productCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProductCategory), new { id = productCategory.Id }, productCategory);
        }

        // PUT: api/ProductCategory/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductCategory(Guid id, ProductCategory productCategory)
        {
            if (id != productCategory.Id)
            {
                return BadRequest();
            }

            _context.Entry(productCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductCategoryExists(id))
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

        // DELETE: api/ProductCategory/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductCategory(Guid id)
        {
            var productCategory = await _context.ProductCategories.FindAsync(id);
            if (productCategory == null)
            {
                return NotFound();
            }

            _context.ProductCategories.Remove(productCategory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductCategoryExists(Guid id)
        {
            return _context.ProductCategories.Any(e => e.Id == id);
        }
    }
}
