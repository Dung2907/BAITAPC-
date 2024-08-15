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
    public class OrderItemController : ControllerBase
    {
        private readonly Exercise01Context _context;

        public OrderItemController(Exercise01Context context)
        {
            _context = context;
        }

        // GET: api/orderitems
        [HttpGet]
        public ActionResult<IEnumerable<OrderItem>> GetOrderItems()
        {
            var orderItems = _context.OrderItems.ToList();
            return orderItems;
        }

        // GET: api/orderitems/{orderItemId}
        [HttpGet("{orderItemId}")]
        public ActionResult<OrderItem> GetOrderItem(int orderItemId)
        {
            var orderItem = _context.OrderItems.Find(orderItemId);

            if (orderItem == null)
            {
                return NotFound();
            }

            return orderItem;
        }

        // POST: api/orderitems
        [HttpPost]
        public async Task<IActionResult> AddOrderItem([FromBody] OrderItemInputModel orderItemInput)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newOrderItem = new OrderItem
                    {
                        ProductId = orderItemInput.ProductId,
                        OrderId = orderItemInput.OrderId,
                        Quantity = orderItemInput.Quantity
                    };

                    _context.OrderItems.Add(newOrderItem);
                    await _context.SaveChangesAsync();

                    return Ok(newOrderItem);
                }

                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        // PUT: api/orderitems/{orderItemId}
        [HttpPut("{orderItemId}")]
        public async Task<IActionResult> PutOrderItem(int orderItemId, [FromBody] OrderItemInputModel orderItemInput)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var existingOrderItem = await _context.OrderItems.FindAsync(orderItemId);

                    if (existingOrderItem == null)
                    {
                        return NotFound();
                    }

                    // Update order item information from the input data
                    existingOrderItem.ProductId = orderItemInput.ProductId;
                    existingOrderItem.OrderId = orderItemInput.OrderId;
                    existingOrderItem.Quantity = orderItemInput.Quantity;

                    _context.Entry(existingOrderItem).State = EntityState.Modified;

                    await _context.SaveChangesAsync();
                    return Ok(existingOrderItem);
                }

                return BadRequest(ModelState);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.OrderItems.AnyAsync(oi => oi.OrderItemId == orderItemId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        // DELETE: api/orderitems/{orderItemId}
        [HttpDelete("{orderItemId}")]
        public async Task<ActionResult<OrderItem>> DeleteOrderItem(int orderItemId)
        {
            try
            {
                var orderItem = await _context.OrderItems.FindAsync(orderItemId);

                if (orderItem == null)
                {
                    return NotFound();
                }

                _context.OrderItems.Remove(orderItem);
                await _context.SaveChangesAsync();

                return Ok(new { message = $"OrderItem with ID {orderItemId} has been successfully deleted." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
