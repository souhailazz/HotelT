using Hotel.Data;
using Hotel.Models;
using Hotel.Services;
using System.Windows;

namespace Hotel
{
    public partial class AdminWindow : Window
    {
        private readonly ClientService _clientService;
        private readonly TypeDeChambreService _typeDeChambreService;
        private readonly ReservationService _reservationService;
        private readonly PaiementService _paiementService;

        public AdminWindow(ClientService clientService, TypeDeChambreService typeDeChambreService,
                           ReservationService reservationService, PaiementService paiementService)
        {
            InitializeComponent(); // Ensure XAML components are initialized first.

            _clientService = clientService ?? throw new ArgumentNullException(nameof(clientService));
            _typeDeChambreService = typeDeChambreService ?? throw new ArgumentNullException(nameof(typeDeChambreService));
            _reservationService = reservationService ?? throw new ArgumentNullException(nameof(reservationService));
            _paiementService = paiementService ?? throw new ArgumentNullException(nameof(paiementService));
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            // Create and show the LoginWindow
            var adminWindow = new AdminWindow(
                        _clientService,
                        _typeDeChambreService,
                        _reservationService,
                        _paiementService);
            adminWindow.Show();

            this.Close();
        }


        private void GestionClients_Click(object sender, RoutedEventArgs e)
        {
            var gestionClientsWindow = new GestionClientsWindow(_clientService);
            gestionClientsWindow.Show();
        }

        private void GestionEmployes_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Gestion des Employés");
        }

        private void GestionTypesChambres_Click(object sender, RoutedEventArgs e)
        {
            var gestionTypesChambresWindow = new GestionTypesChambresWindow(_typeDeChambreService);
            gestionTypesChambresWindow.Show();
        }

        private void GestionReservations_Click(object sender, RoutedEventArgs e)
        {
            var gestionReservationsWindow = new GestionReservationsWindow(_reservationService);
            gestionReservationsWindow.Show();
        }

        private void GestionPaiements_Click(object sender, RoutedEventArgs e)
        {
            var gestionPaiementsWindow = new GestionPaiementsWindow(_paiementService);
            gestionPaiementsWindow.Show();

        }

        private void ExportImport_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Export/Import des Données");
        }
    }
}
