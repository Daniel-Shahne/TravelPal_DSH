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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TravelPal_DSH.Enums;
using TravelPal_DSH.PackingItems;
using TravelPal_DSH.Travels;
using TravelPal_DSH.TravelsWindowFolder;
using TravelPal_DSH.Users;

namespace TravelPal_DSH
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        UserManager userManager;
        TravelManager travelManager;

        /* MainWindow is launched from an event in App.Xaml(.cs)
         * as MainWindow needs to know wether to instantiate a new
         * userManager, or use the one passed in it (from other created
         * windows further down app runtime, as new users are created) */
        internal MainWindow(UserManager? userManager = null, TravelManager? travelManager = null)
        {
            InitializeComponent();

            if (travelManager is not null) this.travelManager = travelManager;
            else this.travelManager = new();

            /* uM 3 accounts upon initialization (name, pass, country)
             * 1. admin, password, Sweden
             * 2. Gandalf, password, Sweden
             * 3. Daniel, 12345, Iran 
             * Additionally calls populateUserTravels if no uM was passed,
             * i.e on app start */
            if (userManager is not null) this.userManager = userManager;
            else
            {
                this.userManager = new();
                populateUserTravels();
            }

            
        }
            

        private void populateUserTravels()
        {
            // Pulls gandalf and daniel for some initial travel population
            User gandalf = (User)this.userManager.Users[1];
            User daniel = (User)this.userManager.Users[2];

            // Creates two List<PackingListItem>, one with pass req and one without
            List<PackingListItem> passreq = new()
            {
            new TravelDocument(true, "Passport"),
            new OtherItem(10000, "Bribery money")
            };
            List<PackingListItem> passNreq = new()
            {
            new TravelDocument(false, "Passport"),
            new OtherItem(1, "EU superiority complex")
            };

            // Adds 3 travels to gandalf
            Trip gTravel1 = new(
                "Paris",
                1,
                All_Countries.France,
                new DateTime(2017, 11, 10),
                new DateTime(2017, 12, 10),
                gandalf,
                Trip_Types.Leisure,
                passNreq
                );
            Vacation gTravel2 = new(
                "Köpenhamn",
                1,
                All_Countries.Denmark,
                new DateTime(2018, 11, 10),
                new DateTime(2018, 11, 15),
                gandalf,
                true,
                passNreq
                );
            Trip gTravel3 = new(
                "Tehran",
                1,
                All_Countries.Iran,
                new DateTime(2020, 08, 05),
                new DateTime(2020, 08, 10),
                gandalf,
                Trip_Types.Leisure,
                passreq
                );

            this.travelManager.addTravel(gTravel1);
            this.travelManager.addTravel(gTravel2);
            this.travelManager.addTravel(gTravel3);

            // Adds 2 travels to daniel
            Trip dTravel1 = new(
                "Paris",
                1,
                All_Countries.France,
                new DateTime(2017, 07, 10),
                new DateTime(2017, 12, 10),
                daniel,
                Trip_Types.Leisure,
                passreq
                );
            Vacation dTravel2 = new(
                "Köpenhamn",
                1,
                All_Countries.Denmark,
                new DateTime(2018, 11, 10),
                new DateTime(2019, 11, 15),
                daniel,
                false,
                passNreq
                );

            this.travelManager.addTravel(dTravel1);
            this.travelManager.addTravel(dTravel2);
        }

        /* Used in btnLogin click event to update an initially hidden
         * error messege label */
        // TODO should add an animation or something that makes the text go away after a certain time
        private void updateErrorMsg(string msg)
        {
            lblError.Content = msg;
            lblError.Visibility = Visibility.Visible;
        }

        /* Logs into an user if credentials are correct (confirmed by
         * UserManagers methods), or displays an error message using
         * updateErrorMsg method in this class */
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (userManager.validateUsername(txbUsername.Text))
            {
                if (userManager.signInUser(txbUsername.Text, pswPassword.Password))
                {
                    TravelsWindow traWin = new(userManager, travelManager);
                    traWin.Show();
                    this.Close();
                }
                else
                {
                    updateErrorMsg("Wrong password!");
                }
            }
            else
            {
                updateErrorMsg("This user does not exist!");  
            }
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow regWin = new(userManager, travelManager);
            regWin.Show();
            this.Close();
        }
    }
}
