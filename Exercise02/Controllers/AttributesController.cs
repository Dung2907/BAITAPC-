using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Exercise02.Models;
using Exercise02.Context;
using AttributeModel = Exercise02.Models.Attribute; // Alias cho Attribute của bạn

namespace Exercise02.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttributesController : ControllerBase
    {
        private readonly Exercise02Context _context;

        public AttributesController(Exercise02Context context)
        {
            _context = context;
        }

        // GET: api/Attributes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AttributeModel>>> GetAttributes()
        {
            return await _context.Attributes.ToListAsync();
        }

        // GET: api/Attributes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AttributeModel>> GetAttribute(Guid id)
        {
            var attribute = await _context.Attributes.FindAsync(id);

            if (attribute == null)
            {
                return NotFound();
            }

            return attribute;
        }

        // PUT: api/Attributes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAttribute(Guid id, AttributeModel attribute)
        {
            if (id != attribute.Id)
            {
                return BadRequest();
            }

            _context.Entry(attribute).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AttributeExists(id))
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

        // POST: api/Attributes
        [HttpPost]
        public async Task<ActionResult<AttributeModel>> PostAttribute(AttributeModel attribute)
        {
            _context.Attributes.Add(attribute);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAttribute", new { id = attribute.Id }, attribute);
        }

        // DELETE: api/Attributes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAttribute(Guid id)
        {
            var attribute = await _context.Attributes.FindAsync(id);
            if (attribute == null)
            {
                return NotFound();
            }

            _context.Attributes.Remove(attribute);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AttributeExists(Guid id)
        {
            return _context.Attributes.Any(e => e.Id == id);
        }
    }
}
