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
    public class CartController : ControllerBase
    {
        private readonly Exercise01Context _context;

        public CartController(Exercise01Context context)
        {
            _context = context;
        }

        // GET: api/carts
        [HttpGet]
        public ActionResult<IEnumerable<Cart>> GetCarts()
        {
            var carts = _context.Carts.ToList();
            return carts;
        }

        // GET: api/carts/{cartId}
        [HttpGet("{cartId}")]
        public ActionResult<Cart> GetCart(int cartId)
        {
            var cart = _context.Carts.Find(cartId);

            if (cart == null)
            {
                return NotFound();
            }

            return cart;
        }

        // POST: api/carts
        [HttpPost]
        public async Task<IActionResult> AddCart([FromBody] CartInputModel cartInput)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newCart = new Cart
                    {
                        UserId = cartInput.UserId
                    };

                    _context.Carts.Add(newCart);
                    await _context.SaveChangesAsync();

                    return Ok(newCart);
                }

                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        // PUT: api/carts/{cartId}
        [HttpPut("{cartId}")]
        public async Task<IActionResult> PutCart(int cartId, [FromBody] CartInputModel cartInput)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var existingCart = await _context.Carts.FindAsync(cartId);

                    if (existingCart == null)
                    {
                        return NotFound();
                    }

                    // Update cart information from the input data
                    existingCart.UserId = cartInput.UserId;

                    _context.Entry(existingCart).State = EntityState.Modified;

                    await _context.SaveChangesAsync();
                    return Ok(existingCart);
                }

                return BadRequest(ModelState);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Carts.AnyAsync(c => c.CartId == cartId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        // DELETE: api/carts/{cartId}
        [HttpDelete("{cartId}")]
        public async Task<ActionResult<Cart>> DeleteCart(int cartId)
        {
            try
            {
                var cart = await _context.Carts.FindAsync(cartId);

                if (cart == null)
                {
                    return NotFound();
                }

                _context.Carts.Remove(cart);
                await _context.SaveChangesAsync();

                return Ok(new { message = $"Cart with ID {cartId} has been successfully deleted." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
