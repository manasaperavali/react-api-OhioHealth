using Microsoft.AspNetCore.Mvc;
using BackendAPI.Data;
using BackendAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/user - Save a new user
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            if (user == null)
                return BadRequest("Invalid user data.");

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        // GET: api/user/{id} - Retrieve a user by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound("User not found.");

            return Ok(user);
        }

        // GET: api/user - Retrieve all users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            var users = await _context.Users.ToListAsync();
            return Ok(users);
        }

        // GET: api/user/filter?firstName=x&cityName=y&yearOfJoining=z
        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<User>>> FilterUsers([FromQuery] string? firstName, [FromQuery] string? cityName, [FromQuery] int? yearOfJoining)
        {
            var query = _context.Users.AsQueryable();

            if (!string.IsNullOrWhiteSpace(firstName))
                query = query.Where(u => u.FirstName.ToLower().Contains(firstName.ToLower()));

            if (!string.IsNullOrWhiteSpace(cityName))
                query = query.Where(u => u.CityName.ToLower().Contains(cityName.ToLower()));

            if (yearOfJoining.HasValue)
                query = query.Where(u => u.YearOfJoining == yearOfJoining);

            var results = await query.ToListAsync();

            return Ok(results);
        }

        // PUT: api/user/{id} - Update an existing user
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User updatedUser)
        {
            if (updatedUser == null || id != updatedUser.Id)
                return BadRequest("Invalid user data.");

            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound("User not found.");

            user.FirstName = updatedUser.FirstName;
            user.CityName = updatedUser.CityName;
            user.YearOfJoining = updatedUser.YearOfJoining;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/user/{id} - Delete a user
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound("User not found.");

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

