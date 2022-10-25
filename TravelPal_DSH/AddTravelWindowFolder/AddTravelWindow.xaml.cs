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
using TravelPal_DSH.Users;

namespace TravelPal_DSH.AddTravelWindowFolder
{
    /// <summary>
    /// Interaction logic for AddTravelWindow.xaml
    /// </summary>
    public partial class AddTravelWindow : Window
    {
        TravelManager travelManager;
        UserManager UserManager;

        internal AddTravelWindow(TravelManager tm, UserManager um)
        {
            InitializeComponent();

            travelManager = tm;
            UserManager = um;

            cmbCountry.ItemsSource = Enum.GetValues(typeof(All_Countries)); //TODO fix country names
            cmbTripType.ItemsSource = Enum.GetValues(typeof(Trip_Types));
        }

        private void cmbTravelType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem cbi = (ComboBoxItem)cmbTravelType.SelectedItem;

            if (cbi.Content.ToString().Equals("Trip"))
            {
                lblTripOrVacation.Content = "Trip type";
                cbAllInclusive.Visibility = Visibility.Hidden;
                cmbTripType.Visibility = Visibility.Visible;
            }
            else if (cbi.Content.ToString().Equals("Vacation"))
            {
                lblTripOrVacation.Content = "All inclusive";
                cbAllInclusive.Visibility = Visibility.Visible;
                cmbTripType.Visibility = Visibility.Hidden;
            }
        }

        private void cbDocument_Checked(object sender, RoutedEventArgs e)
        {
            cbRequired.Visibility = Visibility.Visible;
            spQuantity.Visibility = Visibility.Hidden;
        }

        private void cbDocument_Unchecked(object sender, RoutedEventArgs e)
        {
            cbRequired.Visibility = Visibility.Hidden;
            spQuantity.Visibility = Visibility.Visible;
        }
    }
}
