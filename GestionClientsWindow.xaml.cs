using Hotel.Models;
using Hotel.Services;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Hotel
{
    public partial class GestionClientsWindow : Window
    {
        private readonly ClientService _clientService;

        public GestionClientsWindow(ClientService clientService)
        {
            InitializeComponent();
            _clientService = clientService ?? throw new ArgumentNullException(nameof(clientService));
            LoadClients();
        }
        private void ClearInputFields()
        {
            NomTextBox.Clear();
            PrenomTextBox.Clear();
            MailTextBox.Clear();
            PasswordBox.Clear();
        }

        private void LoadClients()
        {
            try
            {
                if (_clientService != null)
                {
                    var clients = _clientService.GetAllClients();
                    ClientsDataGrid.ItemsSource = clients;
                }
                else
                {
                    MessageBox.Show("Client service is not initialized.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading clients: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Add a new client
        private void AddClientButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInputFields())
                return;

            try
            {
                var client = new Client
                {
                    Nom = NomTextBox.Text.Trim(),
                    Prenom = PrenomTextBox.Text.Trim(),
                    Mail = MailTextBox.Text.Trim(),
                    Password = PasswordBox.Password.Trim()
                };

                _clientService.AddClient(client);
                MessageBox.Show("Client added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadClients();
                ClearInputFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding client: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool ValidateInputFields()
        {
            if (string.IsNullOrWhiteSpace(NomTextBox.Text) ||
                string.IsNullOrWhiteSpace(PrenomTextBox.Text) ||
                string.IsNullOrWhiteSpace(MailTextBox.Text) ||
                string.IsNullOrWhiteSpace(PasswordBox.Password))
            {
                MessageBox.Show("All fields (Nom, Prénom, Email, Password) are required.", "Validation Error",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            // Optional: Email format validation
            if (!MailTextBox.Text.Contains("@"))
            {
                MessageBox.Show("Please enter a valid email address.", "Validation Error",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }


        // Update an existing client
        private void UpdateClientButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInputFields())
                return;

            if (ClientsDataGrid.SelectedItem is not Client selectedClient)
            {
                MessageBox.Show("Please select a client to update.", "Warning",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                selectedClient.Nom = NomTextBox.Text.Trim();
                selectedClient.Prenom = PrenomTextBox.Text.Trim();
                selectedClient.Mail = MailTextBox.Text.Trim();
                selectedClient.Password = PasswordBox.Password.Trim();

                _clientService.UpdateClient(selectedClient);
                MessageBox.Show("Client updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadClients();
                ClearInputFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating client: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        // Delete a selected client
        private void DeleteClientButton_Click(object sender, RoutedEventArgs e)
        {
            if (ClientsDataGrid.SelectedItem is not Client selectedClient)
            {
                MessageBox.Show("Please select a client to delete.", "Warning",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this client?",
                                                      "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    _clientService.DeleteClient(selectedClient.Id);
                    MessageBox.Show("Client deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadClients();
                    ClearInputFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting client: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        // When selecting a client in the DataGrid, pre-fill the input fields
        private void ClientsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ClientsDataGrid.SelectedItem is Client selectedClient)
            {
                NomTextBox.Text = selectedClient.Nom;
                PrenomTextBox.Text = selectedClient.Prenom;
                MailTextBox.Text = selectedClient.Mail;
                PasswordBox.Password = selectedClient.Password;
            }
        }

        // Set placeholder text when the TextBox is not focused and is empty
        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (string.IsNullOrEmpty(textBox.Text))
            {
                if (textBox == NomTextBox)
                    textBox.Text = "Nom";  // Placeholder for Nom
                else if (textBox == PrenomTextBox)
                    textBox.Text = "Prénom";  // Placeholder for Prénom
                else if (textBox == MailTextBox)
                    textBox.Text = "Email";  // Placeholder for Email
                textBox.Foreground = Brushes.Gray;  // Change text color to gray (like placeholder text)
            }
        }

        // Event handler for TextBox focus gained
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && (textBox.Text == "Nom" || textBox.Text == "Prénom" || textBox.Text == "Email"))
            {
                textBox.Text = "";  // Clear the placeholder text
                textBox.Foreground = Brushes.Black;  // Change text color to black (user input)
            }
        }

        // Event handler for TextBox focus lost
        private void TextBox_LostFocusHandler(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && string.IsNullOrEmpty(textBox.Text))
            {
                if (textBox == NomTextBox)
                    textBox.Text = "Nom";
                else if (textBox == PrenomTextBox)
                    textBox.Text = "Prénom";
                else if (textBox == MailTextBox)
                    textBox.Text = "Email";

                textBox.Foreground = Brushes.Gray;
            }
        }

        
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filterText = SearchTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(filterText))
            {
                LoadClients();
            }
            else
            {
                // Search clients based on ID input
                var filteredClients = _clientService.SearchClientsById2(filterText);
                ClientsDataGrid.ItemsSource = filteredClients;
            }
        }



    }
}
