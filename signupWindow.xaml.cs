using Hotel.Models;
using Hotel.Services;
using System;
using System.Linq;
using System.Windows;

namespace Hotel
{
    public partial class signupWindow : Window
    {
        private readonly AdministrateurService _administrateurService;

        public signupWindow(AdministrateurService administrateurService)
        {
            InitializeComponent();
            _administrateurService = administrateurService;

            // Test database connection when the window is initialized
            if (!TestDatabaseConnection())
            {
                MessageBox.Show("Impossible de se connecter à la base de données. L'application sera fermée.",
                                "Erreur de connexion",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
        }

        // Signup Button Click Event Handler
        private void SignupButton_Click(object sender, RoutedEventArgs e)
        {
            // Retrieving input values from the textboxes and password box
            string nom = NomTextBox.Text;
            string prenom = PrenomTextBox.Text;
            string email = EmailTextBox.Text;
            string telephone = TelephoneTextBox.Text;
            string password = PasswordBox.Password;

            // Validate that all fields are filled
            if (string.IsNullOrEmpty(nom) || string.IsNullOrEmpty(prenom) ||
                string.IsNullOrEmpty(email) || string.IsNullOrEmpty(telephone) ||
                string.IsNullOrEmpty(password))
            {
                // Show error message if any field is empty
                MessageBox.Show("Tous les champs doivent être remplis.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Validate email format
            if (!IsValidEmail(email))
            {
                MessageBox.Show("L'email n'est pas valide.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Validate password length (example: at least 6 characters)
            if (password.Length < 6)
            {
                MessageBox.Show("Le mot de passe doit comporter au moins 6 caractères.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var administrateur = new Administrateur
            {
                Nom = nom,
                Prenom = prenom,
                Email = email,
                Telephone = telephone,
                Password = password
            };

            try
            {
                _administrateurService.AddAdministrateur(administrateur);

                // Show success message
                MessageBox.Show("Inscription réussie !", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Une erreur est survenue: {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Validate email format
        private bool IsValidEmail(string email)
        {
            try
            {
                var mailAddress = new System.Net.Mail.MailAddress(email);
                return mailAddress.Address == email;
            }
            catch
            {
                return false;
            }
        }

        // Method to test database connection
        private bool TestDatabaseConnection()
        {
            try
            {
                // Try to get the first administrator from the database
                var testConnection = _administrateurService.GetAdministrateurs().FirstOrDefault();

                // If the query doesn't throw an exception, the connection is successful
                return true;
            }
            catch (Exception ex)
            {
                // Show an error message if there is an issue with the connection
                MessageBox.Show($"Erreur de connexion à la base de données : {ex.Message}",
                                "Erreur de connexion",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
                return false;
            }
        }
    }
}
