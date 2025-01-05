using Hotel.Models;
using Hotel.Services;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Hotel
{
    public partial class GestionTypesChambresWindow : Window
    {
        private readonly TypeDeChambreService _typeDeChambreService;

        public GestionTypesChambresWindow(TypeDeChambreService typeDeChambreService)
        {
            _typeDeChambreService = typeDeChambreService ?? throw new ArgumentNullException(nameof(typeDeChambreService));
            InitializeComponent();
            LoadRoomTypes();
        }

        // Load room types into the DataGrid
        private void LoadRoomTypes()
        {
            try
            {
                List<TypeDeChambre> roomTypes = _typeDeChambreService.GetAllRoomTypes();
                RoomDataGrid.ItemsSource = roomTypes;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading room types: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Add a new room type
        private void AddRoomType_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string type = TypeTextBox.Text;
                if (!decimal.TryParse(PrixTextBox.Text, out decimal prix))
                {
                    MessageBox.Show("Please enter a valid price.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var newRoomType = new TypeDeChambre { Type = type, Prix = prix };
                _typeDeChambreService.AddRoomType(newRoomType);

                MessageBox.Show("Room type added successfully!", "Success");
                ClearInputs();
                LoadRoomTypes();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding room type: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Update selected room type
        private void UpdateRoomType_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (RoomDataGrid.SelectedItem is TypeDeChambre selectedRoomType)
                {
                    string newType = TypeTextBox.Text;
                    if (!decimal.TryParse(PrixTextBox.Text, out decimal newPrix))
                    {
                        MessageBox.Show("Please enter a valid price.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    selectedRoomType.Type = newType;
                    selectedRoomType.Prix = newPrix;

                    _typeDeChambreService.UpdateRoomType(selectedRoomType);
                    MessageBox.Show("Room type updated successfully!", "Success");
                    ClearInputs();
                    LoadRoomTypes();
                }
                else
                {
                    MessageBox.Show("Please select a room type to update.", "Selection Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating room type: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Delete selected room type
        private void DeleteRoomType_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (RoomDataGrid.SelectedItem is TypeDeChambre selectedRoomType)
                {
                    MessageBoxResult result = MessageBox.Show($"Are you sure you want to delete {selectedRoomType.Type}?",
                                                              "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                    if (result == MessageBoxResult.Yes)
                    {
                        _typeDeChambreService.DeleteRoomType(selectedRoomType.Id);
                        MessageBox.Show("Room type deleted successfully!", "Success");
                        LoadRoomTypes();
                    }
                }
                else
                {
                    MessageBox.Show("Please select a room type to delete.", "Selection Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting room type: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RoomDataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (RoomDataGrid.SelectedItem is TypeDeChambre selectedRoomType)
            {
                TypeTextBox.Text = selectedRoomType.Type;
                PrixTextBox.Text = selectedRoomType.Prix.ToString("0.00");
            }
        }
        private void ClearInputs()
        {
            TypeTextBox.Clear();
            PrixTextBox.Clear();
        }
    }
}
