using Hotel.Models;
using Hotel.Services;
using System;
using System.Linq;
using System.Windows;

namespace Hotel
{
    public partial class GestionPaiementsWindow : Window
    {
        private readonly PaiementService _paiementService;

        public GestionPaiementsWindow(PaiementService paiementService)
        {
            InitializeComponent();
            _paiementService = paiementService ?? throw new ArgumentNullException(nameof(paiementService));
            LoadPaiements();
        }

        private void LoadPaiements()
        {
            try
            {
                var paiements = _paiementService.GetAllPaiements();
                PaiementsDataGrid.ItemsSource = paiements;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading payments: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddPaymentButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!decimal.TryParse(TotalTextBox.Text, out decimal total))
                {
                    MessageBox.Show("Invalid total amount. Please enter a valid number.", "Error");
                    return;
                }

                var payment = new Paiement
                {
                    Total = total,
                    MethodeDePaiement = PaymentMethodComboBox.Text,
                    PaymentCode = PaymentCodeTextBox.Text
                };

                _paiementService.AddPayment(payment);
                MessageBox.Show("Payment added successfully.", "Success");
                LoadPaiements();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding payment: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdatePaymentButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (PaiementsDataGrid.SelectedItem is Paiement selectedPayment)
                {
                    selectedPayment.PaymentCode = PaymentCodeTextBox.Text;
                    if (decimal.TryParse(TotalTextBox.Text, out decimal total))
                        selectedPayment.Total = total;

                    selectedPayment.MethodeDePaiement = PaymentMethodComboBox.Text;

                    _paiementService.UpdatePayment(selectedPayment);
                    MessageBox.Show("Payment updated successfully.", "Success");
                    LoadPaiements();
                }
                else
                {
                    MessageBox.Show("Please select a payment to update.", "Warning");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating payment: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeletePaymentButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (PaiementsDataGrid.SelectedItem is Paiement selectedPayment)
                {
                    MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this payment?",
                                                              "Confirmation",
                                                              MessageBoxButton.YesNo,
                                                              MessageBoxImage.Warning);
                    if (result == MessageBoxResult.Yes)
                    {
                        _paiementService.DeletePayment(selectedPayment.Id);
                        MessageBox.Show("Payment deleted successfully.", "Success");
                        LoadPaiements();
                    }
                }
                else
                {
                    MessageBox.Show("Please select a payment to delete.", "Warning");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting payment: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PaiementsDataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (PaiementsDataGrid.SelectedItem is Paiement selectedPayment)
            {
                PaymentCodeTextBox.Text = selectedPayment.PaymentCode;
                TotalTextBox.Text = selectedPayment.Total.ToString();
                PaymentMethodComboBox.Text = selectedPayment.MethodeDePaiement;
            }
        }

        // Event handler for search by payment code
        private void SearchPaymentCodeTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            try
            {
                var paymentCode = SearchPaymentCodeTextBox.Text.Trim();  // Get the text entered in the search box

                // If payment code is entered, filter by code
                if (!string.IsNullOrEmpty(paymentCode))
                {
                    var payments = _paiementService.GetPaiementsByCode(paymentCode);  // Assuming this method exists
                    PaiementsDataGrid.ItemsSource = payments;
                }
                else
                {
                    // If the search box is empty, show all payments
                    LoadPaiements();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching payments: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        
    }
}
