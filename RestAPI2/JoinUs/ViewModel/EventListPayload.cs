using JoinUs.Model;
using JoinUs.Model.EventDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinUs.ViewModel
{
    public class EventListPayload
    {
        public AuthenticatedUser CurrentUser { get; set; }
        public List<EventShortDTO> EventsToDisplay { get; set; }

        public EventListPayload(AuthenticatedUser currentUser, List<EventShortDTO> eventsToDisplay)
        {
            CurrentUser = currentUser;
            EventsToDisplay = eventsToDisplay;
        }
    }
}
