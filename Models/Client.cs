using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;  // Required for validation

namespace Hotel.Models
{
    public class Client
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Mail { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        [MaxLength(50)]
        public string Nom { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        [MaxLength(50)]
        public string Prenom { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string Password { get; set; }

        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}