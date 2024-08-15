using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Exercise01.Context;
using Exercise01.Models;
using Exercise01.InputModels;
using System.Threading.Tasks;
using System;

namespace Exercise01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly Exercise01Context _context;

        public CategoryController(Exercise01Context context)
        {
            _context = context;
        }

        // GET: api/categories
        [HttpGet]
        public ActionResult<IEnumerable<Category>> GetCategories(int page = 1, int pageSize = 10)
        {
            var categories = _context.Categories
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return categories;
        }

        // GET: api/categories/{categoryId}
        [HttpGet("{categoryId}")]
        public ActionResult<Category> GetCategory(int categoryId)
        {
            var category = _context.Categories.Find(categoryId);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        // POST: api/categories
        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] CategoryInputModel categoryInput)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (categoryInput.SubCategoryId != null)
                    {
                        var existingSubCategoryId = await _context.Categories.FindAsync(categoryInput.SubCategoryId);
                        if (existingSubCategoryId == null)
                        {
                            return NotFound(new { message = $"SubCategoryId with ID {categoryInput.SubCategoryId} not found." });
                        }
                    }

                    var newCategory = new Category
                    {
                        SubCategoryId = categoryInput.SubCategoryId,
                        CategoryTitle = categoryInput.CategoryTitle,
                        ImageUrl = categoryInput.ImageUrl
                    };

                    _context.Categories.Add(newCategory);
                    await _context.SaveChangesAsync();

                    return Ok(newCategory);
                }

                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        // PUT: api/categories/{categoryId}
        [HttpPut("{categoryId}")]
        public async Task<IActionResult> PutCategory(int categoryId, [FromBody] CategoryInputModel categoryInput)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var existingCategory = await _context.Categories.FindAsync(categoryId);

                    if (existingCategory == null)
                    {
                        return NotFound();
                    }

                    // Cập nhật thông tin của đối tượng Category từ dữ liệu nhập
                    existingCategory.SubCategoryId = categoryInput.SubCategoryId;
                    existingCategory.CategoryTitle = categoryInput.CategoryTitle;
                    existingCategory.ImageUrl = categoryInput.ImageUrl;

                    _context.Entry(existingCategory).State = EntityState.Modified;

                    await _context.SaveChangesAsync();
                    return Ok(existingCategory);
                }

                return BadRequest(ModelState);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Categories.AnyAsync(c => c.CategoryId == categoryId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        // DELETE: api/categories/{categoryId}
        [HttpDelete("{categoryId}")]
        public async Task<ActionResult<Category>> DeleteCategory(int categoryId)
        {
            try
            {
                var category = await _context.Categories.FindAsync(categoryId);

                if (category == null)
                {
                    return NotFound();
                }

                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();

                return Ok(new { message = $"Category with ID {categoryId} has been successfully deleted." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
