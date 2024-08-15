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
    public class CredentialController : ControllerBase
    {
        private readonly Exercise01Context _context;

        public CredentialController(Exercise01Context context)
        {
            _context = context;
        }

        // GET: api/credentials
        [HttpGet]
        public ActionResult<IEnumerable<Credential>> GetCredentials()
        {
            var credentials = _context.Credentials.ToList();
            return credentials;
        }

        // GET: api/credentials/{credentialId}
        [HttpGet("{credentialId}")]
        public ActionResult<Credential> GetCredential(int credentialId)
        {
            var credential = _context.Credentials.Find(credentialId);

            if (credential == null)
            {
                return NotFound();
            }

            return credential;
        }

        // POST: api/credentials
        [HttpPost]
        public async Task<IActionResult> AddCredential([FromBody] CredentialInputModel credentialInput)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newCredential = new Credential
                    {
                        UserId = credentialInput.UserId,
                        Username = credentialInput.Username,
                        Password = credentialInput.Password,
                        Role = credentialInput.Role,
                        IsEnabled = credentialInput.IsEnabled,
                        IsAccountNonExpired = credentialInput.IsAccountNonExpired,
                        IsAccountNonLocked = credentialInput.IsAccountNonLocked,
                        IsCredentialsNonExpired = credentialInput.IsCredentialsNonExpired
                    };

                    _context.Credentials.Add(newCredential);
                    await _context.SaveChangesAsync();

                    return Ok(newCredential);
                }

                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        // PUT: api/credentials/{credentialId}
        [HttpPut("{credentialId}")]
        public async Task<IActionResult> PutCredential(int credentialId, [FromBody] CredentialInputModel credentialInput)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var existingCredential = await _context.Credentials.FindAsync(credentialId);

                    if (existingCredential == null)
                    {
                        return NotFound();
                    }

                    // Cập nhật thông tin của đối tượng Credential từ dữ liệu nhập
                    existingCredential.UserId = credentialInput.UserId;
                    existingCredential.Username = credentialInput.Username;
                    existingCredential.Password = credentialInput.Password;
                    existingCredential.Role = credentialInput.Role;
                    existingCredential.IsEnabled = credentialInput.IsEnabled;
                    existingCredential.IsAccountNonExpired = credentialInput.IsAccountNonExpired;
                    existingCredential.IsAccountNonLocked = credentialInput.IsAccountNonLocked;
                    existingCredential.IsCredentialsNonExpired = credentialInput.IsCredentialsNonExpired;

                    _context.Entry(existingCredential).State = EntityState.Modified;

                    await _context.SaveChangesAsync();
                    return Ok(existingCredential);
                }

                return BadRequest(ModelState);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Credentials.AnyAsync(c => c.CredentialId == credentialId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        // DELETE: api/credentials/{credentialId}
        [HttpDelete("{credentialId}")]
        public async Task<ActionResult<Credential>> DeleteCredential(int credentialId)
        {
            try
            {
                var credential = await _context.Credentials.FindAsync(credentialId);

                if (credential == null)
                {
                    return NotFound();
                }

                _context.Credentials.Remove(credential);
                await _context.SaveChangesAsync();

                return Ok(new { message = $"Credential with ID {credentialId} has been successfully deleted." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
