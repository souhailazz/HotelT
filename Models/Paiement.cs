



using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel.Models
{
    public class Paiement
    {
        [Key]
        [Column("id")]
        public int Id { get; set; } // Primary Key

        [Column("total")]
        public decimal Total { get; set; }

        [Column("methode_de_paiement")]
        public string MethodeDePaiement { get; set; }

        [Column("payment_code")]
        public string PaymentCode { get; set; } // Unique property

        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}