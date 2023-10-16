using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security;
using System.Security.Claims;
using System.Security.Cryptography;
using Vagtplan.Data;
using Vagtplan.Models;

namespace Vagtplan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ShiftPlannerContext _context;
        public static User user = new User();
        private readonly IConfiguration _configuration;

        public AuthController(ShiftPlannerContext dbContext,IConfiguration configuration)
        {
            _context = dbContext;
            _configuration = configuration;
        }


        [HttpGet]
        public IActionResult GetEmployees()
        {

            var users = _context.Users.ToList();
            return Ok(users);
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDto request) {

            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            user.Username = request.UserName;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            var userFromDb = _context.Users.FirstOrDefault(u => u.Username == request.UserName);

            if (userFromDb != null)
            {
                return BadRequest("User  already exists");
            } else
            {
                _context.Users.Add(user);
                _context.SaveChanges();
                return Ok(user);
            }

        }


        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserDto request)
        {
           
            var userFromDb = _context.Users.FirstOrDefault(u => u.Username == request.UserName);
            
            if (userFromDb == null || userFromDb.Username != request.UserName)
            {
                await Console.Out.WriteLineAsync("the user is" + userFromDb);
                return BadRequest("User not found");
            }


            if(!VerifyPasswordHash(request.Password, userFromDb.PasswordHash, userFromDb.PasswordSalt))
            {
                return BadRequest("Wrong password");

            }

            string token = CreateToken(userFromDb);
            return Ok(token);

        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username)

            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));


            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);


            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;           

        }



        private void CreatePasswordHash(String password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            }

        }


        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                return computedHash.SequenceEqual(passwordHash);

            }

        }


    }
}
