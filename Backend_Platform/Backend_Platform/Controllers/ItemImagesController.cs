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
    public class ItemImagesController : ControllerBase
    {
        private readonly DBContext _context;
        public ItemImagesController(DBContext context)
        {
            _context = context;
            
        }



        // GET: api/Items/5/ItemImages
        [HttpGet("/api/Items/{id}/ItemImages")]
        public async Task<ActionResult<ICollection<ItemImage>>> GetItemImagesForItems(Guid itemId)
        {
            var itemImage = await _context.ItemImages.Where(itemImage => itemImage.ItemId == itemId).ToListAsync();

            if (itemImage == null)
            {
                return NotFound();
            }

            return Content(JsonSerializer.Serialize(itemImage, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            }), "application/json"); ; ;
        }

       
        // POST: api/ItemImages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ItemImage>> PostItemImage(ItemImageRecords.CreateItemImageRecord itemImageRecord)
        {
            
            var itemImage = new ItemImage(){
               ImageUrl  = itemImageRecord.ImageUrl,
               ItemId = itemImageRecord.ItemId,
            };
            _context.ItemImages.Add(itemImage);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetItemImagesForItems), new { itemId = itemImageRecord.ItemId }, itemImage);
        }
        // DELETE: api/ItemImages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemImages(Guid id)
        {
            var request = await _context.ItemImages.FindAsync(id);
            if (request == null)
            {
                return NotFound();
            }

            _context.ItemImages.Remove(request);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RequestExists(Guid id)
        {
            return _context.ItemImages.Any(e => e.Id == id);
        }
    }

    public class ItemImageRecords
    {
        public record CreateItemImageRecord(string ImageUrl, Guid ItemId);

    }
}
