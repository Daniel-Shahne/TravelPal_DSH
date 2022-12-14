using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPal_DSH.Enums;
using TravelPal_DSH.Travels;

namespace TravelPal_DSH.Users
{
    internal class User : IUser , INotifyPropertyChanged
    {
        private string name;
        private string password;
        private All_Countries location;
        private bool isEuropean;
        private List<Travel> travels = new(); // Pulled from TravelManagers getUserTravels via refreshUserTravels

        public event PropertyChangedEventHandler? PropertyChanged;

        public string Name { get { return name; } 
            set 
            { 
                name = value;
                NotifyPropertyChanged("Name");
            } 
        }
        public string Password { get { return password; } set { password = value; } }
        public All_Countries Location { 
            get { return location; } 
            set { 
                location = value;
                determineIfEuropean();
                NotifyPropertyChanged("Location");
                NotifyPropertyChanged("IsEuropean");
            } 
        }
        public bool IsEuropean { get { return isEuropean; } }
        public List<Travel> Travels { get => travels; }

        public User(string name, string password, All_Countries location)
        {
            this.name = name;
            this.password = password;
            this.location = location;

            determineIfEuropean();
        }

        /*  Shouls be run anytime location is changed, to update
         *  isEuropean. */
        // WTF WHY CANT IT BE PROTECTED OR PRIVATE, I DONT WANT IT PUBLIC
        private void determineIfEuropean()
        {
            if (Enum.TryParse<EU_Countries>(location.ToString(), out EU_Countries eu_country))
            {
                isEuropean = true;
            }
            else isEuropean = false;
        }

        /* Parameter userTravels is meant to come from TravelManager's 
         * getUserTravel(string usernane) method which filters its
         * list of all travels, by username */
        public void refreshUserTravels(List<Travel> userTravels)
        {
            this.travels = userTravels;
            NotifyPropertyChanged("Travels");
        }

        /* Used to notify about properties changed */
        public void NotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
