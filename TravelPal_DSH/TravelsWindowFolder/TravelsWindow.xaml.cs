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
using TravelPal_DSH.AddTravelWindowFolder;
using TravelPal_DSH.TravelDetailsWindowFolder;
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
        bool adminLoggedIn = false;

        internal TravelsWindow(UserManager userManager, TravelManager travelManager)
        {
            InitializeComponent();

            User user = userManager.SignedInUser as User;
            this.DataContext = user;

            if (user is null)
            {
                Admin adminuser = userManager.SignedInUser as Admin;
                adminuser.refreshAdminTravels(travelManager.getAdminTravels());
                this.DataContext = adminuser;
                adminLoggedIn = true;

                // Colors refuse to change, even if removed from xaml
                btnAddTravel.Background = Brushes.Red;
                btnUserDetails.Background = Brushes.Red;

                btnAddTravel.IsEnabled = false;
                btnUserDetails.IsEnabled = false;
            }

            this.userManager = userManager;
            this.travelManager = travelManager;
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
                
                if (adminLoggedIn)
                {
                    travelManager.removeTravel(travelToRemove, (Admin)userManager.SignedInUser);
                    lvTravels.Items.Refresh(); // INPC refuses to update lv when admin removes an item, so i force with refresh
                }
                else travelManager.removeTravel(travelToRemove);
            }
            else MessageBox.Show("Need to select a travel first", "No selected travel", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnAddTravel_Click(object sender, RoutedEventArgs e)
        {
            AddTravelWindow addwin = new(travelManager, userManager);
            addwin.Show();
            this.Close();
        }

        private void btnDetails_Click(object sender, RoutedEventArgs e)
        {
            if (lvTravels.SelectedItem is not null)
            {
                Travel selectedTravel = (Travel)lvTravels.SelectedItem;
                TravelDetailsWindow tdw = new(selectedTravel);
                tdw.Show();
            }
        }
    }
}
