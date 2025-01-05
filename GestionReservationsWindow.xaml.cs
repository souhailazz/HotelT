using Hotel.Models;
using Hotel.Services;
using System;
using System.Linq;
using System.Windows;

namespace Hotel
{
    public partial class GestionReservationsWindow : Window
    {
        private readonly ReservationService _reservationService;

        public GestionReservationsWindow(ReservationService reservationService)
        {
            InitializeComponent();
            _reservationService = reservationService ?? throw new ArgumentNullException(nameof(reservationService));
            LoadReservations();
            LoadReservationEtats();
            ClearInputs();
        }

        private void LoadReservations()
        {
            try
            {
                ReservationsDataGrid.ItemsSource = _reservationService.GetAllReservations();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading reservations: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadReservationEtats()
        {
            cmbReservationEtat.ItemsSource = new[]
            {
                new { Id = 1, Name = "En Attente" },
                new { Id = 2, Name = "Confirmée" },
                new { Id = 3, Name = "Annulée" }
            };
            cmbReservationEtat.DisplayMemberPath = "Name";
            cmbReservationEtat.SelectedValuePath = "Id";
        }

        private void ReservationsDataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (ReservationsDataGrid.SelectedItem is Reservation selectedReservation)
            {
                PopulateInputs(selectedReservation);
                btnUpdateReservation.IsEnabled = true;
                btnDeleteReservation.IsEnabled = true;
            }
        }

        private void PopulateInputs(Reservation reservation)
        {
            txtIdReservation.Text = reservation.IdReservation.ToString();
            txtIdClient.Text = reservation.IdClient.ToString();
            dpDateDepart.SelectedDate = reservation.DateDepart;
            dpDateSortie.SelectedDate = reservation.DateSortie;
            txtNbrPersonne.Text = reservation.NbrPersonne.ToString();
            cmbReservationEtat.SelectedValue = reservation.ReservationEtatId;
        }

        private void btnUpdateReservation_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInputs()) return;

            try
            {
                var updatedReservation = new Reservation
                {
                    IdReservation = int.Parse(txtIdReservation.Text),
                    IdClient = int.Parse(txtIdClient.Text),
                    DateDepart = dpDateDepart.SelectedDate.Value,
                    DateSortie = dpDateSortie.SelectedDate.Value,
                    NbrPersonne = int.Parse(txtNbrPersonne.Text),
                    ReservationEtatId = (int)cmbReservationEtat.SelectedValue
                };

                _reservationService.UpdateReservation(updatedReservation);
                LoadReservations();
                ClearInputs();

                MessageBox.Show("Réservation mise à jour avec succès.", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la mise à jour : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDeleteReservation_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtIdReservation.Text))
            {
                MessageBox.Show("Veuillez sélectionner une réservation à supprimer.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                if (MessageBox.Show("Êtes-vous sûr de vouloir supprimer cette réservation ?", "Confirmation",
                                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    _reservationService.DeleteReservation(int.Parse(txtIdReservation.Text));
                    LoadReservations();
                    ClearInputs();

                    MessageBox.Show("Réservation supprimée avec succès.", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la suppression : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtIdClient.Text) ||
                !dpDateDepart.SelectedDate.HasValue ||
                !dpDateSortie.SelectedDate.HasValue ||
                string.IsNullOrWhiteSpace(txtNbrPersonne.Text) ||
                cmbReservationEtat.SelectedValue == null)
            {
                MessageBox.Show("Veuillez remplir tous les champs.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (!int.TryParse(txtNbrPersonne.Text, out _) || !int.TryParse(txtIdClient.Text, out _))
            {
                MessageBox.Show("Veuillez entrer des valeurs numériques valides.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (dpDateSortie.SelectedDate <= dpDateDepart.SelectedDate)
            {
                MessageBox.Show("La date de sortie doit être ultérieure à la date de départ.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

        private void ClearInputs()
        {
            txtIdReservation.Clear();
            txtIdClient.Clear();
            dpDateDepart.SelectedDate = null;
            dpDateSortie.SelectedDate = null;
            txtNbrPersonne.Clear();
            cmbReservationEtat.SelectedIndex = -1;
            btnUpdateReservation.IsEnabled = false;
            btnDeleteReservation.IsEnabled = false;
        }
    }
}
