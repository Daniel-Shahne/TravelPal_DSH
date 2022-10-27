using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelPal_DSH.PackingItems
{
    internal class OtherItem : PackingListItem
    {
        private int quantity;
        private string name;

        public event PropertyChangedEventHandler? PropertyChanged;

        public int Quantity
        {
            get => quantity; set
            {
                quantity = value;
                NotifyPropertyChanged("Quantity");
            }
        }
        public string Name
        {
            get => name; set
            {
                Name = value;
                NotifyPropertyChanged("Name");
            }
        }
        public OtherItem(int quantity, string name)
        {
            this.quantity = quantity;
            this.name = name;
        }

        public string GetInfo()
        {
            return "otheritem getinfo not yet implemented";
        }

        public override string ToString() // TODO need to make items listview use getinfo
        {
            return $"Item {Name}, Quantity: {Quantity}";
        }

        public void NotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));

        }
    }
}
