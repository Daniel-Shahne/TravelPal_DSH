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
    /* Each travel is meant to be assigned to each user individually
     * via TravelManager, hence travelOwner. If an admin then uses 
     * TravelManager it will refresh its own list of travels by pulling
     * from all user instances. */
    internal class Travel
    {
        private string destination;
        private int travellers;
        private int travelDays;
        private All_Countries country;
        private DateTime startDate;
        private DateTime endDate;
        private User travelOwner;
        private List<PackingListItem>? packingList;

        public string Destination { get => destination; set => destination = value; }
        public int Travellers { get => travellers; set => travellers = value; }
        public int TravelDays { get => travelDays; }
        internal All_Countries Country { get => country; set => country = value; }
        public DateTime StartDate { get => startDate; 
            set 
            {
                startDate = value;
                calculateTravelDays();
            } 
        }
        public DateTime EndDate { get => endDate; set 
            { 
                endDate = value;
                calculateTravelDays();
            } 
        }
        internal User TravelOwner { get => travelOwner; }
        internal List<PackingListItem>? PackingList { get => packingList; }

        // Sets fields and calls calculateTravelDays
        public Travel(string destination, int travellers, All_Countries country, DateTime startDate, DateTime endDate, User travelOwner, List<PackingListItem>? packingList = null)
        {
            this.destination = destination;
            this.travellers = travellers;
            this.country = country;
            this.startDate = startDate;
            this.endDate = endDate;
            this.travelOwner = travelOwner;

            calculateTravelDays();

            if (packingList is not null) this.packingList = packingList;
        }

        // Calculates how many days the travel is
        private int calculateTravelDays()
        {
            // TODO check if rounding causes no issues
            int hours = (int)(endDate - startDate).TotalDays;
            travelDays = hours;
            return hours;
        }

        /* To be overriden and used for displaying in listviews */
        public virtual string GetInfo()
        {
            return $"ERROR: Using Travels class getinfo.";
        }
    }
}
