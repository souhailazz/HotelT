using Hotel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Hotel.Models;
using Hotel.Data;



namespace Hotel.Services
{
    public class PaiementService
    {
        private readonly HotelReservationContext _context;

        public PaiementService(HotelReservationContext context)
        {
            _context = context;
        }

        // Add a payment
        public void AddPayment(Paiement paiement)
        {
            _context.Paiements.Add(paiement);
            _context.SaveChanges();
        }
        //update paiement
        public void UpdatePayment(Paiement paiement)
        {
            var existingPayment = _context.Paiements.FirstOrDefault(p => p.Id == paiement.Id);
            if (existingPayment != null)
            {
                existingPayment.Total = paiement.Total;
                existingPayment.MethodeDePaiement = paiement.MethodeDePaiement;
                existingPayment.PaymentCode = paiement.PaymentCode;
                _context.SaveChanges();
            }
        }
        //supprimer paiement
        public void DeletePayment(int paymentId)
        {
            var payment = _context.Paiements.FirstOrDefault(p => p.Id == paymentId);
            if (payment != null)
            {
                _context.Paiements.Remove(payment);
                _context.SaveChanges();
            }
        }

        // Get payments for a specific reservation
        public List<Paiement> GetPaymentsForReservation(int reservationId)
        {
            return _context.Paiements
                           .Where(p => p.Reservations.Any(r => r.IdReservation == reservationId))
                           .ToList();
        }
    

        public List<Paiement> GetAllPaiements()
         {
           return _context.Paiements.ToList();
        }
        public List<Paiement> GetPaiementsByCode(string paymentCode)
        {
            return _context.Paiements
                           .Where(p => p.PaymentCode.Contains(paymentCode))
                           .ToList();
        }



    }
}
