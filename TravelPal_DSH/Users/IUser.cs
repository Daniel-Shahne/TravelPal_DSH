using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPal_DSH.Enums;

namespace TravelPal_DSH.Users
{
    internal interface IUser
    {
        string Name { get; set; }
        string Password { get; set; }
        All_Countries Location { get; set; }
        bool IsEuropean { get; }


        // Should be run anytime location is set
        protected void determineIfEuropean();
    }
}
