namespace HotelReservationSystem.Models
{
    public class ServizioPrenotato
    {
        public int ID { get; set; }
        public int IDPrenotazione { get; set; }
        public int IDServizio { get; set; }
        public DateTime DataServizio { get; set; }
        public int Quantità { get; set; }
        public decimal PrezzoTotale { get; set; }
    }
}
