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
    public class PaymentController : ControllerBase
    {
        private readonly Exercise01Context _context;

        public PaymentController(Exercise01Context context)
        {
            _context = context;
        }

        // GET: api/payments
        [HttpGet]
        public ActionResult<IEnumerable<Payment>> GetPayments()
        {
            var payments = _context.Payments.ToList();
            return payments;
        }

        // GET: api/payments/{paymentId}
        [HttpGet("{paymentId}")]
        public ActionResult<Payment> GetPayment(int paymentId)
        {
            var payment = _context.Payments.Find(paymentId);

            if (payment == null)
            {
                return NotFound();
            }

            return payment;
        }

        // POST: api/payments
        [HttpPost]
        public async Task<IActionResult> AddPayment([FromBody] PaymentInputModel paymentInput)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newPayment = new Payment
                    {
                        OrderId = paymentInput.OrderId,
                        IdPayed = paymentInput.IdPayed,
                        PaymentStatus = paymentInput.PaymentStatus
                    };

                    _context.Payments.Add(newPayment);
                    await _context.SaveChangesAsync();

                    return Ok(newPayment);
                }

                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        // PUT: api/payments/{paymentId}
        [HttpPut("{paymentId}")]
        public async Task<IActionResult> PutPayment(int paymentId, [FromBody] PaymentInputModel paymentInput)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var existingPayment = await _context.Payments.FindAsync(paymentId);

                    if (existingPayment == null)
                    {
                        return NotFound();
                    }

                    // Update payment information from the input data
                    existingPayment.OrderId = paymentInput.OrderId;
                    existingPayment.IdPayed = paymentInput.IdPayed;
                    existingPayment.PaymentStatus = paymentInput.PaymentStatus;

                    _context.Entry(existingPayment).State = EntityState.Modified;

                    await _context.SaveChangesAsync();
                    return Ok(existingPayment);
                }

                return BadRequest(ModelState);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Payments.AnyAsync(p => p.PaymentId == paymentId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        // DELETE: api/payments/{paymentId}
        [HttpDelete("{paymentId}")]
        public async Task<ActionResult<Payment>> DeletePayment(int paymentId)
        {
            try
            {
                var payment = await _context.Payments.FindAsync(paymentId);

                if (payment == null)
                {
                    return NotFound();
                }

                _context.Payments.Remove(payment);
                await _context.SaveChangesAsync();

                return Ok(new { message = $"Payment with ID {paymentId} has been successfully deleted." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
