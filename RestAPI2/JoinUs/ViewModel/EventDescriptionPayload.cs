using JoinUs.Model;
using JoinUs.Model.EventDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinUs.ViewModel
{
    public class EventDescriptionPayload
    {
        public AuthenticatedUser CurrentUser { get; set; }
        public Event EventToDisplay { get; set; }
        public bool IsCurrentUserParticipating { get; set; }

        public EventDescriptionPayload(AuthenticatedUser currentUser,Event eventToDisplay, bool isCurrentUserParticipating)
        {
            CurrentUser = currentUser;
            EventToDisplay = eventToDisplay;
            IsCurrentUserParticipating = isCurrentUserParticipating;
        }

        public EventDescriptionPayload()
        {

        }
    }
}
