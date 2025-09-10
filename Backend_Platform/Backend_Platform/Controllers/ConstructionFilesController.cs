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
    public class ConstructionFilesController : ControllerBase
    {
        private readonly DBContext _context;
        public ConstructionFilesController(DBContext context)
        {
            _context = context;
            
        }


     
        // GET: api/ConstructionFiles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ConstructionFile>> GetConstructionFile(string id)
        {
            var constructionFIle = await _context.ConstructionFiles.FindAsync(id);

            if (constructionFIle == null)
            {
                return NotFound();
            }

            return Content(JsonSerializer.Serialize(constructionFIle, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            }), "application/json"); ; ;
        }

        // PUT: api/ConstructionFiles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConstructionFile(Guid id, ConstructionFileRecords.UpdateConstructionFileRecord record)
        {
            if (id != record.Id)
            {
                return BadRequest();
            }
            var request = await _context.ConstructionFiles.FirstOrDefaultAsync(u => u.Id == id);
            if (request == null)
            {
                return NotFound();
            }



            request.Id = id;
            request.ItemId = record.ItemId;
            request.CreatedAt = record.createdAt;
            request.FileUrl = record.FileUrl;
               
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

        // POST: api/ConstructionFiles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ConstructionFile>> PostConstructionFile(ConstructionFileRecords.CreateConstructionFileRecord constructionFIleRecord)
        {
            
            var constructionFile = new ConstructionFile(){
               FileUrl  = constructionFIleRecord.FileUrl,
               CreatedAt    = constructionFIleRecord.createdAt,
               ItemId = constructionFIleRecord.ItemId,
            };
            _context.ConstructionFiles.Add(constructionFile);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetConstructionFile), new { id = constructionFile.Id }, constructionFile);
        }
      

        private bool RequestExists(Guid id)
        {
            return _context.ConstructionFiles.Any(e => e.Id == id);
        }
    }

    public class ConstructionFileRecords
    {
        public record CreateConstructionFileRecord(string FileUrl, Guid ItemId, DateTime createdAt);

        public record UpdateConstructionFileRecord(Guid Id, string FileUrl, Guid ItemId, DateTime createdAt);
    }
}
