using Hotel.Data;
using Hotel.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Hotel.Services
{
    public class TypeDeChambreService
    {
        private readonly HotelReservationContext _context;

        public TypeDeChambreService(HotelReservationContext context)
        {
            _context = context;
        }

        public List<TypeDeChambre> GetAllTypes()
        {
            return _context.TypeDeChambres.Include(t => t.Chambres).ToList();
        }

        public TypeDeChambre GetTypeById(int id)
        {
            return _context.TypeDeChambres.Include(t => t.Chambres)
                                          .FirstOrDefault(t => t.Id == id);
        }

        public void UpdateRoomType(TypeDeChambre roomType)
        {
            // Replace _roomTypes with _context
            var existingRoomType = _context.TypeDeChambres.FirstOrDefault(r => r.Id == roomType.Id);
            if (existingRoomType != null)
            {
                existingRoomType.Type = roomType.Type;
                existingRoomType.Prix = roomType.Prix;

                // Save changes to the database
                _context.SaveChanges();
            }
        }

        public void DeleteRoomType(int roomId)
        {
            var roomType = _context.TypeDeChambres.FirstOrDefault(r => r.Id == roomId);
            if (roomType != null)
            {
                _context.TypeDeChambres.Remove(roomType);
                _context.SaveChanges();
            }
        }

        public void AddRoomType(TypeDeChambre roomType)
        {
            // Remove manual ID generation
            _context.TypeDeChambres.Add(roomType);
            _context.SaveChanges();
        }

        public List<TypeDeChambre> GetAllRoomTypes()
        {
            return _context.TypeDeChambres.Include(t => t.Chambres).ToList();
        }
    }
}