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
    public class SlideshowController : ControllerBase
    {
        private readonly Exercise02Context _context;

        public SlideshowController(Exercise02Context context)
        {
            _context = context;
        }

        // GET: api/Slideshow
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Slideshow>>> GetSlideshows()
        {
            return await _context.Slideshows.ToListAsync();
        }

        // GET: api/Slideshow/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Slideshow>> GetSlideshow(Guid id)
        {
            var slideshow = await _context.Slideshows.FindAsync(id);

            if (slideshow == null)
            {
                return NotFound();
            }

            return slideshow;
        }

        // POST: api/Slideshow
        [HttpPost]
        public async Task<ActionResult<Slideshow>> PostSlideshow(Slideshow slideshow)
        {
            _context.Slideshows.Add(slideshow);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSlideshow), new { id = slideshow.Id }, slideshow);
        }

        // PUT: api/Slideshow/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSlideshow(Guid id, Slideshow slideshow)
        {
            if (id != slideshow.Id)
            {
                return BadRequest();
            }

            _context.Entry(slideshow).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SlideshowExists(id))
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

        // DELETE: api/Slideshow/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSlideshow(Guid id)
        {
            var slideshow = await _context.Slideshows.FindAsync(id);
            if (slideshow == null)
            {
                return NotFound();
            }

            _context.Slideshows.Remove(slideshow);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SlideshowExists(Guid id)
        {
            return _context.Slideshows.Any(e => e.Id == id);
        }
    }
}
