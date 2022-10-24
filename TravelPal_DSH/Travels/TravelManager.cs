using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPal_DSH.Enums;
using TravelPal_DSH.Users;

namespace TravelPal_DSH.Travels
{
    internal class TravelManager
    {
        private List<Travel> travels = new();

        public TravelManager()
        {
           
        }

        // Creates a new travel and adds to travels
        public void addTravel(string destination, int travellers, All_Countries country, DateTime startDate, DateTime endDate, User travelOwner)
        {
            Travel newTravel = new Travel(destination, travellers, country, startDate, endDate, travelOwner);
            travels.Add(newTravel);
        }

        // Deletes a travel from travels
        public void removeTravel(Travel travel)
        {
            travels.Remove(travel);
        }

        /* Returns a filtered list of travels where the travel owners
         * username matches the parameter. To be used by an User objects
         * refreshUserTravels method */
        public List<Travel> getUserTravels(string username)
        {
            List<Travel> filteredList = travels.Where(x => x.TravelOwner.Name.Equals(username)).ToList();
            return filteredList;
        }
    }
}
