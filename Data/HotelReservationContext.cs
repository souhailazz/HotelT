using Microsoft.EntityFrameworkCore;
using Hotel.Models; 

namespace Hotel.Data
{
    public class HotelReservationContext : DbContext
    {
        public HotelReservationContext(DbContextOptions<HotelReservationContext> options)
            : base(options)
        {
        }

     
        public DbSet<Client> Clients { get; set; }
        public DbSet<Employe> Employes { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<TypeDeChambre> TypeDeChambres { get; set; }
        public DbSet<Chambre> Chambres { get; set; }
        public DbSet<ReservationEtat> ReservationEtats { get; set; }
        public DbSet<Paiement> Paiements { get; set; }
        public DbSet<Administrateur> Administrateurs { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Reservation Table Configuration
            modelBuilder.Entity<Reservation>()
                .ToTable("Reservation")
                .HasKey(r => r.IdReservation); // Ensure the key is defined properly
            modelBuilder.Entity<Reservation>()
                .Property(r => r.IdReservation).HasColumnName("id_reservation");
            modelBuilder.Entity<Reservation>()
                .Property(r => r.IdClient).HasColumnName("id_client");
            modelBuilder.Entity<Reservation>()
                .Property(r => r.DateSortie).HasColumnName("date_sortie");
            modelBuilder.Entity<Reservation>()
                .Property(r => r.DateDepart).HasColumnName("date_depart");
            modelBuilder.Entity<Reservation>()
                .Property(r => r.ReservationEtatId).HasColumnName("reservation_etat_id");
            modelBuilder.Entity<Reservation>()
                .Property(r => r.NbrPersonne).HasColumnName("nbr_personne");
            modelBuilder.Entity<Reservation>()
                .Property(r => r.ChambreId).HasColumnName("chambre_id");
            modelBuilder.Entity<Reservation>()
                .Property(r => r.IdPaiement).HasColumnName("id_paiement");

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Client)
                .WithMany(c => c.Reservations)
                .HasForeignKey(r => r.IdClient)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.ReservationEtat)
                .WithMany(e => e.Reservations)
                .HasForeignKey(r => r.ReservationEtatId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Chambre)
                .WithMany(c => c.Reservations)
                .HasForeignKey(r => r.ChambreId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Paiement)
                .WithMany(p => p.Reservations)
                .HasForeignKey(r => r.IdPaiement)
                .OnDelete(DeleteBehavior.Restrict);

            // Chambre Table Configuration
            modelBuilder.Entity<Chambre>()
                .ToTable("Chambre")
                .HasKey(c => c.Id); 
            modelBuilder.Entity<Chambre>()
                .Property(c => c.Id).HasColumnName("id");
            modelBuilder.Entity<Chambre>()
                .Property(c => c.NbrChambre).HasColumnName("nbr_chambre");
            modelBuilder.Entity<Chambre>()
                .Property(c => c.Etat).HasColumnName("etat");
            modelBuilder.Entity<Chambre>()
                .Property(c => c.TypeId).HasColumnName("type_id");

            modelBuilder.Entity<Chambre>()
                .HasOne(c => c.TypeDeChambre)
                .WithMany(t => t.Chambres)
                .HasForeignKey(c => c.TypeId)
                .OnDelete(DeleteBehavior.Restrict);

            // ReservationEtat Table Configuration
            modelBuilder.Entity<ReservationEtat>()
                .ToTable("ReservationEtat")
                .HasKey(e => e.Id); // Ensure the key is defined properly
            modelBuilder.Entity<ReservationEtat>()
                .Property(e => e.Id).HasColumnName("id");
            modelBuilder.Entity<ReservationEtat>()
                .Property(e => e.Etat).HasColumnName("etat");

            // Client Table Configuration
            modelBuilder.Entity<Client>()
                .ToTable("Client")
                .HasKey(c => c.Id); // Ensure the key is defined properly
            modelBuilder.Entity<Client>()
                .Property(c => c.Id).HasColumnName("id");
            modelBuilder.Entity<Client>()
                .Property(c => c.Mail).HasColumnName("mail");
            modelBuilder.Entity<Client>()
                .Property(c => c.Nom).HasColumnName("nom");
            modelBuilder.Entity<Client>()
                .Property(c => c.Prenom).HasColumnName("prenom");
            modelBuilder.Entity<Client>()
                .Property(c => c.Password).HasColumnName("password");

            // TypeDeChambre Table Configuration
            modelBuilder.Entity<TypeDeChambre>()
                .ToTable("TypeDeChambre")
                .HasKey(t => t.Id); // Ensure the key is defined properly
            modelBuilder.Entity<TypeDeChambre>()
                .Property(t => t.Id).HasColumnName("id");
            modelBuilder.Entity<TypeDeChambre>()
                .Property(t => t.Type).HasColumnName("type");
            modelBuilder.Entity<TypeDeChambre>()
                .Property(t => t.Prix).HasColumnName("prix");

            modelBuilder.Entity<TypeDeChambre>()
                .HasMany(t => t.Chambres)
                .WithOne(c => c.TypeDeChambre)
                .HasForeignKey(c => c.TypeId)
                .OnDelete(DeleteBehavior.Restrict);

            // Paiement Table Configuration
            modelBuilder.Entity<Paiement>()
                .ToTable("Paiement")
                .HasKey(p => p.Id); // Ensure the key is defined properly
            modelBuilder.Entity<Paiement>()
                .Property(p => p.Id).HasColumnName("id");
            modelBuilder.Entity<Paiement>()
                .Property(p => p.Total).HasColumnName("total");
            modelBuilder.Entity<Paiement>()
                .Property(p => p.MethodeDePaiement).HasColumnName("methode_de_paiement");
            modelBuilder.Entity<Paiement>()
                .Property(p => p.PaymentCode).HasColumnName("payment_code");

            // Administrateur Table Configuration
            modelBuilder.Entity<Administrateur>()
                .ToTable("Administrateur")
                .HasKey(a => a.Id); // Primary Key
            modelBuilder.Entity<Administrateur>()
                .Property(a => a.Id).HasColumnName("id");
            modelBuilder.Entity<Administrateur>()
                .Property(a => a.Nom).HasColumnName("nom");
            modelBuilder.Entity<Administrateur>()
                .Property(a => a.Prenom).HasColumnName("prenom");
            modelBuilder.Entity<Administrateur>()
                .Property(a => a.Email).HasColumnName("email");
            modelBuilder.Entity<Administrateur>()
                .Property(a => a.Telephone).HasColumnName("telephone");
            modelBuilder.Entity<Administrateur>()
                .Property(a => a.Password).HasColumnName("password");
        }

    }
}
