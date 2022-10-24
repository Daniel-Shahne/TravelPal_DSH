using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPal_DSH.Enums;

namespace TravelPal_DSH.Users
{
    internal class User : IUser
    {
        private string name;
        private string password;
        private All_Countries location;
        private bool isEuropean;
        // TODO implement List<Travel>

        public string Name { get { return name; } set { name = value; } }
        public string Password { get { return password; } set { password = value; } }
        public All_Countries Location { 
            get { return location; } 
            set { 
                location = value;
                determineIfEuropean();
            } 
        }
        public bool IsEuropean { get { return isEuropean; } }

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
        public void determineIfEuropean()
        {
            if (Enum.TryParse<EU_Countries>(location.ToString(), out EU_Countries eu_country))
            {
                isEuropean = true;
            }
            else isEuropean = false;
        }
    }
}
