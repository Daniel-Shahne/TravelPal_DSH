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
using TravelPal_DSH.TravelsWindowFolder;
using TravelPal_DSH.Users;

namespace TravelPal_DSH.UserDetailsWindowFolder
{
    /// <summary>
    /// Interaction logic for UserDetailsWindow.xaml
    /// </summary>
    public partial class UserDetailsWindow : Window
    {
        private TravelManager tm;
        private UserManager um;
        bool userNameOk;
        bool pswLengthOk;
        bool pswEqualsOk;
        BrushConverter bc = new();
        Brush? badInputColor;

        internal UserDetailsWindow(TravelManager tm, UserManager um)
        {
            InitializeComponent();

            this.tm = tm;
            this.um = um;

            cmbCountry.ItemsSource = Enum.GetValues(typeof(All_Countries)); // TODO fix display name

            // Initialize colors
            badInputColor = (Brush)bc.ConvertFrom("#f5b8b9");

            // Fills in default (current) input field information
            txbUsername.Text = um.SignedInUser.Name;
            pswPassword1.Password = um.SignedInUser.Password;
            pswPassword2.Password = um.SignedInUser.Password;
            cmbCountry.SelectedItem = um.SignedInUser.Location;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            TravelsWindow trawin = new(um, tm);
            trawin.Show();
            this.Close();
        }

        /* Checks if txbUsername is longer than 3 upon text changed.
         * Field variable userNameOk as status bool */
        private void txbNewUsername_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txbUsername.Text.Length >= 3)
            {
                userNameOk = true;
                txbUsername.Background = Brushes.MintCream;
            }
            else
            {
                userNameOk = false;
                txbUsername.Background = badInputColor;
            }
        }

        /* Called by both password boxes. Checks if their lenght
         * is longer than 5. Field variable pswLengthOk as status
         * bool. Also
         * checks if the contens are equal, with pswEqualsOk 
         * as status bool. */
        private void PasswordChangedEvent(object sender, RoutedEventArgs e)
        {
            PasswordBox pswb = (PasswordBox)sender;

            // Checks length of the sender psw and sets its color for user feedback
            if (pswb.Password.Length >= 5)
            {
                pswb.Background = Brushes.MintCream;
            }
            else
            {
                pswb.Background= badInputColor;
            }

            // Determines pswLengthOk if both psw have >=5 length
            if (pswPassword1.Password.Length >= 5 && pswPassword2.Password.Length >= 5)
            {
                pswLengthOk = true;
            }
            else pswLengthOk = false;

            // Determines pswEqualsOk if both have equal content
            if (pswPassword1.Password.Equals(pswPassword2.Password))
            {
                pswEqualsOk = true;
            }
            else
            {
                pswEqualsOk= false;
            }
        }

        /* Used in save event to generate potential error messages
         * and display them as a messagebox. Also doubles as an 
         * status check to see if a save is possible. */
        private bool generateErrors()
        {
            List<string> listOfErrors = new();
;
            if (!userNameOk) listOfErrors.Add("Username must be longer than 3 characters long");
            if (!pswLengthOk) listOfErrors.Add("Password must be longer than 5 characters long");
            if (!pswEqualsOk) listOfErrors.Add("Passwords must match");
            if (cmbCountry.SelectedItem is null) listOfErrors.Add("Must select a country");

            if (listOfErrors.Count > 0)
            {
                string multiLineErrorMsg = string.Join("\n", listOfErrors);
                MessageBox.Show(multiLineErrorMsg, "Cant save changes", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else return true;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (generateErrors())
            {
                if (um.updateUsername(um.SignedInUser, txbUsername.Text))
                {
                    um.SignedInUser.Password = pswPassword1.Password;
                    um.SignedInUser.Location = (All_Countries)cmbCountry.SelectedItem;

                    lblMessage.Content = "Successfully changed account settings";
                    lblMessage.Visibility = Visibility.Visible;
                }
                else
                {
                    lblMessage.Content = "Cannot have same username as before";
                    lblMessage.Visibility = Visibility.Visible;
                }
            }
            else return;
        }
    }
}
