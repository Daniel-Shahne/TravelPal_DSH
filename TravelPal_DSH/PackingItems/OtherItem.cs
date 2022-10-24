using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelPal_DSH.PackingItems
{
    internal class OtherItem : PackingListItem
    {
        private int quantity;
        private string name;

        public int Quantity { get => quantity; set => quantity = value; }
        public string Name { get => name; set => name = value; }

        public OtherItem(int quantity, string name)
        {
            this.quantity = quantity;
            this.name = name;
        }

        public string GetInfo()
        {
            return "otheritem getinfo not yet implemented";
        }
    }
}
