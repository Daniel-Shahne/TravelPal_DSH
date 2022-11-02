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

        /* Sets datacontext to user or admin, in addition to
         * setting adminLoggedIn. Refreshes lvTravels. If
         * admin is logged in then it also disables adding
         * travels and viewing user details. */
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

                btnAddTravel.IsEnabled = false;
                btnUserDetails.IsEnabled = false;
            }

            this.userManager = userManager;
            this.travelManager = travelManager;

            refreshLvTravels();
        }

        /* Since databinding is more off a pain in the ass than "smidigt"
         * the listview has to be manually refreshed each time rather than
         * bind to Travels property */
        private void refreshLvTravels()
        {
            lvTravels.Items.Clear();
            
            if (!adminLoggedIn)
            {
                User user = (User)userManager.SignedInUser;
                foreach (Travel travel in user.Travels)
                {
                    ListViewItem lvitem = new();
                    lvitem.Tag = travel;
                    lvitem.Content = travel.GetInfo();
                    lvTravels.Items.Add(lvitem);
                }
            }
            else if (adminLoggedIn)
            {
                Admin admin = (Admin)userManager.SignedInUser;
                foreach (Travel travel in admin.Travels)
                {
                    ListViewItem lvitem = new();
                    lvitem.Tag = travel;
                    lvitem.Content = travel.GetInfo();
                    lvTravels.Items.Add(lvitem);
                }
            }
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
                ListViewItem lvitem = (ListViewItem)lvTravels.SelectedItem;
                Travel travelToRemove = lvitem.Tag as Travel;

                if (adminLoggedIn)
                {
                    travelManager.removeTravel(travelToRemove, (Admin)userManager.SignedInUser);
                    refreshLvTravels();
                }
                else
                {
                    travelManager.removeTravel(travelToRemove);
                    refreshLvTravels();
                }
            }
            else MessageBox.Show("Need to select a travel first", "No selected travel", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        /* Simply passes managers and opens new window while
         * closing this one */
        private void btnAddTravel_Click(object sender, RoutedEventArgs e)
        {
            AddTravelWindow addwin = new(travelManager, userManager);
            addwin.Show();
            this.Close();
        }

        /* Opens a separate travel details window, for the
         * selected travel. */
        private void btnDetails_Click(object sender, RoutedEventArgs e)
        {
            if (lvTravels.SelectedItem is not null)
            {
                ListViewItem lvitem = (ListViewItem)lvTravels.SelectedItem;
                Travel selectedTravel = lvitem.Tag as Travel;
                TravelDetailsWindow tdw = new(selectedTravel);
                tdw.Show();
            }
        }

        /* Opens up a very helpful "help" window */
        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {
            HelpWindow helpWin = new();
            helpWin.Show();
        }
    }
}
