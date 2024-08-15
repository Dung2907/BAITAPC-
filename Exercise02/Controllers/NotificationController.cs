using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Exercise02.Models;
using Exercise02.Context;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Exercise02.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly Exercise02Context _context;

        public NotificationsController(Exercise02Context context)
        {
            _context = context;
        }

        // GET: api/Notifications
        [HttpGet]
        public async Task<IActionResult> GetNotifications()
        {
            var notifications = await _context.Notifications.ToListAsync();
            return Ok(notifications);
        }

        // GET: api/Notifications/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNotification(Guid id)
        {
            var notification = await _context.Notifications.FindAsync(id);

            if (notification == null)
            {
                return NotFound();
            }

            return Ok(notification);
        }

        // POST: api/Notifications
        [HttpPost]
        public async Task<IActionResult> PostNotification(Notification notification)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            notification.Id = Guid.NewGuid(); // Ensure the Id is set
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetNotification), new { id = notification.Id }, notification);
        }

        // PUT: api/Notifications/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNotification(Guid id, Notification notification)
        {
            if (id != notification.Id)
            {
                return BadRequest();
            }

            _context.Entry(notification).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NotificationExists(id))
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

        // DELETE: api/Notifications/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotification(Guid id)
        {
            var notification = await _context.Notifications.FindAsync(id);
            if (notification == null)
            {
                return NotFound();
            }

            _context.Notifications.Remove(notification);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NotificationExists(Guid id)
        {
            return _context.Notifications.Any(e => e.Id == id);
        }
    }
}
