using Hotel.Data;
using Hotel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Services
{
    public class ClientService
    {
        private readonly HotelReservationContext _context;

        public ClientService(HotelReservationContext context)
        {
            _context = context;
        }

        public List<Client> GetAllClients()
        {
            return _context.Clients.ToList(); 
        }

        public void AddClient(Client client)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));

            _context.Clients.Add(client);
            _context.SaveChanges();
        }


        public Client GetClientById(int id)
        {
            return _context.Clients.Include(c => c.Reservations).FirstOrDefault(c => c.Id == id);  
        }

        public Client GetClientByEmail(string email)
        {
            return _context.Clients.Include(c => c.Reservations).FirstOrDefault(c => c.Mail == email); 
        }

        public void UpdateClient(Client client)
        {
            _context.Clients.Update(client);
            _context.SaveChanges();
        }
        public List<Client> SearchClientsById(string idText)
        {
            if (int.TryParse(idText, out int id))  // Check if the input is a valid integer
            {
                var client = GetClientById(id);  // Use the method to fetch by ID
                return client != null ? new List<Client> { client } : new List<Client>();
            }
            else
            {
                return new List<Client>();  // If the ID is invalid, return an empty list
            }
        }
        public void DeleteClient(int id)
        {
            var client = _context.Clients
                .Include(c => c.Reservations) // Include related Reservations
                .FirstOrDefault(c => c.Id == id);

            if (client == null)
                throw new InvalidOperationException("Client not found.");

            // Remove related reservations first
            _context.Reservations.RemoveRange(client.Reservations);

            // Remove the client
            _context.Clients.Remove(client);

            _context.SaveChanges();
        }
        public List<Client> SearchClientsById2(string idText)
        {
            if (int.TryParse(idText, out int id))  // Check if the input is a valid integer
            {
                var client = GetClientById(id);  // Use the method to fetch by ID
                return client != null ? new List<Client> { client } : new List<Client>();
            }
            else
            {
                return new List<Client>();  // If the ID is invalid, return an empty list
            }
        }

    }
}
