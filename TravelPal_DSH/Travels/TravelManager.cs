using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPal_DSH.Enums;
using TravelPal_DSH.PackingItems;
using TravelPal_DSH.Users;

namespace TravelPal_DSH.Travels
{
    internal class TravelManager
    {
        private List<Travel> travels = new();

        public TravelManager()
        {
           
        }

        /* Creates a new Trip OR Vacation depending on
         * whether allInclusive or trip_type is null*/
        public void addTravel(Travel travel)
        {
            User travelOwner = travel.TravelOwner;
            travels.Add(travel);
            travelOwner.refreshUserTravels(getUserTravels(travelOwner));
        }

        // Deletes a travel from travels
        public void removeTravel(Travel travel)
        {
            travels.Remove(travel);

            travel.TravelOwner.refreshUserTravels(getUserTravels(travel.TravelOwner));
        }

        /* Returns a filtered list of travels where the travel owners
         * matches the parameter obj. To be used by an User objects
         * refreshUserTravels method */
        public List<Travel> getUserTravels(User user)
        {
            List<Travel> filteredList = travels.Where(x => x.TravelOwner.Equals(user)).ToList();
            return filteredList;
        }
    }
}
