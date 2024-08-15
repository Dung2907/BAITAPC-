using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Exercise02.Models;
using Exercise02.Context;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Exercise02.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly Exercise02Context _context;

        public OrdersController(Exercise02Context context)
        {
            _context = context;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _context.Orders
                .Include(o => o.Coupon)
                .Include(o => o.Customer)
                .Include(o => o.OrderStatus)
                .Include(o => o.UpdatedByStaffAccount)
                .ToListAsync();

            return Ok(orders);
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(string id)
        {
            var order = await _context.Orders
                .Include(o => o.Coupon)
                .Include(o => o.Customer)
                .Include(o => o.OrderStatus)
                .Include(o => o.UpdatedByStaffAccount)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        // POST: api/Orders
        [HttpPost]
        public async Task<IActionResult> PostOrder(Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            order.CreatedAt = DateTime.UtcNow;
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
        }

        // PUT: api/Orders/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(string id, Order order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
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

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(string id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderExists(string id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
