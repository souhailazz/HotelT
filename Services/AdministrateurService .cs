using Hotel.Models;
using Hotel.Data;
using System;
using System.Linq;

namespace Hotel.Services
{
    public class AdministrateurService
    {
        private readonly HotelReservationContext _context;

        public AdministrateurService(HotelReservationContext context)
        {
            _context = context;
        }

        public void AddAdministrateur(Administrateur administrateur)
        {
            try
            {
                _context.Administrateurs.Add(administrateur); 
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error while saving the administrateur to the database", ex);
            }
        }

        public IQueryable<Administrateur> GetAdministrateurs()
        {
            return _context.Administrateurs;  
        }

        public Administrateur GetAdministrateurByEmail(string email)
        {
            return _context.Administrateurs.FirstOrDefault(a => a.Email == email);  
        }
    }
}
