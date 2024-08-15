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
    public class LikeController : ControllerBase
    {
        private readonly Exercise01Context _context;

        public LikeController(Exercise01Context context)
        {
            _context = context;
        }

        // GET: api/likes
        [HttpGet]
        public ActionResult<IEnumerable<Like>> GetLikes()
        {
            var likes = _context.Likes.ToList();
            return likes;
        }

        // GET: api/likes/{userId}/{productId}
        [HttpGet("{userId}/{productId}")]
        public ActionResult<Like> GetLike(int userId, int productId)
        {
            var like = _context.Likes.Find(userId, productId);

            if (like == null)
            {
                return NotFound();
            }

            return like;
        }

        // POST: api/likes
        [HttpPost]
        public async Task<IActionResult> AddLike([FromBody] LikeInputModel likeInput)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newLike = new Like
                    {
                        UserId = likeInput.UserId,
                        ProductId = likeInput.ProductId,
                    };

                    _context.Likes.Add(newLike);
                    await _context.SaveChangesAsync();

                    return Ok(newLike);
                }

                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        // DELETE: api/likes/{userId}/{productId}
        [HttpDelete("{userId}/{productId}")]
        public async Task<ActionResult<Like>> DeleteLike(int userId, int productId)
        {
            try
            {
                var like = await _context.Likes.FindAsync(userId, productId);

                if (like == null)
                {
                    return NotFound();
                }

                _context.Likes.Remove(like);
                await _context.SaveChangesAsync();

                return Ok(new { message = $"Like with UserId {userId} and ProductId {productId} has been successfully deleted." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
