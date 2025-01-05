using Hotel.Models;

namespace Hotel.Models
{
    public class Chambre
    {
        public int Id { get; set; }         
        public int NbrChambre { get; set; }
        public bool Etat { get; set; }   
        public int TypeId { get; set; }   
        public virtual ICollection<Reservation> Reservations { get; set; }

        public virtual TypeDeChambre TypeDeChambre { get; set; }
    }
}
