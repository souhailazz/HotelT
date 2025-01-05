using Hotel.Services;
using System;
using System.Windows;

namespace Hotel
{
    public partial class LoginWindow : Window
    {
        private readonly ClientService _clientService;
        private readonly TypeDeChambreService _typeDeChambreService;
        private readonly AdministrateurService _administrateurService;
        private readonly ReservationService _reservationService;
        private readonly PaiementService _paiementService;

        public LoginWindow(
            AdministrateurService administrateurService,
            ClientService clientService,
            TypeDeChambreService typeDeChambreService,
            ReservationService reservationService,
            PaiementService paiementService)
        {
            _administrateurService = administrateurService ?? throw new ArgumentNullException(nameof(administrateurService));
            _clientService = clientService ?? throw new ArgumentNullException(nameof(clientService));
            _typeDeChambreService = typeDeChambreService ?? throw new ArgumentNullException(nameof(typeDeChambreService));
            _reservationService = reservationService ?? throw new ArgumentNullException(nameof(reservationService));
            _paiementService = paiementService ?? throw new ArgumentNullException(nameof(paiementService));
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBox.Text.Trim();
            string password = PasswordBox.Password.Trim();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ErrorTextBlock.Text = "Email and password cannot be empty.";
                return;
            }

            try
            {
                var administrateur = _administrateurService.GetAdministrateurByEmail(email);

                if (administrateur != null && administrateur.Password == password)
                {
                    // Pass all services to AdminWindow
                    var adminWindow = new AdminWindow(
                        _clientService,
                        _typeDeChambreService,
                        _reservationService,
                        _paiementService);
                    adminWindow.Show();
                    this.Close();
                }
                else
                {
                    ErrorTextBlock.Text = "Invalid email or password.";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

      

    }
}
