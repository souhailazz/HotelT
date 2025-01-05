using Hotel.Data;
using Hotel.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Hotel.Services
{
    public class ChambreService
    {
        private readonly HotelReservationContext _context;

        public ChambreService(HotelReservationContext context)
        {
            _context = context;
        }

        public List<Chambre> GetAllChambres()
        {
            return _context.Chambres.Include(c => c.TypeDeChambre).ToList();
        }

        public Chambre GetChambreById(int id)
        {
            return _context.Chambres.Include(c => c.TypeDeChambre)
                                     .FirstOrDefault(c => c.Id == id);
        }

        public void AddChambre(Chambre chambre)
        {
            _context.Chambres.Add(chambre);
            _context.SaveChanges();
        }

        public void UpdateChambre(Chambre chambre)
        {
            _context.Chambres.Update(chambre);
            _context.SaveChanges();
        }

        public void DeleteChambre(int id)
        {
            var chambre = _context.Chambres.Find(id);
            if (chambre != null)
            {
                _context.Chambres.Remove(chambre);
                _context.SaveChanges();
            }
        }
    }
}
