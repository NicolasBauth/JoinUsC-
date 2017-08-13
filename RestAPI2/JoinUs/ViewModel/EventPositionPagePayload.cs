using JoinUs.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinUs.ViewModel
{
    public class EventPositionPagePayload
    {
        public string Title { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public AuthenticatedUser CurrentUser { get; set; }
    }
}
