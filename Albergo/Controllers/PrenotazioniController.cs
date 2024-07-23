// Controllers/PrenotazioniController.cs
using HotelReservationSystem.Data;
using HotelReservationSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

public class PrenotazioniController : Controller
{
    private readonly DatabaseHelper _db;

    public PrenotazioniController(IConfiguration configuration)
    {
        _db = new DatabaseHelper(configuration.GetConnectionString("DefaultConnection"));
    }

    public IActionResult Index()
    {
        string query = "SELECT * FROM Prenotazioni";
        DataTable prenotazioni = _db.ExecuteQuery(query);
        return View(prenotazioni);
    }

    public IActionResult Details(int id)
    {
        string query = "SELECT * FROM Prenotazioni WHERE IDPrenotazione = @IDPrenotazione";
        SqlParameter[] parameters = { new SqlParameter("@IDPrenotazione", id) };
        DataTable prenotazione = _db.ExecuteQuery(query, parameters);
        return View(prenotazione);
    }

    [HttpPost]
    public IActionResult Create(Prenotazione prenotazione)
    {
        string query = "INSERT INTO Prenotazioni (CodiceFiscale, NumeroCamera, DataPrenotazione, NumeroProgressivoAnno, Anno, DataInizioSoggiorno, DataFineSoggiorno, CaparraConfirmatoria, TariffaApplicata, TipoSoggiorno) " +
                       "VALUES (@CodiceFiscale, @NumeroCamera, @DataPrenotazione, @NumeroProgressivoAnno, @Anno, @DataInizioSoggiorno, @DataFineSoggiorno, @CaparraConfirmatoria, @TariffaApplicata, @TipoSoggiorno)";
        SqlParameter[] parameters = {
            new SqlParameter("@CodiceFiscale", prenotazione.CodiceFiscale),
            new SqlParameter("@NumeroCamera", prenotazione.NumeroCamera),
            new SqlParameter("@DataPrenotazione", prenotazione.DataPrenotazione),
            new SqlParameter("@NumeroProgressivoAnno", prenotazione.NumeroProgressivoAnno),
            new SqlParameter("@Anno", prenotazione.Anno),
            new SqlParameter("@DataInizioSoggiorno", prenotazione.DataInizioSoggiorno),
            new SqlParameter("@DataFineSoggiorno", prenotazione.DataFineSoggiorno),
            new SqlParameter("@CaparraConfirmatoria", prenotazione.CaparraConfirmatoria),
            new SqlParameter("@TariffaApplicata", prenotazione.TariffaApplicata),
            new SqlParameter("@TipoSoggiorno", prenotazione.TipoSoggiorno)
        };
        _db.ExecuteNonQuery(query, parameters);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Edit(Prenotazione prenotazione)
    {
        string query = "UPDATE Prenotazioni SET CodiceFiscale = @CodiceFiscale, NumeroCamera = @NumeroCamera, DataPrenotazione = @DataPrenotazione, NumeroProgressivoAnno = @NumeroProgressivoAnno, " +
                       "Anno = @Anno, DataInizioSoggiorno = @DataInizioSoggiorno, DataFineSoggiorno = @DataFineSoggiorno, CaparraConfirmatoria = @CaparraConfirmatoria, TariffaApplicata = @TariffaApplicata, TipoSoggiorno = @TipoSoggiorno " +
                       "WHERE IDPrenotazione = @IDPrenotazione";
        SqlParameter[] parameters = {
            new SqlParameter("@IDPrenotazione", prenotazione.IDPrenotazione),
            new SqlParameter("@CodiceFiscale", prenotazione.CodiceFiscale),
            new SqlParameter("@NumeroCamera", prenotazione.NumeroCamera),
            new SqlParameter("@DataPrenotazione", prenotazione.DataPrenotazione),
            new SqlParameter("@NumeroProgressivoAnno", prenotazione.NumeroProgressivoAnno),
            new SqlParameter("@Anno", prenotazione.Anno),
            new SqlParameter("@DataInizioSoggiorno", prenotazione.DataInizioSoggiorno),
            new SqlParameter("@DataFineSoggiorno", prenotazione.DataFineSoggiorno),
            new SqlParameter("@CaparraConfirmatoria", prenotazione.CaparraConfirmatoria),
            new SqlParameter("@TariffaApplicata", prenotazione.TariffaApplicata),
            new SqlParameter("@TipoSoggiorno", prenotazione.TipoSoggiorno)
        };
        _db.ExecuteNonQuery(query, parameters);
        return RedirectToAction("Index");
    }

    public IActionResult Delete(int id)
    {
        string query = "DELETE FROM Prenotazioni WHERE IDPrenotazione = @IDPrenotazione";
        SqlParameter[] parameters = { new SqlParameter("@IDPrenotazione", id) };
        _db.ExecuteNonQuery(query, parameters);
        return RedirectToAction("Index");
    }
}
