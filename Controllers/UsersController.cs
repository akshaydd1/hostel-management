using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HostelManagementApi.Models;

namespace HostelManagementApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly HostelDbContext _db;

        public UsersController(HostelDbContext db)
        {
            _db = db;
        }

        // GET /api/users
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _db.Users.ToListAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return Problem($"Error fetching users: {ex.Message}\nInner: {ex.InnerException?.Message}");
            }
        }

        // GET /api/users/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            try
            {
                var user = await _db.Users.FindAsync(id);
                if (user == null)
                    return NotFound(new { error = $"User with ID {id} not found." });

                return Ok(user);
            }
            catch (Exception ex)
            {
                return Problem($"Error fetching user: {ex.Message}\nInner: {ex.InnerException?.Message}");
            }
        }

        // POST /api/users
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Name))
                    return BadRequest(new { error = "Name is required." });

                if (string.IsNullOrWhiteSpace(request.Email))
                    return BadRequest(new { error = "Email is required." });

                var user = new User
                {
                    Name = request.Name.Trim(),
                    Email = request.Email.Trim(),
                    City = request.City?.Trim(),
                    State = request.State?.Trim(),
                    DocType = request.DocType?.Trim(),
                    DocNumber = request.DocNumber?.Trim(),
                    CreatedAt = DateTimeOffset.UtcNow
                };

                _db.Users.Add(user);
                await _db.SaveChangesAsync();

                return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, new
                {
                    message = "User created successfully.",
                    user
                });
            }
            catch (Exception ex)
            {
                return Problem($"Error creating user: {ex.Message}\nInner: {ex.InnerException?.Message}");
            }
        }

        // PUT /api/users/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] CreateUserRequest request)
        {
            try
            {
                var user = await _db.Users.FindAsync(id);
                if (user == null)
                    return NotFound(new { error = $"User with ID {id} not found." });

                if (!string.IsNullOrWhiteSpace(request.Name))
                    user.Name = request.Name.Trim();
                if (!string.IsNullOrWhiteSpace(request.Email))
                    user.Email = request.Email.Trim();
                if (request.City != null)
                    user.City = request.City.Trim();
                if (request.State != null)
                    user.State = request.State.Trim();
                if (request.DocType != null)
                    user.DocType = request.DocType.Trim();
                if (request.DocNumber != null)
                    user.DocNumber = request.DocNumber.Trim();

                await _db.SaveChangesAsync();

                return Ok(new { message = "User updated successfully.", user });
            }
            catch (Exception ex)
            {
                return Problem($"Error updating user: {ex.Message}\nInner: {ex.InnerException?.Message}");
            }
        }

        // DELETE /api/users/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var user = await _db.Users.FindAsync(id);
                if (user == null)
                    return NotFound(new { error = $"User with ID {id} not found." });

                _db.Users.Remove(user);
                await _db.SaveChangesAsync();

                return Ok(new { message = "User deleted successfully." });
            }
            catch (Exception ex)
            {
                return Problem($"Error deleting user: {ex.Message}\nInner: {ex.InnerException?.Message}");
            }
        }

        // GET /api/users/test-db
        [HttpGet("test-db")]
        public async Task<IActionResult> TestDatabaseConnection()
        {
            try
            {
                var canConnect = await _db.Database.CanConnectAsync();
                if (canConnect)
                    return Ok("✅ Database connection successful! Connected to myStay database.");
                else
                    return Problem("❌ Database connection failed.");
            }
            catch (Exception ex)
            {
                return Problem($"❌ Database connection error: {ex.Message}");
            }
        }
    }
}
