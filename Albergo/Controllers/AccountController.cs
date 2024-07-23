
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Data;
using System.Data.SqlClient;
using HotelReservationSystem.Data;

public class AccountController : Controller
{
    private readonly DatabaseHelper _db;

    public AccountController(IConfiguration configuration)
    {
        _db = new DatabaseHelper(configuration.GetConnectionString("DefaultConnection"));
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(string username, string password)
    {
        string query = "SELECT * FROM Utenti WHERE Username = @Username";
        SqlParameter[] parameters = { new SqlParameter("@Username", username) };
        DataTable userTable = _db.ExecuteQuery(query, parameters);

        if (userTable.Rows.Count == 1)
        {
            DataRow userRow = userTable.Rows[0];
            string hashedPassword = userRow["PasswordHash"].ToString();
            if (PasswordHelper.VerifyPassword(password, hashedPassword))
            {
                HttpContext.Session.SetString("Username", username);
                HttpContext.Session.SetString("Role", userRow["Ruolo"].ToString());
                return RedirectToAction("Index", "Home");
            }
        }

        ViewBag.Message = "Invalid username or password";
        return View();
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }
}

