using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Exercise02.Context;
using Exercise02.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exercise02.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly Exercise02Context _context;

        public RolesController(Exercise02Context context)
        {
            _context = context;
        }

        // GET: api/Roles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>> GetRoles()
        {
            return await _context.Roles.ToListAsync();
        }

        // GET: api/Roles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Role>> GetRole(Guid id) // Thay đổi kiểu id thành Guid
        {
            var role = await _context.Roles.FindAsync(id); // Sửa đổi so sánh thành Guid

            if (role == null)
            {
                return NotFound();
            }

            return role;
        }

        // POST: api/Roles
        [HttpPost]
        public async Task<ActionResult<Role>> PostRole(Role role)
        {
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRole), new { id = role.Id }, role);
        }

        // PUT: api/Roles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRole(Guid id, Role role) // Thay đổi kiểu id thành Guid
        {
            if (id != role.Id) // Sửa đổi so sánh thành Guid
            {
                return BadRequest();
            }

            _context.Entry(role).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoleExists(id)) // Thay đổi kiểu id thành Guid
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

        // DELETE: api/Roles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(Guid id) // Thay đổi kiểu id thành Guid
        {
            var role = await _context.Roles.FindAsync(id); // Sửa đổi so sánh thành Guid
            if (role == null)
            {
                return NotFound();
            }

            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RoleExists(Guid id) // Thay đổi kiểu id thành Guid
        {
            return _context.Roles.Any(e => e.Id == id); // Sửa đổi so sánh thành Guid
        }
    }
}
