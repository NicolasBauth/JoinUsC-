using JoinUs.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinUs.ViewModel
{
    public class ProfileFromOutsidePayload
    {
        public User CurrentUser { get; set; }
        public User VisitedUser { get; set; }

        public ProfileFromOutsidePayload(User currentUser, User visitedUser)
        {
            CurrentUser = currentUser;
            VisitedUser = visitedUser;
        }
    }
}
