namespace HotelReservationSystem.Models
{
    public class Utente
    {
        public int IDUtente { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Ruolo { get; set; }
    }
}
