using Hotel.Data;
using Hotel.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hotel.Services
{
    public class ReservationService
    {
        private readonly HotelReservationContext _context;

        public ReservationService(HotelReservationContext context)
        {
            _context = context;
        }

        // Get all reservations
        public List<Reservation> GetAllReservations()
        {
            return _context.Reservations
                .Include(r => r.Client)
                .Include(r => r.Chambre)
                .Include(r => r.ReservationEtat)
                .Include(r => r.Paiement)
                .ToList();
        }

        // Get reservation by ID
        public Reservation GetReservationById(int id)
        {
            var reservation = _context.Reservations
                .Include(r => r.Client)
                .Include(r => r.Chambre)
                .Include(r => r.ReservationEtat)
                .Include(r => r.Paiement)
                .FirstOrDefault(r => r.IdReservation == id);

            if (reservation == null)
                throw new Exception($"Reservation with ID {id} not found.");

            return reservation;
        }

        // Add a new reservation
        public void AddReservation(Reservation reservation)
        {
            if (reservation == null)
                throw new ArgumentNullException(nameof(reservation), "Reservation cannot be null.");

            _context.Reservations.Add(reservation);
            _context.SaveChanges();
        }

        // Update a reservation
        public void UpdateReservation(Reservation reservation)
        {
            if (reservation == null)
                throw new ArgumentNullException(nameof(reservation), "Reservation cannot be null.");

            var existingReservation = _context.Reservations.Find(reservation.IdReservation);
            if (existingReservation == null)
                throw new Exception($"Reservation with ID {reservation.IdReservation} not found.");

            existingReservation.DateDepart = reservation.DateDepart;
            existingReservation.DateSortie = reservation.DateSortie;
            existingReservation.ReservationEtatId = reservation.ReservationEtatId;
            existingReservation.NbrPersonne = reservation.NbrPersonne;
            existingReservation.ChambreId = reservation.ChambreId;
            existingReservation.IdClient = reservation.IdClient;
            existingReservation.IdPaiement = reservation.IdPaiement;

            _context.SaveChanges();
        }

        // Delete a reservation
        public void DeleteReservation(int reservationId)
        {
            var reservation = _context.Reservations.Find(reservationId);
            if (reservation == null)
                throw new Exception($"Reservation with ID {reservationId} not found.");
            else
            {
                reservation.Chambre.Etat = true;
            }
            _context.Reservations.Remove(reservation);
            _context.SaveChanges();
        }
        
    }
}
