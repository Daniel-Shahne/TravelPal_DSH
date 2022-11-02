using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TravelPal_DSH.Enums;
using TravelPal_DSH.Travels;

namespace TravelPal_DSH.TravelDetailsWindowFolder
{
    /// <summary>
    /// Interaction logic for TravelDetailsWindow.xaml
    /// </summary>
    public partial class TravelDetailsWindow : Window
    {
        /* Sets the windows datacontext to the travel
         * and fills in all the inputs via fillFields 
         * method, which also disables the inputs */
        internal TravelDetailsWindow(Travel travel)
        {
            InitializeComponent();

            this.DataContext = travel;

            fillFields(travel);
        }


        /* Fills in all the fields with information
         * from the given travel */
        private void fillFields(Travel travel)
        {
            // Fills country cmb
            ComboBoxItem cmbCountryItem = new ComboBoxItem();
            cmbCountryItem.Content = travel.Country;
            cmbCountry.Tag = travel;
            cmbCountry.Items.Add(cmbCountryItem);
            cmbCountry.SelectedIndex = 0;
            cmbCountry.IsEnabled = false;

            // Fills travellers
            txbTravellers.Text = travel.Travellers.ToString();
            txbTravellers.IsEnabled = false;

            // Fills destination
            txbDestination.Text = travel.Destination;
            txbDestination.IsEnabled = false;

            // Fills travel type cmb
            ComboBoxItem travelTypeItem = new();
            travelTypeItem.Content = travel.GetType().Name;
            travelTypeItem.Tag = travel;
            cmbTravelType.Items.Add(travelTypeItem);
            cmbTravelType.SelectedIndex = 0;
            cmbTravelType.IsEnabled = false;

            // Uses obj from above to determine and fill trip or vacation
            if (travel.GetType().Name.Equals("Trip"))
            {
                Trip trip = (Trip)travel;

                lblTripOrVacation.Content = "Trip type:";
                
                cmbTripType.Visibility = Visibility.Visible;
                ComboBoxItem tripTypeItem = new ComboBoxItem();
                tripTypeItem.Content = trip.Trip_Type;
                tripTypeItem.Tag = trip;
                cmbTripType.Items.Add(tripTypeItem);
                cmbTripType.SelectedIndex = 0;
                cmbTripType.IsEnabled = false;
            }
            else if (travel.GetType().Name.Equals("Vacation"))
            {
                Vacation vacation = (Vacation)travel;

                lblTripOrVacation.Content = "";

                cbAllInclusive.Visibility = Visibility.Visible;
                cbAllInclusive.IsChecked = vacation.AllInclusive;
                cbAllInclusive.IsEnabled = false;
            }

            // Fills start/end dates
            dpStartDate.SelectedDate = travel.StartDate;
            dpStartDate.IsEnabled = false;
            dpEndDate.SelectedDate = travel.EndDate;
            dpEndDate.IsEnabled = false;

            // Fills duration
            txbDuration.Text = travel.TravelDays.ToString();
            txbDuration.IsEnabled = false;

            // Fills packing list
            lvPackingItems.ItemsSource = travel.PackingList;
            lvPackingItems.IsEnabled = false;
        }

        // Closes the window
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
