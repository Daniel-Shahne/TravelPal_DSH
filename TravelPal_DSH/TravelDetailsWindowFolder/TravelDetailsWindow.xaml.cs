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
        
        internal TravelDetailsWindow(Travel travel)
        {
            InitializeComponent();

            this.DataContext = travel;

            fillFields(travel);
        }


        /* Things that needs to be set on instantiation
         * 1. Country cmb
         * 2. Travel type cmb and its resulting subfield 
         * 3. Start and end dates */
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

            // Fills packing list
            lvPackingItems.ItemsSource = travel.PackingList;
            lvPackingItems.IsEnabled = false;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
