using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Backend_Platform.Entities;
using Backend_Platform.Entities.enums;
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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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



            request.Id = id;
            request.ItemId = record.ItemId;
            request.PriceMax = record.PriceMax; 
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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CommunityPrintRequest>> PostCommunityPrintRequest(CommunityPrintRequestRecords.CreateCommunityPrintRequestRecord requestRecord)
        {
            
            var communityPrintRequest = new CommunityPrintRequest(){
               PriceMax  = requestRecord.PriceMax,
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
        public record CreateCommunityPrintRequestRecord(double PriceMax, Guid ItemId, PrintMaterial PrintMaterial);

        public record UpdateCommunityPrintRequestRecord(Guid Id, double PriceMax, Guid ItemId, PrintMaterial PrintMaterial);
    }
}
