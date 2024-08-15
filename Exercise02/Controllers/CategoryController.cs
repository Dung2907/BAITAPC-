using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Exercise02.Context;
using Exercise02.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exercise02.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly Exercise02Context _context;

        public CategoryController(Exercise02Context context)
        {
            _context = context;
        }

        // GET: api/Category
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            return await _context.Categories
                .Include(c => c.Parent)
                .Include(c => c.Children)
                .ToListAsync();
        }

        // GET: api/Category/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(Guid id) // Thay đổi kiểu id thành Guid
        {
            var category = await _context.Categories
                .Include(c => c.Parent)
                .Include(c => c.Children)
                .FirstOrDefaultAsync(c => c.Id == id); // Sửa đổi so sánh thành Guid

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        // POST: api/Category
        [HttpPost]
        public async Task<ActionResult<Category>> CreateCategory(Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, category);
        }

        // PUT: api/Category/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(Guid id, Category category) // Thay đổi kiểu id thành Guid
        {
            if (id != category.Id) // Sửa đổi so sánh thành Guid
            {
                return BadRequest();
            }

            _context.Entry(category).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id)) // Sửa đổi so sánh thành Guid
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

        // DELETE: api/Category/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id) // Thay đổi kiểu id thành Guid
        {
            var category = await _context.Categories.FindAsync(id); // Sửa đổi so sánh thành Guid
            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoryExists(Guid id) // Thay đổi kiểu id thành Guid
        {
            return _context.Categories.Any(e => e.Id == id); // Sửa đổi so sánh thành Guid
        }
    }
}
