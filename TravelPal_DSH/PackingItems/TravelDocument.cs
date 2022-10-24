using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelPal_DSH.PackingItems
{
    internal class TravelDocument : PackingListItem
    {
        private bool required;
        private string name;

        public string Name { get => name; set => name = value; }

        public TravelDocument(bool required, string name)
        {
            this.required = required;
            this.name = name;
        }

        public string GetInfo()
        {
            return "TravelDocument getinfo not yet implemented";
        }
    }
}
