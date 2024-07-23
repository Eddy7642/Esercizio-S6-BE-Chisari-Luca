namespace HotelReservationSystem.Models
{
    public class Prenotazione
    {
        public int IDPrenotazione { get; set; }
        public string CodiceFiscale { get; set; }
        public int NumeroCamera { get; set; }
        public DateTime DataPrenotazione { get; set; }
        public int NumeroProgressivoAnno { get; set; }
        public int Anno { get; set; }
        public DateTime DataInizioSoggiorno { get; set; }
        public DateTime DataFineSoggiorno { get; set; }
        public decimal CaparraConfirmatoria { get; set; }
        public decimal TariffaApplicata { get; set; }
        public string TipoSoggiorno { get; set; }
    }
}
