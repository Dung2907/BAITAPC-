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
    public class CardItemsController : ControllerBase // Sử dụng ControllerBase cho API
    {
        private readonly Exercise02Context _context;

        public CardItemsController(Exercise02Context context)
        {
            _context = context;
        }

        // GET: api/CardItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CardItem>>> Index()
        {
            var cardItems = await _context.CardItems
                .Include(ci => ci.Card)
                .Include(ci => ci.Product)
                .ToListAsync();
            return Ok(cardItems);
        }

        // GET: api/CardItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CardItem>> Details(Guid id)
        {
            var cardItem = await _context.CardItems
                .Include(ci => ci.Card)
                .Include(ci => ci.Product)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (cardItem == null)
            {
                return NotFound();
            }

            return Ok(cardItem);
        }

        // POST: api/CardItems
        [HttpPost]
        public async Task<ActionResult<CardItem>> Create([Bind("Id,CardId,ProductId,Quantity")] CardItem cardItem)
        {
            if (ModelState.IsValid)
            {
                cardItem.Id = Guid.NewGuid(); // Generate a new ID
                _context.Add(cardItem);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(Details), new { id = cardItem.Id }, cardItem);
            }
            return BadRequest(ModelState);
        }

        // PUT: api/CardItems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,CardId,ProductId,Quantity")] CardItem cardItem)
        {
            if (id != cardItem.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cardItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CardItemExists(cardItem.Id))
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

        // DELETE: api/CardItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var cardItem = await _context.CardItems.FindAsync(id);
            if (cardItem == null)
            {
                return NotFound();
            }

            _context.CardItems.Remove(cardItem);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool CardItemExists(Guid id)
        {
            return _context.CardItems.Any(e => e.Id == id);
        }
    }
}
