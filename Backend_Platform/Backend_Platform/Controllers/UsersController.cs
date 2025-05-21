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
    public class UsersController : ControllerBase
    {
        private readonly DBContext _context;
       private readonly IClaimsService _claimsService;
        public UsersController(DBContext context, IClaimsService claimsService)
        {
            _context = context;
            _claimsService = claimsService;
        }


        // GET: api/Application-users/5/User
        [HttpGet("/api/application-users/{accountId}/user")]
        public async Task<ActionResult<User>> GetUserForAccount(string accountId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.UserAccountId == accountId);
            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(string id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Content(JsonSerializer.Serialize(user, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            }), "application/json"); ; ;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(Guid id, UserRecords.UpdateUserProfileRecord record)
        {
            if (id != record.Id)
            {
                return BadRequest();
            }
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }



            user.Id = id;
            user.AddressId = record.AddressId;
                user.FirstName = record.FirstName;
            user.LastName = record.LastName;
            user.UserProfilePictureUrl = record.UserProfilePictureUrl;
            user.Region    = record.Region;
           

            _context.Entry(user).State = EntityState.Modified;

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

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(UserRecords.CreateUserRecord userRecord)
        {
            
            var userAccountId = _claimsService.GetUserAccountId(User);
           if (userAccountId == null)
            {
                return NotFound();
            }
          
            if (userRecord.UserAccountId != userAccountId)
            {
                return BadRequest();
            }
            
            
            var user = new User(){
                UserAccountId= userRecord.UserAccountId,
                FirstName = userRecord.FirstName,
                LastName = userRecord.LastName,
                UserProfilePictureUrl = userRecord.UserProfilePictureUrl,
                Region = userRecord.Region
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUserForAccount), new { accountId = user.UserAccountId }, user);
        }

  

        private bool UserExists(Guid id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }

    public class UserRecords
    {
        public record CreateUserRecord(string? UserProfilePictureUrl, Guid? AddressId, string Region, string FirstName, string LastName,  string UserAccountId);

        public record UpdateUserProfileRecord(Guid Id, string? UserProfilePictureUrl , Guid? AddressId, string Region, string FirstName, string LastName);
    }
}

