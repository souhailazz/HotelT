using Hotel.Data;
using Hotel.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hotel.Services
{
    public class EmployeService
    {
        private readonly HotelReservationContext _context;

        public EmployeService(HotelReservationContext context)
        {
            _context = context;
        }

        public List<Employe> GetAllEmployees()
        {
            return _context.Employes.ToList();
        }

        // Add a new employee
        public void AddEmployee(Employe employe)
        {
            _context.Employes.Add(employe);
            _context.SaveChanges();
        }

        // Update an employee
        public void UpdateEmployee(Employe employe)
        {
            var existingEmploye = _context.Employes.Find(employe.Id);
            if (existingEmploye == null)
                throw new Exception("Employee not found.");

            existingEmploye.Nom = employe.Nom;
            existingEmploye.Prenom = employe.Prenom;
            existingEmploye.Position = employe.Position;

            _context.SaveChanges();
        }

        // Delete an employee
        public void DeleteEmployee(int employeeId)
        {
            var employe = _context.Employes.Find(employeeId);
            if (employe == null)
                throw new Exception("Employee not found.");

            _context.Employes.Remove(employe);
            _context.SaveChanges();
        }
    }
}
