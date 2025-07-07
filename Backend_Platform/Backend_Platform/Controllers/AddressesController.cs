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
                StreetAndHouseNumber = addressRecord.StreetAndHouseNumber,
                City = addressRecord.City,  
                ZipCode = addressRecord.ZipCode,
            };
            _context.Addresses.Add(address);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAddress), address);
        }
        // PUT: api/Adresses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAddress(Guid id, AddressesRecords.UpdateAddressRecord record)
        {
            if (id != record.Id)
            {
                return BadRequest();
            }
            var address = await _context.Addresses.FirstOrDefaultAsync(u => u.Id == id);
            if (address == null)
            {
                return NotFound();
            }


           address.City  = record.City;
            address.ZipCode = record.ZipCode;
            address.StreetAndHouseNumber = record.StreetAndHouseNumber;


            _context.Entry(address).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddressExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Addresses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddresses(Guid id)
        {
            var address = await _context.Addresses.FindAsync(id);
            if (address == null)
            {
                return NotFound();
            }

            _context.Addresses.Remove(address);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AddressExists(Guid id)
        {
            return _context.Addresses.Any(e => e.Id == id);
        }

    }

    public class AddressesRecords
    {
        public record CreateAddressRecord(string StreetAndHouseNumber, string City, string ZipCode);
        public record UpdateAddressRecord(Guid Id, string StreetAndHouseNumber, string City, string ZipCode);

    }
}
