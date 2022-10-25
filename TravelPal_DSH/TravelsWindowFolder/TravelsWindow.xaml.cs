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
using TravelPal_DSH.Travels;
using TravelPal_DSH.UserDetailsWindowFolder;
using TravelPal_DSH.Users;

namespace TravelPal_DSH.TravelsWindowFolder
{
    /// <summary>
    /// Interaction logic for TravelsWindow.xaml
    /// </summary>
    public partial class TravelsWindow : Window
    {
        UserManager userManager;
        TravelManager travelManager;
        internal TravelsWindow(UserManager userManager, TravelManager travelManager)
        {
            InitializeComponent();

            User user = userManager.SignedInUser as User;

            this.userManager = userManager;
            this.travelManager = travelManager;
            this.DataContext = user;

            // TODO remove this, only for debugging
            travelManager.addTravel("Paris", 2, Enums.All_Countries.France, DateTime.Now, DateTime.Now, user);
            travelManager.addTravel("Kopenhamn", 2, Enums.All_Countries.Denmark, DateTime.Now, DateTime.Now, user);
            travelManager.addTravel("Malmo", 2, Enums.All_Countries.Sweden, DateTime.Now, DateTime.Now, new User("Deez", "nuts", Enums.All_Countries.Sweden));
        }

        /* Sends the current usermanager and travelmanager instances 
         * to a new mainwindow, and transitions windows to mainwindow */
        private void btnSignOut_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWin = new(userManager, travelManager);
            mainWin.Show();
            this.Close();
        }

        /* Sends the current usermanager and travelmanager instances
         * to a new userdetailswindow, and transitions windows to
         * the userdetailswindow */
        private void btnUserDetails_Click(object sender, RoutedEventArgs e)
        {
            UserDetailsWindow udw = new(travelManager, userManager);
            udw.Show();
            this.Close();
        }

        /* Removes a selected travel (nullchecks too) from lvTravel.
         * lvTravel itself is updated due to User implementing 
         * INotifyPropertyChanged, where its Travels property 
         * notifies lvTravels of the change. */
        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (lvTravels.SelectedItem is not null)
            {
                Travel travelToRemove = (Travel)lvTravels.SelectedItem;
                travelManager.removeTravel(travelToRemove);
            }
            else MessageBox.Show("Need to select a travel first", "No selected travel", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
