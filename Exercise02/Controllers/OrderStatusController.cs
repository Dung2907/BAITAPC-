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
    public class OrderStatusesController : ControllerBase
    {
        private readonly Exercise02Context _context;

        public OrderStatusesController(Exercise02Context context)
        {
            _context = context;
        }

        // GET: api/OrderStatuses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderStatus>>> GetOrderStatuses()
        {
            var orderStatuses = await _context.OrderStatuses
                .Include(o => o.CreatedByStaffAccount)
                .Include(o => o.UpdatedByStaffAccount)
                .ToListAsync();
            return Ok(orderStatuses);
        }

        // GET: api/OrderStatuses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderStatus>> GetOrderStatus(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid ID.");
            }

            var orderStatus = await _context.OrderStatuses
                .Include(o => o.CreatedByStaffAccount)
                .Include(o => o.UpdatedByStaffAccount)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (orderStatus == null)
            {
                return NotFound();
            }

            return Ok(orderStatus);
        }

        // POST: api/OrderStatuses
        [HttpPost]
        public async Task<ActionResult<OrderStatus>> CreateOrderStatus([FromBody] OrderStatus orderStatus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            orderStatus.Id = Guid.NewGuid(); // Ensure that the orderStatus has a unique Id
            _context.OrderStatuses.Add(orderStatus);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrderStatus), new { id = orderStatus.Id }, orderStatus);
        }

        // PUT: api/OrderStatuses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrderStatus(Guid id, [FromBody] OrderStatus orderStatus)
        {
            if (id != orderStatus.Id)
            {
                return BadRequest("ID mismatch.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _context.Entry(orderStatus).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderStatusExists(id))
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

        // DELETE: api/OrderStatuses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderStatus(Guid id)
        {
            var orderStatus = await _context.OrderStatuses.FindAsync(id);
            if (orderStatus == null)
            {
                return NotFound();
            }

            _context.OrderStatuses.Remove(orderStatus);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderStatusExists(Guid id)
        {
            return _context.OrderStatuses.Any(e => e.Id == id);
        }
    }
}
