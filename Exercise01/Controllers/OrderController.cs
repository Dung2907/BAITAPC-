using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Exercise01.Context;
using Exercise01.Models;
using Exercise01.InputModels;

namespace Exercise01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly Exercise01Context _context;

        public OrderController(Exercise01Context context)
        {
            _context = context;
        }

        // GET: api/orders
        [HttpGet]
        public ActionResult<IEnumerable<Order>> GetOrders()
        {
            var orders = _context.Orders.ToList();
            return orders;
        }

        // GET: api/orders/{orderId}
        [HttpGet("{orderId}")]
        public ActionResult<Order> GetOrder(int orderId)
        {
            var order = _context.Orders.Find(orderId);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // POST: api/orders
        [HttpPost]
        public async Task<IActionResult> AddOrder([FromBody] OrderInputModel orderInput)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newOrder = new Order
                    {
                        CartId = orderInput.CartId,
                        OrderDesc = orderInput.OrderDesc,
                        OrderFee = orderInput.OrderFee,
                       
                    };

                    _context.Orders.Add(newOrder);
                    await _context.SaveChangesAsync();

                    return Ok(newOrder);
                }

                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        // PUT: api/orders/{orderId}
        [HttpPut("{orderId}")]
        public async Task<IActionResult> PutOrder(int orderId, [FromBody] OrderInputModel orderInput)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var existingOrder = await _context.Orders.FindAsync(orderId);

                    if (existingOrder == null)
                    {
                        return NotFound();
                    }

                    // Update order information from the input data
                    existingOrder.CartId = orderInput.CartId;
                    existingOrder.OrderDesc = orderInput.OrderDesc;
                    existingOrder.OrderFee = orderInput.OrderFee;

                    _context.Entry(existingOrder).State = EntityState.Modified;

                    await _context.SaveChangesAsync();
                    return Ok(existingOrder);
                }

                return BadRequest(ModelState);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Orders.AnyAsync(o => o.OrderId == orderId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        // DELETE: api/orders/{orderId}
        [HttpDelete("{orderId}")]
        public async Task<ActionResult<Order>> DeleteOrder(int orderId)
        {
            try
            {
                var order = await _context.Orders.FindAsync(orderId);

                if (order == null)
                {
                    return NotFound();
                }

                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();

                return Ok(new { message = $"Order with ID {orderId} has been successfully deleted." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
