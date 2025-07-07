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
    public class ItemsController : ControllerBase
    {
        private readonly DBContext _context;
        public ItemsController(DBContext context)
        {
            _context = context;
            
        }

        // GET: api/Users/5/Items
        [HttpGet("/api/Users/{userId}/Items")]
        public async Task<ActionResult<ICollection<Item>>> GetItemsForUser(Guid userId)
        {
            var items = await _context.Items
                .Where(item => item.UserId == userId)
                .ToListAsync();


            return Content(JsonSerializer.Serialize(items, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            }), "application/json"); ; ;
        }

        // GET: api/Items/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItem(Guid id)
        {
            var item = await _context.Items.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return Content(JsonSerializer.Serialize(item, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            }), "application/json"); ; ;
        }

        // PUT: api/Items/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItem(Guid id, ItemRecords.UpdateItemRecord record)
        {
            if (id != record.Id)
            {
                return BadRequest();
            }
            var item = await _context.Items.FirstOrDefaultAsync(u => u.Id == id);
            if (item == null)
            {
                return NotFound();
            }



            item.Status = record.Status;
            item.UserId = record.UserId;
            item.CommunityPrintRequestId = record.CommunityPrintRequestId;
            item.Title = record.Title;
            item.Description = record.Description;
            item.ConstructionFileId = record.ConstructionFileId;
               
            _context.Entry(item).State = EntityState.Modified;

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

        // POST: api/Items
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Item>> PostItem(ItemRecords.CreateItemRecord requestRecord)
        {
            
            var item = new Item(){
               Status  = requestRecord.Status,
               UserId = requestRecord.UserId,
               Title = requestRecord.Title, 
               Description      = requestRecord.Description,
               ConstructionFileId = requestRecord.ConstructionFileId,   
            };
            _context.Items.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetItem), new { id = item.Id }, item);
        }
        
        private bool RequestExists(Guid id)
        {
            return _context.Items.Any(e => e.Id == id);
        }
    }

    public class ItemRecords
    {
        public record CreateItemRecord(RepairStatus Status, Guid UserId, string Title, string Description, Guid? ConstructionFileId);

        public record UpdateItemRecord(Guid Id, RepairStatus Status, Guid UserId, Guid? CommunityPrintRequestId, string Title, string Description, Guid? ConstructionFileId);
    }
}