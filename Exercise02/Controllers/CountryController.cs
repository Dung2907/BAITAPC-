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
    public class CountriesController : ControllerBase // Sử dụng ControllerBase cho API
    {
        private readonly Exercise02Context _context;

        public CountriesController(Exercise02Context context)
        {
            _context = context;
        }

        // GET: api/Countries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Country>>> Index()
        {
            var countries = await _context.Countries.ToListAsync();
            return Ok(countries);
        }

        // GET: api/Countries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Country>> Details(int id)
        {
            var country = await _context.Countries
                .FirstOrDefaultAsync(m => m.Id == id);

            if (country == null)
            {
                return NotFound();
            }

            return Ok(country);
        }

        // POST: api/Countries
        [HttpPost]
        public async Task<ActionResult<Country>> Create([Bind("Id,Iso,Name,UpperName,Iso3,NumCode,PhoneCode")] Country country)
        {
            if (ModelState.IsValid)
            {
                _context.Add(country);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(Details), new { id = country.Id }, country);
            }
            return BadRequest(ModelState);
        }

        // PUT: api/Countries/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Iso,Name,UpperName,Iso3,NumCode,PhoneCode")] Country country)
        {
            if (id != country.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(country);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CountryExists(country.Id))
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

        // DELETE: api/Countries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var country = await _context.Countries.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }

            _context.Countries.Remove(country);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool CountryExists(int id)
        {
            return _context.Countries.Any(e => e.Id == id);
        }
    }
}
