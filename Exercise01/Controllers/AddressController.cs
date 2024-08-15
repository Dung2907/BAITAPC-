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
    public class AddressController : ControllerBase
    {
        private readonly Exercise01Context _context;

        public AddressController(Exercise01Context context)
        {
            _context = context;
        }

        // GET: api/addresses
        [HttpGet]
        public ActionResult<IEnumerable<Address>> GetAddresses()
        {
            var addresses = _context.Addresses.ToList();
            return addresses;
        }

        // GET: api/addresses/{addressId}
        [HttpGet("{addressId}")]
        public ActionResult<Address> GetAddress(int addressId)
        {
            var address = _context.Addresses.Find(addressId);

            if (address == null)
            {
                return NotFound();
            }

            return address;
        }

        // POST: api/addresses
        [HttpPost]
        public async Task<IActionResult> AddAddress([FromBody] AddressInputModel addressInput)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newAddress = new Address
                    {
                        FullAddress = addressInput.FullAddress,
                        PostalCode = addressInput.PostalCode,
                        City = addressInput.City,
                        UserId = addressInput.UserId
                    };

                    _context.Addresses.Add(newAddress);
                    await _context.SaveChangesAsync();

                    return Ok(newAddress);
                }

                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        // PUT: api/addresses/{addressId}
        [HttpPut("{addressId}")]
        public async Task<IActionResult> PutAddress(int addressId, [FromBody] AddressInputModel addressInput)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var existingAddress = await _context.Addresses.FindAsync(addressId);

                    if (existingAddress == null)
                    {
                        return NotFound();
                    }

                    // Update address information from the input data
                    existingAddress.FullAddress = addressInput.FullAddress;
                    existingAddress.PostalCode = addressInput.PostalCode;
                    existingAddress.City = addressInput.City;
                    existingAddress.UserId = addressInput.UserId;

                    _context.Entry(existingAddress).State = EntityState.Modified;

                    await _context.SaveChangesAsync();
                    return Ok(existingAddress);
                }

                return BadRequest(ModelState);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Addresses.AnyAsync(a => a.AddressId == addressId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        // DELETE: api/addresses/{addressId}
        [HttpDelete("{addressId}")]
        public async Task<ActionResult<Address>> DeleteAddress(int addressId)
        {
            try
            {
                var address = await _context.Addresses.FindAsync(addressId);

                if (address == null)
                {
                    return NotFound();
                }

                _context.Addresses.Remove(address);
                await _context.SaveChangesAsync();

                return Ok(new { message = $"Address with ID {addressId} has been successfully deleted." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
