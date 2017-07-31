using System;
using System.Collections.Generic;

namespace JoinUs.Model.UserDTOs
{
    public class UserProfileDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public List<string> Interests { get; set; }
    }
}
