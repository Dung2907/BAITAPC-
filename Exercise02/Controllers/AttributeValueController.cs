using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Exercise02.Context;
using Exercise02.Models;

namespace Exercise02.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttributeValuesController : ControllerBase // Sử dụng ControllerBase thay vì Controller cho API
    {
        private readonly Exercise02Context _context;

        public AttributeValuesController(Exercise02Context context)
        {
            _context = context;
        }

        // GET: api/AttributeValues
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AttributeValue>>> Index()
        {
            var attributeValues = await _context.AttributeValues.Include(a => a.Attribute).ToListAsync();
            return Ok(attributeValues);
        }

        // GET: api/AttributeValues/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AttributeValue>> Details(Guid id)
        {
            var attributeValue = await _context.AttributeValues
                .Include(a => a.Attribute)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (attributeValue == null)
            {
                return NotFound();
            }

            return Ok(attributeValue);
        }

        // POST: api/AttributeValues
        [HttpPost]
        public async Task<ActionResult<AttributeValue>> Create(AttributeValue attributeValue)
        {
            if (ModelState.IsValid)
            {
                attributeValue.Id = Guid.NewGuid(); // Tạo Id mới
                _context.Add(attributeValue);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(Details), new { id = attributeValue.Id }, attributeValue);
            }
            return BadRequest(ModelState);
        }

        // PUT: api/AttributeValues/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(Guid id, AttributeValue attributeValue)
        {
            if (id != attributeValue.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(attributeValue);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AttributeValueExists(attributeValue.Id))
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
            return BadRequest(ModelState);
        }

        // DELETE: api/AttributeValues/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var attributeValue = await _context.AttributeValues.FindAsync(id);
            if (attributeValue == null)
            {
                return NotFound();
            }

            _context.AttributeValues.Remove(attributeValue);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool AttributeValueExists(Guid id)
        {
            return _context.AttributeValues.Any(e => e.Id == id);
        }
    }
}
