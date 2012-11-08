using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Jarvis.Client.Model
{
    public class Location : Entity<Guid>
    {
 
        public double? Score { get; set; }
 
        public string Name { get; set; }
 
        public string StreetAddress { get; set; }
 
        public string City { get; set; }

        public string State { get; set; }
        
        public ObservableCollection<LocationCategory> Categories { get; set; }
        
        public ObservableCollection<Action> Actions { get; set; }
    }
}
