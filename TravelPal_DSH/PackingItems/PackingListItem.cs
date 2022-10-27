﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelPal_DSH.PackingItems
{
    internal interface PackingListItem : INotifyPropertyChanged
    {
        string Name { get; }

        string GetInfo();
    }
}
