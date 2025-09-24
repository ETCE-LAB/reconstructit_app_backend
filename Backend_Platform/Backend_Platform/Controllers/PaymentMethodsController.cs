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
    public class PaymentMethodsController : ControllerBase
    {
        private readonly DBContext _context;
        public PaymentMethodsController(DBContext context) => _context = context;

        // GET: api/PaymentMethods
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentMethod>>> GetAll() =>
            await _context.PaymentMethods.ToListAsync();

        // GET: api/PaymentMethods/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentMethod>> GetById(Guid id)
        {
            var method = await _context.PaymentMethods.FindAsync(id);
            return method == null ? NotFound() : Ok(method);
        }
    }
}
