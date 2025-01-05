namespace Hotel.Models
{
    public class ReservationEtat
    {
        public int Id { get; set; }
        public string Etat { get; set; }
        public ICollection<Reservation> Reservations { get; set; }

    }

}
