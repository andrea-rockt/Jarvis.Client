using System;
using System.Runtime.Serialization;

namespace Jarvis.Client.Model
{
    public class LocationCategory : Entity<Guid>
    {
        [DataMember]
        public string Name { get; set; }
    }
}
