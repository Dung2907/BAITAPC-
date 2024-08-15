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
    public class OrderItemsController : ControllerBase
    {
        private readonly Exercise02Context _context;

        public OrderItemsController(Exercise02Context context)
        {
            _context = context;
        }

        // GET: api/OrderItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderItem>>> GetOrderItems()
        {
            var orderItems = await _context.OrderItems
                .Include(o => o.Product)
                .Include(o => o.Order)
                .ToListAsync();
            return Ok(orderItems);
        }

        // GET: api/OrderItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderItem>> GetOrderItem(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid ID.");
            }

            var orderItem = await _context.OrderItems
                .Include(o => o.Product)
                .Include(o => o.Order)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (orderItem == null)
            {
                return NotFound();
            }

            return Ok(orderItem);
        }

        // POST: api/OrderItems
        [HttpPost]
        public async Task<ActionResult<OrderItem>> CreateOrderItem([FromBody] OrderItem orderItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            orderItem.Id = Guid.NewGuid(); // Ensure that the orderItem has a unique Id
            _context.OrderItems.Add(orderItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrderItem), new { id = orderItem.Id }, orderItem);
        }

        // PUT: api/OrderItems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrderItem(Guid id, [FromBody] OrderItem orderItem)
        {
            if (id != orderItem.Id)
            {
                return BadRequest("ID mismatch.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                orderItem.Id = id; // Ensure ID is set
                _context.Entry(orderItem).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderItemExists(id))
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

        // DELETE: api/OrderItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderItem(Guid id)
        {
            var orderItem = await _context.OrderItems.FindAsync(id);
            if (orderItem == null)
            {
                return NotFound();
            }

            _context.OrderItems.Remove(orderItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderItemExists(Guid id)
        {
            return _context.OrderItems.Any(e => e.Id == id);
        }
    }
}
