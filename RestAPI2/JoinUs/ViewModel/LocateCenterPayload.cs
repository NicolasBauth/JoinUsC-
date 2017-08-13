using JoinUs.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace JoinUs.ViewModel
{
    public class LocateCenterPayload
    {
        public AuthenticatedUser CurrentUser { get; set; }
        public Geopoint MapCenter { get; set; }
    }
}
