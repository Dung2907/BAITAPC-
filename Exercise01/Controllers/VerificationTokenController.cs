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
    public class VerificationTokenController : ControllerBase
    {
        private readonly Exercise01Context _context;

        public VerificationTokenController(Exercise01Context context)
        {
            _context = context;
        }

        // GET: api/verificationtokens
        [HttpGet]
        public ActionResult<IEnumerable<VerificationToken>> GetVerificationTokens()
        {
            var verificationTokens = _context.VerificationTokens.ToList();
            return verificationTokens;
        }

        // GET: api/verificationtokens/{tokenId}
        [HttpGet("{tokenId}")]
        public ActionResult<VerificationToken> GetVerificationToken(int tokenId)
        {
            var verificationToken = _context.VerificationTokens.Find(tokenId);

            if (verificationToken == null)
            {
                return NotFound();
            }

            return verificationToken;
        }

        // POST: api/verificationtokens
        [HttpPost]
        public async Task<IActionResult> AddVerificationToken([FromBody] VerificationTokenInputModel verificationTokenInput)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newVerificationToken = new VerificationToken
                    {
                        CredentialId = verificationTokenInput.CredentialId,
                        VerifToken = verificationTokenInput.VerifToken,
                        ExpiredDate = verificationTokenInput.ExpiredDate
                    };

                    _context.VerificationTokens.Add(newVerificationToken);
                    await _context.SaveChangesAsync();

                    return Ok(newVerificationToken);
                }

                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        // PUT: api/verificationtokens/{tokenId}
        [HttpPut("{tokenId}")]
        public async Task<IActionResult> PutVerificationToken(int tokenId, [FromBody] VerificationTokenInputModel verificationTokenInput)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var existingVerificationToken = await _context.VerificationTokens.FindAsync(tokenId);

                    if (existingVerificationToken == null)
                    {
                        return NotFound();
                    }

                    // Cập nhật thông tin của đối tượng VerificationToken từ dữ liệu nhập
                    existingVerificationToken.CredentialId = verificationTokenInput.CredentialId;
                    existingVerificationToken.VerifToken = verificationTokenInput.VerifToken;
                    existingVerificationToken.ExpiredDate = verificationTokenInput.ExpiredDate;

                    _context.Entry(existingVerificationToken).State = EntityState.Modified;

                    await _context.SaveChangesAsync();
                    return Ok(existingVerificationToken);
                }

                return BadRequest(ModelState);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.VerificationTokens.AnyAsync(vt => vt.TokenId == tokenId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        // DELETE: api/verificationtokens/{tokenId}
        [HttpDelete("{tokenId}")]
        public async Task<ActionResult<VerificationToken>> DeleteVerificationToken(int tokenId)
        {
            try
            {
                var verificationToken = await _context.VerificationTokens.FindAsync(tokenId);

                if (verificationToken == null)
                {
                    return NotFound();
                }

                _context.VerificationTokens.Remove(verificationToken);
                await _context.SaveChangesAsync();

                return Ok(new { message = $"VerificationToken with ID {tokenId} has been successfully deleted." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
