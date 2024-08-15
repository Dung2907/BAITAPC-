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
    public class UserController : ControllerBase
    {
        private readonly Exercise01Context _context;

        public UserController(Exercise01Context context)
        {
            _context = context;
        }

        // GET: api/users
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            var users = _context.Users.ToList();
            return users;
        }

        // GET: api/users/{userId}
        [HttpGet("{userId}")]
        public ActionResult<User> GetUser(int userId)
        {
            var user = _context.Users.Find(userId);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // POST: api/users
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] UserInputModel userInput)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newUser = new User
                    {
                        FirstName = userInput.FirstName,
                        LastName = userInput.LastName,
                        ImageUrl = userInput.ImageUrl,
                        Email = userInput.Email,
                        Phone = userInput.Phone
                    };

                    _context.Users.Add(newUser);
                    await _context.SaveChangesAsync();

                    return Ok(newUser);
                }

                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        // PUT: api/users/{userId}
        [HttpPut("{userId}")]
        public async Task<IActionResult> PutUser(int userId, [FromBody] UserInputModel userInput)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var existingUser = await _context.Users.FindAsync(userId);

                    if (existingUser == null)
                    {
                        return NotFound();
                    }

                    // Update user information from the input data
                    existingUser.FirstName = userInput.FirstName;
                    existingUser.LastName = userInput.LastName;
                    existingUser.ImageUrl = userInput.ImageUrl;
                    existingUser.Email = userInput.Email;
                    existingUser.Phone = userInput.Phone;

                    _context.Entry(existingUser).State = EntityState.Modified;

                    await _context.SaveChangesAsync();
                    return Ok(existingUser);
                }

                return BadRequest(ModelState);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Users.AnyAsync(u => u.UserId == userId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        // DELETE: api/users/{userId}
        [HttpDelete("{userId}")]
        public async Task<ActionResult<User>> DeleteUser(int userId)
        {
            try
            {
                var user = await _context.Users.FindAsync(userId);

                if (user == null)
                {
                    return NotFound();
                }

                _context.Users.Remove(user);
                await _context.SaveChangesAsync();

                return Ok(new { message = $"User with ID {userId} has been successfully deleted." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
