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
    public class CustomerAddressesController : ControllerBase
    {
        private readonly Exercise02Context _context;

        public CustomerAddressesController(Exercise02Context context)
        {
            _context = context;
        }

        // GET: api/CustomerAddresses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerAddress>>> Index()
        {
            var customerAddresses = await _context.CustomerAddresses.Include(ca => ca.Customer).ToListAsync();
            return Ok(customerAddresses);
        }

        // GET: api/CustomerAddresses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerAddress>> Details(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var customerAddress = await _context.CustomerAddresses
                .Include(ca => ca.Customer)
                .FirstOrDefaultAsync(ca => ca.Id == id);

            if (customerAddress == null)
            {
                return NotFound();
            }

            return Ok(customerAddress);
        }

        // POST: api/CustomerAddresses
        [HttpPost]
        public async Task<ActionResult<CustomerAddress>> Create([Bind("Id,CustomerId,AddressLine1,AddressLine2,PhoneNumber,DialCode,Country,PostalCode,City")] CustomerAddress customerAddress)
        {
            if (ModelState.IsValid)
            {
                customerAddress.Id = Guid.NewGuid(); // Ensure that the address has a unique Id
                _context.Add(customerAddress);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(Details), new { id = customerAddress.Id }, customerAddress);
            }
            return BadRequest(ModelState);
        }

        // PUT: api/CustomerAddresses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,CustomerId,AddressLine1,AddressLine2,PhoneNumber,DialCode,Country,PostalCode,City")] CustomerAddress customerAddress)
        {
            if (id != customerAddress.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customerAddress);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerAddressExists(customerAddress.Id))
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

        // DELETE: api/CustomerAddresses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var customerAddress = await _context.CustomerAddresses.FindAsync(id);
            if (customerAddress == null)
            {
                return NotFound();
            }

            _context.CustomerAddresses.Remove(customerAddress);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool CustomerAddressExists(Guid id)
        {
            return _context.CustomerAddresses.Any(e => e.Id == id);
        }
    }
}
