using HostelManagementApi.DTOs.Requests;
using HostelManagementApi.DTOs.Responses;
using HostelManagementApi.Helpers;
using HostelManagementApi.Models;
using HostelManagementApi.Repositories.Interfaces;
using HostelManagementApi.Services.Interfaces;

namespace HostelManagementApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserResponse>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return users.Select(MapToResponse);
        }

        public async Task<UserResponse?> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return user == null ? null : MapToResponse(user);
        }

        public async Task<UserResponse> CreateUserAsync(CreateUserRequest request)
        {
            var user = new User
            {
                Name = request.Name.Trim(),
                Email = request.Email.Trim(),
                City = request.City?.Trim(),
                State = request.State?.Trim(),
                DocType = request.DocType?.Trim(),
                DocNumber = request.DocNumber?.Trim(),
                MobileNo = request.MobileNo?.Trim(),
                Password = PasswordHelper.HashPassword(request.Password),
                CreatedAt = DateTimeOffset.UtcNow
            };

            var created = await _userRepository.CreateAsync(user);
            return MapToResponse(created);
        }

        public async Task<UserResponse?> UpdateUserAsync(int id, UpdateUserRequest request)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return null;

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
            if (request.MobileNo != null)
                user.MobileNo = request.MobileNo.Trim();

            var updated = await _userRepository.UpdateAsync(user);
            return MapToResponse(updated);
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return false;

            await _userRepository.DeleteAsync(user);
            return true;
        }

        public async Task<UserResponse?> LoginAsync(LoginRequest request)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email.Trim());
            if (user == null) return null;

            if (!PasswordHelper.VerifyPassword(request.Password, user.Password!))
                return null;

            return MapToResponse(user);
        }

        private static UserResponse MapToResponse(User user)
        {
            return new UserResponse
            {
                Id = user.Id,
                Name = user.Name ?? string.Empty,
                Email = user.Email ?? string.Empty,
                City = user.City,
                State = user.State,
                DocType = user.DocType,
                DocNumber = user.DocNumber,
                MobileNo = user.MobileNo,
                CreatedAt = user.CreatedAt
            };
        }
    }
}
