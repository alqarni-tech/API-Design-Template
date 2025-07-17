using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using API.Design.Template.Core.Entities;
using API.Design.Template.Core.Interfaces;

namespace API.Design.Template.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out var id))
                return Unauthorized();

            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                return NotFound();

            return Ok(new
            {
                user.Id,
                user.Username,
                user.Email,
                user.Role,
                user.CreatedAt,
                user.LastLoginAt
            });
        }

        [HttpGet("admin/users")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUsers()
        {
            // In a real implementation, you'd have a method to get all users
            // For now, return a placeholder
            return Ok(new { message = "Admin endpoint - all users would be returned here" });
        }

        [HttpPut("profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileRequest request)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out var id))
                return Unauthorized();

            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                return NotFound();

            // Update allowed fields
            if (!string.IsNullOrEmpty(request.Email))
                user.Email = request.Email;

            await _userRepository.UpdateAsync(user);

            return Ok(new
            {
                user.Id,
                user.Username,
                user.Email,
                user.Role,
                user.CreatedAt,
                user.LastLoginAt
            });
        }
    }

    public class UpdateProfileRequest
    {
        public string? Email { get; set; }
    }
} 