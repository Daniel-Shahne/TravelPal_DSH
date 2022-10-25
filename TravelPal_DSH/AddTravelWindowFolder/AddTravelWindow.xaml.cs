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
    }
}
