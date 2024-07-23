namespace HotelReservationSystem.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using Microsoft.IdentityModel.Tokens;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using HotelReservationSystem.Data;
    using HotelReservationSystem.Models;

    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly DatabaseHelper _db;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IConfiguration configuration, ILogger<AccountController> logger)
        {
            _db = new DatabaseHelper(configuration.GetConnectionString("DefaultConnection"));
            _configuration = configuration;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginModel model)
        {
            _logger.LogInformation("Login attempt for user {Username}", model.Username);

            string query = "SELECT * FROM Utenti WHERE Username = @Username";
            SqlParameter[] parameters = { new SqlParameter("@Username", model.Username) };
            DataTable userTable = _db.ExecuteQuery(query, parameters);

            if (userTable.Rows.Count == 1)
            {
                DataRow userRow = userTable.Rows[0];
                string hashedPassword = userRow["PasswordHash"].ToString();
                if (PasswordHelper.VerifyPassword(model.Password, hashedPassword))
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new[]
                        {
                            new Claim(ClaimTypes.Name, model.Username),
                            new Claim(ClaimTypes.Role, userRow["Ruolo"].ToString())
                        }),
                        Expires = DateTime.UtcNow.AddHours(1),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    _logger.LogInformation("Login successful for user {Username}", model.Username);
                    return Ok(new { Token = tokenHandler.WriteToken(token) });
                }
                else
                {
                    _logger.LogWarning("Invalid password for user {Username}", model.Username);
                }
            }
            else
            {
                _logger.LogWarning("User not found: {Username}", model.Username);
            }

            return Unauthorized(new { Message = "Invalid credentials" });
        }
    }
}
