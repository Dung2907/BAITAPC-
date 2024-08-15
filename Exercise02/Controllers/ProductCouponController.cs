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
    public class ProductCouponsController : ControllerBase
    {
        private readonly Exercise02Context _context;

        public ProductCouponsController(Exercise02Context context)
        {
            _context = context;
        }

        // GET: api/ProductCoupons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductCoupon>>> GetProductCoupons()
        {
            var productCoupons = await _context.ProductCoupons
                .Include(p => p.Product)
                .Include(p => p.Coupon)
                .ToListAsync();
            return Ok(productCoupons);
        }

        // GET: api/ProductCoupons/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductCoupon>> GetProductCoupon(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid ID.");
            }

            var productCoupon = await _context.ProductCoupons
                .Include(p => p.Product)
                .Include(p => p.Coupon)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (productCoupon == null)
            {
                return NotFound();
            }

            return Ok(productCoupon);
        }

        // POST: api/ProductCoupons
        [HttpPost]
        public async Task<ActionResult<ProductCoupon>> CreateProductCoupon([FromBody] ProductCoupon productCoupon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            productCoupon.Id = Guid.NewGuid(); // Ensure that the productCoupon has a unique Id
            _context.ProductCoupons.Add(productCoupon);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProductCoupon), new { id = productCoupon.Id }, productCoupon);
        }

        // PUT: api/ProductCoupons/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProductCoupon(Guid id, [FromBody] ProductCoupon productCoupon)
        {
            if (id != productCoupon.Id)
            {
                return BadRequest("ID mismatch.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _context.Entry(productCoupon).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductCouponExists(id))
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

        // DELETE: api/ProductCoupons/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductCoupon(Guid id)
        {
            var productCoupon = await _context.ProductCoupons.FindAsync(id);
            if (productCoupon == null)
            {
                return NotFound();
            }

            _context.ProductCoupons.Remove(productCoupon);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductCouponExists(Guid id)
        {
            return _context.ProductCoupons.Any(e => e.Id == id);
        }
    }
}
