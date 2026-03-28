using Microsoft.AspNetCore.Mvc;
using HostelManagementApi.DTOs.Requests;
using HostelManagementApi.DTOs.Responses;
using HostelManagementApi.Services.Interfaces;

namespace HostelManagementApi.Controllers
{
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET /api/users
        [HttpGet("api/users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(ApiResponse<IEnumerable<UserResponse>>.SuccessResponse(users));
        }

        // GET /api/user/{id}
        [HttpGet("api/user/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound(ApiResponse<UserResponse>.FailResponse($"User with ID {id} not found."));

            return Ok(ApiResponse<UserResponse>.SuccessResponse(user));
        }

        // POST /api/insertuser
        [HttpPost("api/insertuser")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
        {
            var user = await _userService.CreateUserAsync(request);
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id },
                ApiResponse<UserResponse>.SuccessResponse(user, "User created successfully."));
        }

        // POST /api/login
        [HttpPost("api/login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _userService.LoginAsync(request);
            if (user == null)
                return Unauthorized(ApiResponse<UserResponse>.FailResponse("Invalid email or password."));

            return Ok(ApiResponse<UserResponse>.SuccessResponse(user, "Login successful."));
        }

        // PUT /api/updateuser/{id}
        [HttpPut("api/updateuser/{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserRequest request)
        {
            var user = await _userService.UpdateUserAsync(id, request);
            if (user == null)
                return NotFound(ApiResponse<UserResponse>.FailResponse($"User with ID {id} not found."));

            return Ok(ApiResponse<UserResponse>.SuccessResponse(user, "User updated successfully."));
        }

        // DELETE /api/deleteuser/{id}
        [HttpDelete("api/deleteuser/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var deleted = await _userService.DeleteUserAsync(id);
            if (!deleted)
                return NotFound(ApiResponse<string>.FailResponse($"User with ID {id} not found."));

            return Ok(ApiResponse<string>.SuccessResponse("Deleted", "User deleted successfully."));
        }
    }
}
