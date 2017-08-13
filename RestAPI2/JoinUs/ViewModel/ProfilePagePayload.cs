using JoinUs.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinUs.ViewModel
{
    public class ProfilePagePayload
    {
        public AuthenticatedUser CurrentUser;
        public User ProfileOwner;
    }
}
