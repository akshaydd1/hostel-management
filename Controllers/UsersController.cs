using Microsoft.AspNetCore.Mvc;
using HostelManagementApi.DTOs.Requests;
using HostelManagementApi.DTOs.Responses;
using HostelManagementApi.Services.Interfaces;

namespace HostelManagementApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET /api/User/view
        [HttpGet("view")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(ApiResponse<IEnumerable<UserResponse>>.SuccessResponse(users));
        }

        // GET /api/User/view/{id}
        [HttpGet("view/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound(ApiResponse<UserResponse>.FailResponse($"User with ID {id} not found."));

            return Ok(ApiResponse<UserResponse>.SuccessResponse(user));
        }

        // POST /api/User/insert
        [HttpPost("insert")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
        {
            var user = await _userService.CreateUserAsync(request);
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id },
                ApiResponse<UserResponse>.SuccessResponse(user, "User created successfully."));
        }

        // POST /api/User/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _userService.LoginAsync(request);
            if (user == null)
                return Unauthorized(ApiResponse<UserResponse>.FailResponse("Invalid email or password."));

            return Ok(ApiResponse<UserResponse>.SuccessResponse(user, "Login successful."));
        }

        // PUT /api/User/update/{id}
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserRequest request)
        {
            var user = await _userService.UpdateUserAsync(id, request);
            if (user == null)
                return NotFound(ApiResponse<UserResponse>.FailResponse($"User with ID {id} not found."));

            return Ok(ApiResponse<UserResponse>.SuccessResponse(user, "User updated successfully."));
        }

        // DELETE /api/User/delete/{id}
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var deleted = await _userService.DeleteUserAsync(id);
            if (!deleted)
                return NotFound(ApiResponse<string>.FailResponse($"User with ID {id} not found."));

            return Ok(ApiResponse<string>.SuccessResponse("Deleted", "User deleted successfully."));
        }
    }
}
