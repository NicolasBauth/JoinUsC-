using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace JoinUs.Model
{
    public class LocationInformation
    {
        public Geopoint Location { get; set; }
        public string Address { get; set; }
    }
}
