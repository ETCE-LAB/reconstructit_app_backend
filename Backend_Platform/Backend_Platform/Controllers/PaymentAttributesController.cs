using Backend_Platform.Entities;
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
    public class PaymentAttributesController : ControllerBase
    {
        private readonly DBContext _context;
        public PaymentAttributesController(DBContext context) => _context = context;

        // GET: api/PaymentMethodDefinition/5/PaymentAttributes
        [HttpGet("/api/PaymentMethodDefinition/{id}/PaymentAttributes")]
        public async Task<ActionResult<IEnumerable<PaymentAttribute>>> GetAttributesForMethod(Guid id)
        {
            return await _context.PaymentAttributes
                .Where(attr => attr.PaymentMethodId == id)
                .ToListAsync();
        }

        // GET: api/PaymentAttributes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentAttribute>> GetById(Guid id)
        {
            var attr = await _context.PaymentAttributes.FindAsync(id);
            if(attr == null)
            {
                return NotFound();
            }
             return Content(JsonSerializer.Serialize(attr, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            }), "application/json"); ; ; ;
        }
    }
}
