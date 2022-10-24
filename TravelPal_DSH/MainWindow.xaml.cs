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
using TravelPal_DSH.Users;

namespace TravelPal_DSH
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        UserManager userManager;
        
        /* MainWindow is launched from an event in App.Xaml(.cs)
         * as MainWindow needs to know wether to instantiate a new
         * userManager, or use the one passed in it (from other created
         * windows further down app runtime, as new users are created) */
        internal MainWindow(UserManager? userManager = null)
        {
            InitializeComponent();

            if (userManager is not null) this.userManager = userManager;
            else this.userManager = new();
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
    }
}
