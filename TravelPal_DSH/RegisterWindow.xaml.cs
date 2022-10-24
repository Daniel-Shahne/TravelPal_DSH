using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
using TravelPal_DSH.Users;

namespace TravelPal_DSH
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        private UserManager userManager;
        bool userNameOk;
        bool passwordOk;

        BrushConverter bc = new();
        Brush? badInputColor;

        internal RegisterWindow(UserManager userManager)
        {
            InitializeComponent();

            this.userManager = userManager;

            cmbCountry.ItemsSource = Enum.GetValues(typeof(All_Countries)); // TODO fix display name

            // Initialize colors
            badInputColor = (Brush)bc.ConvertFrom("#f5b8b9");

        }

        /* Changed the buttons content to "Go Back"
         * Will just close the register window and
         * create and go back to a mainwindow, while
         * sending the current usermanager instance */
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWin = new(userManager);
            mainWin.Show();
            this.Close();
        }

        /* Updates userNameOk, and UI input field color,
         * to reflect if said input would be ok as username */
        private void txbUsername_TextChanged(object sender, TextChangedEventArgs e)
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

        /* Updates passwordOk, and UI input field color,
         * to reflect if said input would be ok as username */
        private void pswPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (pswPassword.Password.Length >= 5)
            {
                passwordOk = true;
                pswPassword.Background = Brushes.MintCream;
            }
            else
            {
                passwordOk = false;
                pswPassword.Background = badInputColor;
            }
        }

        /* Creates an account if all fields are properly
         * filled in, relying on this instance bools and
         * a null check on cmbCountry */
        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            if (userNameOk && passwordOk && cmbCountry.SelectedItem is not null)
            {
                All_Countries country = (All_Countries)cmbCountry.SelectedItem;
                User newUser = new(txbUsername.Text, pswPassword.Password, country);
                userManager.addUser(newUser);
                MessageBox.Show($"Created user {newUser.Name}\nWelcome to TravelPal!", "User created", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                lblError.Content = "One or more fields arent properly filled in";
                lblError.Visibility = Visibility.Visible;
            }
        }
    }
}
