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
    public class GalleryController : ControllerBase
    {
        private readonly Exercise02Context _context;

        public GalleryController(Exercise02Context context)
        {
            _context = context;
        }

        // GET: api/Gallery
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Gallery>>> GetGalleries()
        {
            var galleries = await _context.Galleries.Include(g => g.Product).ToListAsync();
            return Ok(galleries);
        }

        // GET: api/Gallery/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Gallery>> GetGallery(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid ID.");
            }

            var gallery = await _context.Galleries.Include(g => g.Product).FirstOrDefaultAsync(m => m.Id == id);

            if (gallery == null)
            {
                return NotFound();
            }

            return Ok(gallery);
        }

        // POST: api/Gallery
        [HttpPost]
        public async Task<ActionResult<Gallery>> CreateGallery([Bind("Id,ProductId,Image,Placeholder,IsThumbnail,CreatedAt,UpdatedAt")] Gallery gallery)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            gallery.Id = Guid.NewGuid(); // Generate new Id
            gallery.CreatedAt = DateTime.UtcNow; // Set the creation date
            _context.Galleries.Add(gallery);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetGallery), new { id = gallery.Id }, gallery);
        }

        // PUT: api/Gallery/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGallery(Guid id, [Bind("Id,ProductId,Image,Placeholder,IsThumbnail,CreatedAt,UpdatedAt")] Gallery gallery)
        {
            if (id != gallery.Id)
            {
                return BadRequest("ID mismatch.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                gallery.UpdatedAt = DateTime.UtcNow; // Update the timestamp
                _context.Entry(gallery).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GalleryExists(id))
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

        // DELETE: api/Gallery/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGallery(Guid id)
        {
            var gallery = await _context.Galleries.FindAsync(id);
            if (gallery == null)
            {
                return NotFound();
            }

            _context.Galleries.Remove(gallery);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GalleryExists(Guid id)
        {
            return _context.Galleries.Any(e => e.Id == id);
        }
    }
}
