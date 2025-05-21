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
    public class ChatsController : ControllerBase
    {
        private readonly DBContext _context;
       private readonly IClaimsService _claimsService;
        public ChatsController(DBContext context)
        {
            _context = context;
            
        }


        // PUT: api/CommunityPrintRequest/5/Chats
        [HttpGet("/api/CommunityPrintRequests/{accountId}/Chats")]
        public async Task<ActionResult<Chat>> GetChatForCommunityPrintRequest(Guid requestId)
        {
            var chat = await _context.Chats.FirstOrDefaultAsync(chat => chat.CommunityPrintRequestId == requestId);
            if (chat == null)
            {
                return NotFound();
            }

            return chat;
        }
        // GET: api/Chats/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Chat>> GetChat(string id)
        {
            var chat = await _context.Chats.FindAsync(id);

            if (chat == null)
            {
                return NotFound();
            }

            return Content(JsonSerializer.Serialize(chat, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            }), "application/json"); ; ;
        }

        // PUT: api/Chats/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChat(Guid id, ChatRecords.UpdateChatRecord record)
        {
            if (id != record.Id)
            {
                return BadRequest();
            }
            var chat = await _context.Chats.FirstOrDefaultAsync(u => u.Id == id);
            if (chat == null)
            {
                return NotFound();
            }



            chat.Id = id;
            chat.CommunityPrintRequestId = record.CommunityPrintRequestId;
            chat.AddressId = record.AddressId;  
               
            _context.Entry(chat).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Chats
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostChat(ChatRecords.CreateChatRecord chatRecord)
        {
            
         
            
            
            var chat = new Chat(){
               CommunityPrintRequestId  = chatRecord.CommunityPrintRequestId,
               AddressId = chatRecord.AddressId,
            };
            _context.Chats.Add(chat);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetChat), new { id = chat.Id }, chat);
        }


        private bool UserExists(Guid id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }

    public class ChatRecords
    {
        public record CreateChatRecord(Guid CommunityPrintRequestId, Guid? AddressId);

        public record UpdateChatRecord(Guid Id, Guid CommunityPrintRequestId, Guid? AddressId);
    }
}
