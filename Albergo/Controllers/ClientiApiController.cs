namespace HotelReservationSystem.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using HotelReservationSystem.Data;
    using HotelReservationSystem.Models;

    [ApiController]
    [Route("api/[controller]")]
    public class ClientiApiController : ControllerBase
    {
        private readonly DatabaseHelper _db;

        public ClientiApiController(IConfiguration configuration)
        {
            _db = new DatabaseHelper(configuration.GetConnectionString("DefaultConnection"));
        }

        [HttpGet]
        public IEnumerable<Cliente> Get()
        {
            string query = "SELECT * FROM Clienti";
            DataTable dataTable = _db.ExecuteQuery(query);
            List<Cliente> clienti = new List<Cliente>();

            foreach (DataRow row in dataTable.Rows)
            {
                clienti.Add(new Cliente
                {
                    CodiceFiscale = row["CodiceFiscale"].ToString(),
                    Cognome = row["Cognome"].ToString(),
                    Nome = row["Nome"].ToString(),
                    Città = row["Città"].ToString(),
                    Provincia = row["Provincia"].ToString(),
                    Email = row["Email"].ToString(),
                    Telefono = row["Telefono"].ToString(),
                    Cellulare = row["Cellulare"].ToString()
                });
            }

            return clienti;
        }

        [HttpGet("{id}")]
        public ActionResult<Cliente> Get(string id)
        {
            string query = "SELECT * FROM Clienti WHERE CodiceFiscale = @CodiceFiscale";
            SqlParameter[] parameters = { new SqlParameter("@CodiceFiscale", id) };
            DataTable dataTable = _db.ExecuteQuery(query, parameters);

            if (dataTable.Rows.Count == 0)
            {
                return NotFound();
            }

            DataRow row = dataTable.Rows[0];
            Cliente cliente = new Cliente
            {
                CodiceFiscale = row["CodiceFiscale"].ToString(),
                Cognome = row["Cognome"].ToString(),
                Nome = row["Nome"].ToString(),
                Città = row["Città"].ToString(),
                Provincia = row["Provincia"].ToString(),
                Email = row["Email"].ToString(),
                Telefono = row["Telefono"].ToString(),
                Cellulare = row["Cellulare"].ToString()
            };

            return cliente;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Cliente cliente)
        {
            string query = "INSERT INTO Clienti (CodiceFiscale, Cognome, Nome, Città, Provincia, Email, Telefono, Cellulare) " +
                           "VALUES (@CodiceFiscale, @Cognome, @Nome, @Città, @Provincia, @Email, @Telefono, @Cellulare)";
            SqlParameter[] parameters = {
                new SqlParameter("@CodiceFiscale", cliente.CodiceFiscale),
                new SqlParameter("@Cognome", cliente.Cognome),
                new SqlParameter("@Nome", cliente.Nome),
                new SqlParameter("@Città", cliente.Città),
                new SqlParameter("@Provincia", cliente.Provincia),
                new SqlParameter("@Email", cliente.Email),
                new SqlParameter("@Telefono", cliente.Telefono),
                new SqlParameter("@Cellulare", cliente.Cellulare)
            };
            _db.ExecuteNonQuery(query, parameters);
            return CreatedAtAction(nameof(Get), new { id = cliente.CodiceFiscale }, cliente);
        }

        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] Cliente cliente)
        {
            string query = "UPDATE Clienti SET Cognome = @Cognome, Nome = @Nome, Città = @Città, Provincia = @Provincia, " +
                           "Email = @Email, Telefono = @Telefono, Cellulare = @Cellulare WHERE CodiceFiscale = @CodiceFiscale";
            SqlParameter[] parameters = {
                new SqlParameter("@CodiceFiscale", id),
                new SqlParameter("@Cognome", cliente.Cognome),
                new SqlParameter("@Nome", cliente.Nome),
                new SqlParameter("@Città", cliente.Città),
                new SqlParameter("@Provincia", cliente.Provincia),
                new SqlParameter("@Email", cliente.Email),
                new SqlParameter("@Telefono", cliente.Telefono),
                new SqlParameter("@Cellulare", cliente.Cellulare)
            };
            _db.ExecuteNonQuery(query, parameters);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            string query = "DELETE FROM Clienti WHERE CodiceFiscale = @CodiceFiscale";
            SqlParameter[] parameters = { new SqlParameter("@CodiceFiscale", id) };
            _db.ExecuteNonQuery(query, parameters);
            return NoContent();
        }
    }
}
