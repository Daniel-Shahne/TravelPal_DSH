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
    internal class Trip : Travel
    {
        Trip_Types trip_Type;
        Trip_Types Trip_Type { get { return trip_Type; } set { trip_Type = value; } }

        public Trip(string destination, int travellers, All_Countries country, DateTime startDate, DateTime endDate, User travelOwner, Trip_Types trip_Type, List<PackingListItem>? packingList = null) : base(destination, travellers, country, startDate, endDate, travelOwner, packingList)
        {
            Trip_Type = trip_Type;
        }

        public override string GetInfo()
        {
            return "Trip getinfo not yet implemented";
        }

        public override string ToString() // TODO make listviews use GetInfo
        {
            return $"{trip_Type.ToString()} trip to {Destination} for {TravelDays} days";
        }
    }
}
