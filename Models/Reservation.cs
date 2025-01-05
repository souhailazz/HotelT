using Hotel.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel.Models
{
    public class Reservation
    {
        [Key]
        [Column("id_reservation")]
        public int IdReservation { get; set; } // Primary Key

        [ForeignKey("Client")]
        [Column("id_client")]
        public int IdClient { get; set; } // Foreign Key to Client

        [Column("date_depart")]
        public DateTime DateDepart { get; set; }

        [Column("date_sortie")]
        public DateTime DateSortie { get; set; }

        [ForeignKey("ReservationEtat")]
        [Column("reservation_etat_id")]
        public int ReservationEtatId { get; set; } // Foreign Key to ReservationEtat

        [Column("nbr_personne")]
        public int NbrPersonne { get; set; }

        [ForeignKey("Chambre")]
        [Column("chambre_id")]
        public int ChambreId { get; set; } // Foreign Key to Chambre

        [ForeignKey("Paiement")]
        [Column("id_paiement")]
        public int IdPaiement { get; set; } // Foreign Key to Paiement

        // Navigation Properties
        public virtual Client Client { get; set; }
        public virtual ReservationEtat ReservationEtat { get; set; }
        public virtual Chambre Chambre { get; set; }
        public virtual Paiement Paiement { get; set; }
    }
}