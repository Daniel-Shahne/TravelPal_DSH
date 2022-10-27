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
using TravelPal_DSH.PackingItems;
using TravelPal_DSH.Travels;
using TravelPal_DSH.TravelsWindowFolder;
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

        BrushConverter bc = new();
        Brush? badInputColor;


        internal AddTravelWindow(TravelManager tm, UserManager um)
        {
            InitializeComponent();

            travelManager = tm;
            UserManager = um;

            cmbCountry.ItemsSource = Enum.GetValues(typeof(All_Countries));
            cmbTripType.ItemsSource = Enum.GetValues(typeof(Trip_Types));

            // Initialize colors
            badInputColor = (Brush)bc.ConvertFrom("#f5b8b9");

            /* Initially set all input fields tags to false as they're
             * used for validation and null tag breaks it */
            txbTravellers.Tag = false;
            txbDestination.Tag = false;
            txbItemName.Tag = false;
            txbQuantity.Tag = false;
        }



        // ------------------ METHODS --------------------

        /* Will check every field and return bool as status
         * will send out a string variable with error message
         * if any exist */
        private bool generateErrorMsgTravel(out string errorMsg)
        {
            List<string> listOfErrors = new();

            // cmbCountry must be filled
            if (cmbCountry.SelectedItem is null) listOfErrors.Add("Must select a destination country");

            // Travellers and destinationare required regardless of options
            if (txbTravellers.Tag is false) listOfErrors.Add("Travellers must be a number between 0-99");
            if (txbDestination.Tag is false) listOfErrors.Add("Must state a travel destination");

            // Nested error msg depending on trip or vacation (travel type), and then vacation cb OR trip type cmb
            if (cmbTravelType.Text.Equals("Trip"))
            {
                if (cmbTripType.SelectedItem is null) listOfErrors.Add("Must select a trip type");
            }
            else if (cmbTravelType.Text.Equals("Vacation"))
            {
                // Nothing so far because nothing can happen?
            }
            else
            {
                listOfErrors.Add("Invalid/Null travel type selection");
            }


            // Calendar null checks, and check if enddate isnt after startdate
            if (dpStartDate.SelectedDate is null) listOfErrors.Add("Must state a start date");
            if (dpEndDate.SelectedDate is null) listOfErrors.Add("Must state an end date");

            if (dpStartDate.SelectedDate is not null && dpEndDate.SelectedDate is not null)
            {
                if (dpEndDate.SelectedDate < dpStartDate.SelectedDate) listOfErrors.Add("End date must be after start date");
            }


            // Finalizing method and returning status (and possible errorMsg)
            if (listOfErrors.Count > 0)
            {
                string errorMsgA = string.Join("\n", listOfErrors);
                errorMsg = errorMsgA;
                return false;
            }
            else
            {
                errorMsg = "";
                return true;
            }
        }

        /* Works similar to generateErrorMsgTravel, but for item fields */
        private bool generateErrorMsgItem(out string errorMsg)
        {
            List<string> listOfErrors = new();

            // Name allways required
            if (txbItemName.Tag is false) listOfErrors.Add("Must state an item name");

            // Nested error msg depending on cbDocument
            if (cbDocument.IsChecked is not null && !(bool)cbDocument.IsChecked)
            {
                if (txbQuantity.Tag is false) listOfErrors.Add("Must state an item quantity");
            }
            else if (cbDocument.IsChecked is not null && (bool)cbDocument.IsChecked)
            {
                // Nothing should happen?
            }
            else
            {
                // This should not even be possible to reach
                listOfErrors.Add("cbRequired is somehow null?");
            }

            if (listOfErrors.Count > 0)
            {
                string errorMsgA = string.Join("\n", listOfErrors);
                errorMsg = errorMsgA;
                return false;
            }
            else
            {
                errorMsg = "";
                return true;
            }

        }

        /* Either creates a new passport (TravelDocument in the listview), 
         * or "updates" a current one, to reflect passport requirement*/
        private void updatePassReq()
        {
            bool userEuropean = UserManager.SignedInUser.IsEuropean;
            bool passRequired = true;

            TravelDocument? passport = null;

            /* Loops through items list to check if a passport exists 
             * If no existing passport was found, passport var remains
             * null. Index for use in overriding */
            int passIndex = -1;
            var lvItemsItems = lvPackingItems.Items;
            for (int i = 0; i <= lvItemsItems.Count - 1; i++)
            {
                TravelDocument? travelDocument = lvItemsItems[i] as TravelDocument;
                if (travelDocument is not null && travelDocument.Name.Equals("Passport"))
                {
                    passport = travelDocument;
                    passIndex = i;
                    break;
                }
            }

            /* Gets a destination as string, and EU country lists of strings */
            All_Countries destination = (All_Countries)cmbCountry.SelectedItem;
            string destinationString = destination.ToString();
            List<string> EUCountries = Enum.GetNames(typeof(EU_Countries)).ToList<string>();

            /* The only time a passport is NOT required is when user
             * is european AND the destination is in EU */
            if (EUCountries.Contains(destinationString) && userEuropean) passRequired = false;

            /* Changes required setting of existing passport to reflect
             * passrequired. If no passport exists then creates a new one */
            if (passport is null)
            {
                passport = new(passRequired, "Passport");
                lvPackingItems.Items.Add(passport);
            }
            else
            {
                /* This code works but it doesnt update the item, instead
                 * removing and creating a new one. Could not make it
                 * automatically update via INPC so ill "simulate" it
                 * via inserting at its old index*/
                lvPackingItems.Items.Remove(passport);
                passport.Required = passRequired;
                lvPackingItems.Items.Insert(passIndex, passport);
            }
        }


        // ------------------ EVENTS ---------------------

        /* Hides or shows options to select all inclusive
         * OR another cmb to select trip type, depending 
         * on which travel type was selected */
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

        /* Both events below hides/show whether quantity or 
         * required should be visible, depending on document cb */
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

        /* Closes window and returns to travelswindow, also 
         * sends travel and user managers */
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            TravelsWindow trawin = new(UserManager, travelManager);
            trawin.Show();
            this.Close();
        }

        /* Called by TextBoxes supposed to only contain integers, and
         * makes sure it only contains numericals. Uses the txb Tag prop
         * property as status bool */
        private void txbIntField_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox txb = (TextBox)sender;

            if (Int32.TryParse(txb.Text, out int result) && result > 0)
            {
                txb.Background = Brushes.MintCream;
                txb.Tag = true;
            }
            else
            {
                txb.Background = badInputColor;
                txb.Tag = false;
            }
        }

        /* Called by textboxes supposed to contain strings and makes sure it
         * actually contains atleast two characters. Uses txb Tag prop as 
         * status bool */
        private void txbStringField_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox txb = (TextBox)sender;
            
            if (txb.Text.Length > 1)
            {
                txb.Background = Brushes.MintCream;
                txb.Tag = true;
            }
            else
            {
                txb.Background = badInputColor;
                txb.Tag = false;
            }
        }

        /* Creates either a new TravelDocument or OtherItem and
         * adds to the listview, unless user tries to add a
         * passport. User can still however add passport as
         * an OtherItem. */
        private void btnAddItem_Click(object sender, RoutedEventArgs e)
        {
            if (generateErrorMsgItem(out string errorString))
            {
                if ((bool)cbDocument.IsChecked)
                {
                    // User cant add a password manually
                    if (txbItemName.Text.ToLower().Equals("passport"))
                    {
                        MessageBox.Show("Passport is added automatically", "Unallowed item name", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    TravelDocument newDoc = new((bool)cbRequired.IsChecked, txbItemName.Text);
                    lvPackingItems.Items.Add(newDoc);
                }
                else if (!(bool)cbDocument.IsChecked)
                {
                    OtherItem newItem = new(Int32.Parse(txbQuantity.Text), txbItemName.Text);
                    lvPackingItems.Items.Add(newItem);
                }
                else
                {
                    MessageBox.Show("Something wrong with cbDocument and/or cbRequired", "Unkown error");
                }

            }
            else
            {
                MessageBox.Show(errorString);
            }
        }

        /* Removes an item at selected row in listview
         * Will not remove a passport (displays error) */
        private void btnRemoveItem_Click(object sender, RoutedEventArgs e)
        {
            if (lvPackingItems.SelectedIndex >= 0)
            {
                TravelDocument? potentialPass = lvPackingItems.SelectedItem as TravelDocument;
                if (potentialPass != null && potentialPass.Name.ToLower().Equals("passport"))
                {
                    MessageBox.Show("Cannot modify passport", "Passport selected", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                lvPackingItems.Items.RemoveAt(lvPackingItems.SelectedIndex);
            }
            else MessageBox.Show("Need to select an item first to remove", "No selection", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        /* Creates the travel and returns to travelswindow */
        private void btnAddTravel_Click(object sender, RoutedEventArgs e)
        {
            if (generateErrorMsgTravel(out string errorString))
            {
                List<PackingListItem> packingList = new();
                foreach (PackingListItem item in lvPackingItems.Items) packingList.Add(item);

                All_Countries destination = (All_Countries)cmbCountry.SelectedItem;
                DateTime startDate = (DateTime)dpStartDate.SelectedDate;
                DateTime endDate = (DateTime)dpEndDate.SelectedDate;

                if (cmbTravelType.Text.Equals("Trip"))
                {
                    Trip newTrip = new(
                        txbDestination.Text, 
                        Int32.Parse(txbTravellers.Text),
                        destination,
                        startDate, 
                        endDate, 
                        (User)UserManager.SignedInUser,
                        (Trip_Types)cmbTripType.SelectedItem, 
                        packingList);

                    travelManager.addTravel(newTrip);
                    MessageBox.Show($"Added new trip to {destination}!", "Booking successfull", MessageBoxButton.OK, MessageBoxImage.Information);
                    btnCancel_Click(sender, e);
                }
                else if (cmbTravelType.Text.Equals("Vacation"))
                {
                    Vacation newVacation = new(
                        txbDestination.Text,
                        Int32.Parse(txbTravellers.Text),
                        destination,
                        startDate,
                        endDate,
                        (User)UserManager.SignedInUser,
                        (bool)cbAllInclusive.IsChecked,
                        packingList);

                    travelManager.addTravel(newVacation);
                    MessageBox.Show($"Added new trip to {destination}!", "Booking successfull", MessageBoxButton.OK, MessageBoxImage.Information);
                    btnCancel_Click(sender, e);
                }

            }
            else
            {
                MessageBox.Show(errorString);
            }
        }

        /* Calls updatePassReq to update password requirement whever
         * destination country is changed */
        private void cmbCountry_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            updatePassReq();
        }


    }
}
