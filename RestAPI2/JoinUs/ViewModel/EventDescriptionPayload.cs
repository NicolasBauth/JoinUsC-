using JoinUs.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinUs.ViewModel
{
    public class EventDescriptionPayload
    {
        public User CurrentUser { get; set; }
        public Event EventToDisplay { get; set; }

        public EventDescriptionPayload(User currentUser,Event eventToDisplay)
        {
            CurrentUser = currentUser;
            EventToDisplay = eventToDisplay;
        }
    }
}
