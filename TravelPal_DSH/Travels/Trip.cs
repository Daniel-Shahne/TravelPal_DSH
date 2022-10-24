using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPal_DSH.Enums;

namespace TravelPal_DSH.Travels
{
    internal class Trip
    {
        Trip_Types trip_Type;
        Trip_Types Trip_Type { get { return trip_Type; } set { trip_Type = value; } }

        public Trip(Trip_Types trip_Type)
        {
            this.trip_Type = trip_Type;
        }

        public string GetInfo()
        {
            return "Trip getinfo not yet implemented";
        }
    }
}
