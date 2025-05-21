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
    public class MessagesController : ControllerBase
    {
        private readonly DBContext _context;
        public MessagesController(DBContext context)
        {
            _context = context;
            
        }

        // GET: api/Chats/id/Messages
        [HttpGet("/api/Chats/{chatId}/Messages")]
        public async Task<ActionResult<ICollection<Message>>> GetMessagesForChat(Guid chatId)
        {
            var chat = await _context.Messages.FirstOrDefaultAsync(message => message.ChatId ==chatId );

            return Content(JsonSerializer.Serialize(chat, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            }), "application/json"); ; ;
        }

        // GET: api/Messages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Message>> GetMessage(string id)
        {
            var chat = await _context.Messages.FindAsync(id);

            if (chat == null)
            {
                return NotFound();
            }

            return Content(JsonSerializer.Serialize(chat, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            }), "application/json"); ; ;
        }

     

        // POST: api/Messages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Message>> PostMessage(MessageRecords.CreateMessageRecord messageRecord)
        {
            
            var message = new Message(){
               Content  = messageRecord.Content,
               SentAt = messageRecord.SentAt,
               ParticipantId = messageRecord.ParticipantId, 
               ChatId   = messageRecord.ChatId,
            };
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMessage), new { id = message.Id }, message);
        }
       

        private bool RequestExists(Guid id)
        {
            return _context.Messages.Any(e => e.Id == id);
        }
    }

    public class MessageRecords
    {
        public record CreateMessageRecord(string Content, DateTime SentAt, Guid? ParticipantId, Guid ChatId);
    }
}