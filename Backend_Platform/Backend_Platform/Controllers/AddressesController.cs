using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Backend_Platform.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
   [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly DBContext _context;
        public AddressesController(DBContext context)
        {
            _context = context;
        }


        // GET: api/Addresses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Address>> GetAddress(string id)
        {
            var address = await _context.Addresses.FindAsync(id);

            if (address == null)
            {
                return NotFound();
            }

            return Content(JsonSerializer.Serialize(address, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            }), "application/json"); ; ;
        }

       
        // POST: api/Addresses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Address>> PostAddress(AddressesRecords.CreateAddressRecord addressRecord)
        {
              var address = new Address(){
                Street = addressRecord.Street,
                City = addressRecord.City,  
                HouseNumber = addressRecord.HouseNumber,
                ZipCode = addressRecord.ZipCode,
            };
            _context.Addresses.Add(address);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAddress), address);
        }

       
    }

    public class AddressesRecords
    {
        public record CreateAddressRecord(string Street, string City, string? HouseNumber, string ZipCode);

     }
}
