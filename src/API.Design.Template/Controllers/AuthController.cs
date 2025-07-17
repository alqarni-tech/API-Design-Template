using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using API.Design.Template.Core.Entities;
using API.Design.Template.Core.Interfaces;
using API.Design.Template.Application.Services;

namespace API.Design.Template.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;

        public AuthController(IUserRepository userRepository, IAuthService authService)
        {
            _userRepository = userRepository;
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _userRepository.GetByUsernameAsync(request.Username);
            if (user == null)
                return Unauthorized(new { message = "Invalid credentials" });

            if (!await _authService.ValidatePasswordAsync(request.Password, user.PasswordHash))
                return Unauthorized(new { message = "Invalid credentials" });

            var token = await _authService.GenerateJwtTokenAsync(user);
            var refreshToken = await _authService.GenerateRefreshTokenAsync();

            user.LastLoginAt = DateTime.UtcNow;
            await _userRepository.UpdateAsync(user);

            return Ok(new
            {
                token,
                refreshToken,
                user = new
                {
                    user.Id,
                    user.Username,
                    user.Email,
                    user.Role
                }
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (await _userRepository.GetByUsernameAsync(request.Username) != null)
                return BadRequest(new { message = "Username already exists" });

            if (await _userRepository.GetByEmailAsync(request.Email) != null)
                return BadRequest(new { message = "Email already exists" });

            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = request.Username,
                Email = request.Email,
                PasswordHash = await _authService.HashPasswordAsync(request.Password),
                Role = "User",
                CreatedAt = DateTime.UtcNow
            };

            await _userRepository.AddAsync(user);

            var token = await _authService.GenerateJwtTokenAsync(user);
            var refreshToken = await _authService.GenerateRefreshTokenAsync();

            return Ok(new
            {
                token,
                refreshToken,
                user = new
                {
                    user.Id,
                    user.Username,
                    user.Email,
                    user.Role
                }
            });
        }
    }

    public class LoginRequest
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }

    public class RegisterRequest
    {
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
} 