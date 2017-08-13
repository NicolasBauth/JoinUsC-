using JoinUs.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace JoinUs.ViewModel
{
    public class SearchPagePayload
    {
        public AuthenticatedUser CurrentUser { get; set; }
        public LocationInformation ScanCenter { get; set; }
    }
}
