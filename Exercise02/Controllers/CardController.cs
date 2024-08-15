using System;
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
    public class CardsController : ControllerBase // Sử dụng ControllerBase cho API
    {
        private readonly Exercise02Context _context;

        public CardsController(Exercise02Context context)
        {
            _context = context;
        }

        // GET: api/Cards
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Card>>> Index()
        {
            var cards = await _context.Cards.Include(c => c.Customer).ToListAsync();
            return Ok(cards);
        }

        // GET: api/Cards/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Card>> Details(Guid id)
        {
            var card = await _context.Cards
                .Include(c => c.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (card == null)
            {
                return NotFound();
            }

            return Ok(card);
        }

        // POST: api/Cards
        [HttpPost]
        public async Task<ActionResult<Card>> Create([Bind("Id,CustomerId")] Card card)
        {
            if (ModelState.IsValid)
            {
                card.Id = Guid.NewGuid(); // Generate a new ID
                _context.Add(card);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(Details), new { id = card.Id }, card);
            }
            return BadRequest(ModelState);
        }

        // PUT: api/Cards/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,CustomerId")] Card card)
        {
            if (id != card.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(card);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CardExists(card.Id))
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

        // DELETE: api/Cards/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var card = await _context.Cards.FindAsync(id);
            if (card == null)
            {
                return NotFound();
            }

            _context.Cards.Remove(card);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool CardExists(Guid id)
        {
            return _context.Cards.Any(e => e.Id == id);
        }
    }
}
