using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Exercise02.Context;
using Exercise02.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exercise02.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductShippingInfoController : ControllerBase
    {
        private readonly Exercise02Context _context;

        public ProductShippingInfoController(Exercise02Context context)
        {
            _context = context;
        }

        // GET: api/ProductShippingInfo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductShippingInfo>>> GetProductShippingInfos()
        {
            return await _context.ProductShippingInfos
                .Include(ps => ps.Product) // Nếu cần lấy thông tin sản phẩm
                .ToListAsync();
        }

        // GET: api/ProductShippingInfo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductShippingInfo>> GetProductShippingInfo(Guid id)
        {
            var productShippingInfo = await _context.ProductShippingInfos
                .Include(ps => ps.Product) // Nếu cần lấy thông tin sản phẩm
                .FirstOrDefaultAsync(ps => ps.Id == id);

            if (productShippingInfo == null)
            {
                return NotFound();
            }

            return Ok(productShippingInfo);
        }

        // POST: api/ProductShippingInfo
        [HttpPost]
        public async Task<ActionResult<ProductShippingInfo>> PostProductShippingInfo(ProductShippingInfo productShippingInfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ProductShippingInfos.Add(productShippingInfo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProductShippingInfo), new { id = productShippingInfo.Id }, productShippingInfo);
        }

        // PUT: api/ProductShippingInfo/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductShippingInfo(Guid id, ProductShippingInfo productShippingInfo)
        {
            if (id != productShippingInfo.Id)
            {
                return BadRequest();
            }

            _context.Entry(productShippingInfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductShippingInfoExists(id))
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

        // DELETE: api/ProductShippingInfo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductShippingInfo(Guid id)
        {
            var productShippingInfo = await _context.ProductShippingInfos.FindAsync(id);
            if (productShippingInfo == null)
            {
                return NotFound();
            }

            _context.ProductShippingInfos.Remove(productShippingInfo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductShippingInfoExists(Guid id)
        {
            return _context.ProductShippingInfos.Any(e => e.Id == id);
        }
    }
}
