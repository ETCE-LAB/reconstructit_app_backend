using Backend_Platform.Entities;
using Backend_Platform.Entities.enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using WebApplication1.Data;

namespace WebApplication1.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly DBContext _context;
        public PaymentsController(DBContext context) => _context = context;

        [HttpPost]
        public async Task<ActionResult<Payment>> PostPayment(PaymentRecords.CreatePaymentRecord record)
        {
            var payment = new Payment
            {
                PaymentStatus = record.PaymentStatus,
                PrintContractId = record.PrintContractId,
                PaymentMethodId = record.PaymentMethodId
            };
            _context.Payments.Add(payment);
            // add payment id in contract
            var contract = await  _context.PrintContracts.FindAsync(record.PrintContractId);
            if (contract == null) return NotFound();
            contract.PaymentId = payment.Id;
           

            await _context.SaveChangesAsync();
            return Ok(payment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPayment(Guid id, PaymentRecords.UpdatePaymentRecord record)
        {
            if (id != record.Id) return BadRequest();

            var payment = await _context.Payments.FindAsync(id);
            if (payment == null) return NotFound();

            payment.PaymentStatus = record.PaymentStatus;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Payment>> GetPayment(Guid id)
        {
            var payment =  await _context.Payments.FindAsync(id);
            if (payment == null)
            {
                return NotFound();
            }

            return Content(JsonSerializer.Serialize(payment, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            }), "application/json"); ; ;
        }
    }

    public class PaymentRecords
    {
        public record CreatePaymentRecord(Guid PrintContractId, Guid PaymentMethodId, PaymentStatus PaymentStatus);
        public record UpdatePaymentRecord(Guid Id, PaymentStatus PaymentStatus);
    }
}
