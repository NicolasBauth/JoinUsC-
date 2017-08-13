using JoinUs.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace JoinUs.ViewModel
{
    public class LocateEventPayload
    {
        public AuthenticatedUser CurrentUser { get; set; }
        public Geopoint UserCoordinates { get; set; }
    }
}
