using System;
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
    public class CouponsController : ControllerBase // Sử dụng ControllerBase cho API
    {
        private readonly Exercise02Context _context;

        public CouponsController(Exercise02Context context)
        {
            _context = context;
        }

        // GET: api/Coupons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Coupon>>> Index()
        {
            var coupons = await _context.Coupons.ToListAsync();
            return Ok(coupons);
        }

        // GET: api/Coupons/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Coupon>> Details(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var coupon = await _context.Coupons
                .FirstOrDefaultAsync(m => m.Id == id);

            if (coupon == null)
            {
                return NotFound();
            }

            return Ok(coupon);
        }

        // POST: api/Coupons
        [HttpPost]
        public async Task<ActionResult<Coupon>> Create([Bind("Id,Code,DiscountValue,DiscountType,TimesUsed,MaxUsage,OrderAmountLimit,CouponStartDate,CouponEndDate")] Coupon coupon)
        {
            if (ModelState.IsValid)
            {
                coupon.Id = Guid.NewGuid(); // Ensure that the coupon has a unique Id
                _context.Add(coupon);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(Details), new { id = coupon.Id }, coupon);
            }
            return BadRequest(ModelState);
        }

        // PUT: api/Coupons/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Code,DiscountValue,DiscountType,TimesUsed,MaxUsage,OrderAmountLimit,CouponStartDate,CouponEndDate")] Coupon coupon)
        {
            if (id != coupon.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(coupon);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CouponExists(coupon.Id))
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

        // DELETE: api/Coupons/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var coupon = await _context.Coupons.FindAsync(id);
            if (coupon == null)
            {
                return NotFound();
            }

            _context.Coupons.Remove(coupon);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool CouponExists(Guid id)
        {
            return _context.Coupons.Any(e => e.Id == id);
        }
    }
}
