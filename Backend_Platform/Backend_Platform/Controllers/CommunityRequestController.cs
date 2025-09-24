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
    public class CommunityPrintRequestsController : ControllerBase
    {
        private readonly DBContext _context;
        public CommunityPrintRequestsController(DBContext context)
        {
            _context = context;
            
        }


        // GET: api/CommunityPrintRequests
        [HttpGet()]
        public async Task<ActionResult<ICollection<CommunityPrintRequest>>> GetCommunityPrintRequests()
        {
            var requests = await _context.CommunityPrintRequests.ToListAsync();
            // return the newest at first
            requests.Reverse();
            return requests;
        }

        // GET: api/CommunityPrintRequests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CommunityPrintRequest>> GetCommunityPrintRequest(Guid id)
        {
            var request = await _context.CommunityPrintRequests.FindAsync(id);

            if (request == null)
            {
                return NotFound();
            }

            return Content(JsonSerializer.Serialize(request, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            }), "application/json"); ; ;
        }

        // PUT: api/CommunityPrintRequests/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCommunityPrintRequest(Guid id, CommunityPrintRequestRecords.UpdateCommunityPrintRequestRecord record)
        {
            if (id != record.Id)
            {
                return BadRequest();
            }
            var request = await _context.CommunityPrintRequests.FirstOrDefaultAsync(u => u.Id == id);
            if (request == null)
            {
                return NotFound();
            }

            // update price if material changed if the model weight feature is build
            request.Id = id;
            request.ItemId = record.ItemId;
            request.PrintMaterial   =record.PrintMaterial;
               
            _context.Entry(request).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestExists(id))
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

        // POST: api/CommunityPrintRequests
        [HttpPost]
        public async Task<ActionResult<CommunityPrintRequest>> PostCommunityPrintRequest(CommunityPrintRequestRecords.CreateCommunityPrintRequestRecord requestRecord)
        {
            // When including the weight for an model, the price should be build with it          
            var communityPrintRequest = new CommunityPrintRequest(){
               PriceMax  = 100.0,
               ItemId = requestRecord.ItemId,
               PrintMaterial = requestRecord.PrintMaterial,
            };
            _context.CommunityPrintRequests.Add(communityPrintRequest);
            await _context.SaveChangesAsync();
            var item = await _context.Items.FindAsync(requestRecord.ItemId);
            if (item != null)
            {
                item.CommunityPrintRequestId = communityPrintRequest.Id;
                await _context.SaveChangesAsync();
            }

            return CreatedAtAction(nameof(GetCommunityPrintRequest), new { id = communityPrintRequest.Id }, communityPrintRequest);
        }
        // DELETE: api/CommunityPrintRequests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCommunityPrintRequests(Guid id)
        {
            var request = await _context.CommunityPrintRequests.FindAsync(id);
            if (request == null)
            {
                return NotFound();
            }

            _context.CommunityPrintRequests.Remove(request);
            await _context.SaveChangesAsync();
            var item = await _context.Items.FindAsync(request.ItemId);
            if (item != null)
            {
                item.CommunityPrintRequestId = null;
                await _context.SaveChangesAsync();
            }

            return NoContent();
        }

        private bool RequestExists(Guid id)
        {
            return _context.CommunityPrintRequests.Any(e => e.Id == id);
        }
    }

    public class CommunityPrintRequestRecords
    {
        public record CreateCommunityPrintRequestRecord(Guid ItemId, PrintMaterial PrintMaterial);

        public record UpdateCommunityPrintRequestRecord(Guid Id, Guid ItemId, PrintMaterial PrintMaterial);
    }
}
