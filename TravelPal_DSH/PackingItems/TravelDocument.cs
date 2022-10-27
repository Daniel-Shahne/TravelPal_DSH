using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelPal_DSH.PackingItems
{
    internal class TravelDocument : PackingListItem
    {
        private bool required;
        private string name;

        public event PropertyChangedEventHandler? PropertyChanged;

        public string Name { get => name; set 
            { 
                Name = value;
                NotifyPropertyChanged("Name");
            } 
        }
        public bool Required { get => required; set 
            {
                required = value;
                NotifyPropertyChanged("Required");
            } 
        }

        public TravelDocument(bool required, string name)
        {
            this.required = required;
            this.name = name;
        }

        public string GetInfo()
        {
            return "TravelDocument getinfo not yet implemented";
        }

        public override string ToString() // TODO need to make listviews use getinfo
        {
            return $"Travel document {name}. Required: {required}";
        }

        public void NotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
