using Backend_Platform.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;

namespace WebApplication1.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentValuesController : ControllerBase
    {
        private readonly DBContext _context;
        public PaymentValuesController(DBContext context) => _context = context;

        [HttpPost]
        public async Task<ActionResult<PaymentValue>> PostValue(PaymentValueRecord record)
        {
            var value = new PaymentValue { Value = record.Value, PaymentAttributeId = record.PaymentAttributeId, PaymentId = record.PaymentId };
            _context.PaymentValues.Add(value);
            await _context.SaveChangesAsync();
            return Ok(value);
        }

        [HttpGet("/api/PaymentMethod/{id}/paymentValues")]
        public async Task<ActionResult<IEnumerable<PaymentValue>>> GetValuesForMethod(Guid id)
        {
            var values = await _context.PaymentAttributes
                .Where(attr => attr.PaymentMethodId == id)
                .SelectMany(attr => attr.Values)
                .ToListAsync();

            return values;
        }

    }

    public record PaymentValueRecord(string Value, Guid PaymentAttributeId, Guid PaymentId);
}
