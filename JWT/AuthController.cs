using BackendProject.Data;
using firstback.JWT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace firstback.user
{
    [Route("api/auth")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly AuthService _authService;

        public AuthController(ApplicationDbContext context, AuthService authService)
        {
            _context = context;
            _authService = authService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] UserRegisterDTO userDTO)
        {
            var existingUser = _context.Users.FirstOrDefault(u => u.Email == userDTO.Email);
            if (existingUser != null) // Usuario ya existe
            {
                return BadRequest("User already exists");
            }

            // Hash de la contraseña
            if (string.IsNullOrEmpty(userDTO.Password))
            {
                return BadRequest("Password cannot be null or empty");
            }
            var hashedPassword = Convert.ToBase64String(SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(userDTO.Password)));

            var newUser = new User
            {
                Email = userDTO.Email,
                Password = hashedPassword,
                FirstName = userDTO.FirstName,
                LastName = userDTO.LastName,
                RoleId = 2
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();

            return Ok(userDTO);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginDTO userLoginDTO)
        {
            var existingUser = _context.Users.FirstOrDefault(u => u.Email == userLoginDTO.Email);
            if (existingUser == null)
            {
                return Unauthorized("User not found");
            }

            if (string.IsNullOrEmpty(userLoginDTO.Password))
            {
                return BadRequest("Password cannot be null or empty");
            }
            var hashedPassword = Convert.ToBase64String(SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(userLoginDTO.Password)));

            if (existingUser.Password != hashedPassword)
            {
                return Unauthorized("Invalid password");
            }

            var token = _authService.GenerateJwtToken(existingUser);
            return Ok(new { token });
        }
    }
}