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
using TravelPal_DSH.Travels;
using TravelPal_DSH.Users;

namespace TravelPal_DSH
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        private UserManager userManager;
        private TravelManager travelManager;
        bool userNameOk;
        bool passwordEquals;

        BrushConverter bc = new();
        Brush? badInputColor;

        internal RegisterWindow(UserManager userManager, TravelManager travelManager)
        {
            InitializeComponent();

            this.userManager = userManager;
            this.travelManager = travelManager;

            cmbCountry.ItemsSource = Enum.GetValues(typeof(All_Countries)); 

            // Initialize colors
            badInputColor = (Brush)bc.ConvertFrom("#f5b8b9");

            // Set password tags to false
            pswPassword1.Tag = false;
            pswPassword2.Tag = false;
        }

        /* Changed the buttons content to "Go Back"
         * Will just close the register window and
         * create and go back to a mainwindow, while
         * sending the current um/tm instances */
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWin = new(userManager, travelManager);
            mainWin.Show();
            this.Close();
        }

        /* Works in two steps, each being an if/else clause.
         * First clause checks if username is already taken
         * and returns if so, next step is otherwise to
         * check len >= 3 */
        private void txbUsername_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (userManager.isUsernameTaken(txbUsername.Text))
            {
                userNameOk = false;
                txbUsername.Background = badInputColor;
                
                lblError.Content = "Username already taken!";
                lblError.Visibility = Visibility.Visible;
                return;
            }
            else
            {
                lblError.Content = "";
                lblError.Visibility = Visibility.Hidden;
            }


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

        /* Called by both password fields to check valid password
         * (len >= 5) and changes their tags as success bool status
         * for each passwordbox. A second step ensures they are equal 
         * and uses field variable passwordEquals as status bool. */
        private void pswPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox psw = (PasswordBox)sender;

            if (psw.Password.Length >= 5)
            {
                psw.Tag = true;
                psw.Background = Brushes.MintCream;
            }
            else
            {
                psw.Tag = false;
                psw.Background = badInputColor;
            }

            if (pswPassword1.Password.Equals(pswPassword2.Password)) passwordEquals = true;
            else passwordEquals = false;
        }

        /* Creates an account if all fields are properly
         * filled in, relying on this instance bools and
         * a null check on cmbCountry. Will also return
         * to mainwindow if creation successfull */
        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            if (userNameOk && cmbCountry.SelectedItem is not null && (bool)pswPassword1.Tag && (bool)pswPassword2.Tag && passwordEquals)
            {
                All_Countries country = (All_Countries)cmbCountry.SelectedItem;
                User newUser = new(txbUsername.Text, pswPassword1.Password, country);
                userManager.addUser(newUser);
                MessageBox.Show($"Created user {newUser.Name}\nWelcome to TravelPal!", "User created", MessageBoxButton.OK, MessageBoxImage.Information);

                MainWindow mainWin = new(userManager, travelManager);
                mainWin.Show();
                this.Close();
            }
            else
            {
                generateErrorMsg();
            }
        }


        /* Shows error message as messagebox. Unlike other
         * generateErrorMsg this one needs to be preceded by a check
         * if any fields are invalid as it allways shows a msgbox */
        private void generateErrorMsg()
        {
            List<string> errors = new List<string>();

            if (!userNameOk) errors.Add("Username must be atleast 3 characters and not taken");
            if (!passwordEquals) errors.Add("Passwords must match");
            if (!(bool)pswPassword1.Tag || !(bool)pswPassword2.Tag) errors.Add("Password must be atleast 5 characters long");
            if (cmbCountry.SelectedItem is null) errors.Add("Must select a country of residence");

            string errorString = string.Join("\n", errors);

            MessageBox.Show(errorString, "Cant create user", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }
    }
}
