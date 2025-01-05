using Hotel.Data;
using Hotel.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Windows;

namespace Hotel
{
    public partial class ReservationWindow : Window
    {
        private readonly HotelReservationContext _context;

        public ReservationWindow(HotelReservationContext context)
        {
            _context = context;
            InitializeComponent();
            LoadReservationData();
        }

        private void LoadReservationData()
        {
            var reservations = _context.Reservations.Include(r => r.Client).Include(r => r.Chambre).ToList();
            ReservationDataGrid.ItemsSource = reservations;
        }
    }
}
