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
    public class ParticipantsController : ControllerBase
    {
        private readonly DBContext _context;
        public ParticipantsController(DBContext context)
        {
            _context = context;
            
        }

        // GET: api/User/id/Participants
        [HttpGet("/api/Users/{userId}/Participants")]
        public async Task<ActionResult<ICollection<Participant>>> GetParticipantsForUser(Guid userId)
        {
            var participants = await _context.Participants.FirstOrDefaultAsync(participant => participant.UserId ==userId );

            return Content(JsonSerializer.Serialize(participants, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            }), "application/json"); ; ;
        }

        // GET: api/PrintContract/id/Participants
        [HttpGet("/api/PrintContract/{contractId}/Participants")]
        public async Task<ActionResult<ICollection<Participant>>> GetParticipantsForPrintContract(Guid contractId)
        {
            var participants = await _context.Participants.FirstOrDefaultAsync(participant => participant.PrintContractId == contractId);

            return Content(JsonSerializer.Serialize(participants, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            }), "application/json"); ; ;
        }


        // GET: api/Participants/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Participant>> GetParticipant(string id)
        {
            var participant = await _context.Participants.FindAsync(id);

            if (participant == null)
            {
                return NotFound();
            }

            return Content(JsonSerializer.Serialize(participant, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            }), "application/json"); ; ;
        }

     

        // POST: api/Participants
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Participant>> PostParticipant(ParticipantRecords.CreateParticipantRecord participantRecord)
        {
            
            var participant = new Participant(){
               Role  = participantRecord.Role,
               UserId = participantRecord.UserId,
               PrintContractId   = participantRecord.PrintContractId,
            };
            _context.Participants.Add(participant);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetParticipant), new { id = participant.Id }, participant);
        }
       

        private bool ParticipantExists(Guid id)
        {
            return _context.Participants.Any(e => e.Id == id);
        }
    }

    public class ParticipantRecords
    {
        public record CreateParticipantRecord(ParticipantRole Role, Guid? UserId,  Guid PrintContractId);
    }
}