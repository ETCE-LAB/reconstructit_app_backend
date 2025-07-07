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
    public class PrintContractsController : ControllerBase
    {
        private readonly DBContext _context;
        public PrintContractsController(DBContext context) => _context = context;

        [HttpPost]
        public async Task<ActionResult<PrintContract>> PostPrintContract(PrintContractRecords.CreatePrintContractRecord record)
        {
            var contract = new PrintContract
            {
                CommunityPrintRequestId = record.CommunityPrintRequestId,
                ContractStatus = record.ContractStatus,
                ShippingStatus = record.ShippingStatus,
                RevealedAddressId=record.RevealedAddressId,
            };
            _context.PrintContracts.Add(contract);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPrintContractsByRequest), new { id = record.CommunityPrintRequestId }, contract);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrintContract(Guid id, PrintContractRecords.UpdatePrintContractRecord record)
        {
            if (id != record.Id) return BadRequest();

            var contract = await _context.PrintContracts.FindAsync(id);
            if (contract == null) return NotFound();

            contract.ContractStatus = record.ContractStatus;
            contract.ShippingStatus = record.ShippingStatus;
            contract.RevealedAddressId = record.RevealedAddressId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("/api/CommunityPrintRequest/{id}/PrintContracts")]
        public async Task<ActionResult<IEnumerable<PrintContract>>> GetPrintContractsByRequest(Guid id)
        {
            var contracts =  await _context.PrintContracts
                .Where(p => p.CommunityPrintRequestId == id)
                .ToListAsync();

            return Content(JsonSerializer.Serialize(contracts, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            }), "application/json"); ; ;
        }
    }

    public class PrintContractRecords
    {
        public record CreatePrintContractRecord(Guid CommunityPrintRequestId, PrintContractStatus ContractStatus, ShippingStatus ShippingStatus, Guid? RevealedAddressId);
        public record UpdatePrintContractRecord(Guid Id, PrintContractStatus ContractStatus, ShippingStatus ShippingStatus, Guid? RevealedAddressId);
    }
}
