using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Exercise02.Context;
using Exercise02.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exercise02.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StaffAccountsController : ControllerBase
    {
        private readonly Exercise02Context _context;

        public StaffAccountsController(Exercise02Context context)
        {
            _context = context;
        }

        // GET: api/StaffAccounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StaffAccount>>> GetStaffAccounts()
        {
            return await _context.StaffAccounts
                .Include(sa => sa.Role) // Include Role if needed
                .ToListAsync();
        }

        // GET: api/StaffAccounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StaffAccount>> GetStaffAccount(Guid id) // Thay đổi kiểu id thành Guid
        {
            var staffAccount = await _context.StaffAccounts
                .Include(sa => sa.Role) // Include Role if needed
                .FirstOrDefaultAsync(sa => sa.Id == id); // Sửa đổi so sánh thành Guid

            if (staffAccount == null)
            {
                return NotFound();
            }

            return staffAccount;
        }

        // POST: api/StaffAccounts
        [HttpPost]
        public async Task<ActionResult<StaffAccount>> PostStaffAccount(StaffAccount staffAccount)
        {
            _context.StaffAccounts.Add(staffAccount);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStaffAccount), new { id = staffAccount.Id }, staffAccount);
        }

        // PUT: api/StaffAccounts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStaffAccount(Guid id, StaffAccount staffAccount) // Thay đổi kiểu id thành Guid
        {
            if (id != staffAccount.Id) // Sửa đổi so sánh thành Guid
            {
                return BadRequest();
            }

            _context.Entry(staffAccount).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StaffAccountExists(id)) // Thay đổi kiểu id thành Guid
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

        // DELETE: api/StaffAccounts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStaffAccount(Guid id) // Thay đổi kiểu id thành Guid
        {
            var staffAccount = await _context.StaffAccounts.FindAsync(id); // Sửa đổi so sánh thành Guid
            if (staffAccount == null)
            {
                return NotFound();
            }

            _context.StaffAccounts.Remove(staffAccount);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StaffAccountExists(Guid id) // Thay đổi kiểu id thành Guid
        {
            return _context.StaffAccounts.Any(e => e.Id == id); // Sửa đổi so sánh thành Guid
        }
    }
}
