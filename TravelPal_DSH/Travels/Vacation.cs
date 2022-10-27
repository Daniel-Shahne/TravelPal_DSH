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
    internal class Vacation : Travel
    {
        private bool allInclusive;

        public bool AllInclusive { get => allInclusive; set => allInclusive = value; }

        public Vacation(string destination, int travellers, All_Countries country, DateTime startDate, DateTime endDate, User travelOwner, bool allInclusive, List<PackingListItem>? packingList = null) : base(destination, travellers, country, startDate, endDate, travelOwner, packingList)
        {
            this.allInclusive = allInclusive;
        }

        

        public override string GetInfo()
        {
            return "Vacation getinfo not yet implemented";
        }

        public override string ToString() // TODO make listviews use GetInfo
        {
            return $"Vacation (All incl. {AllInclusive}) to {Destination} for {TravelDays} days";
        }
    }
}
