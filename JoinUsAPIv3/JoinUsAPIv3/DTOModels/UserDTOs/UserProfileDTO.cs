using System;
using System.Collections.Generic;

namespace DTOModels.UserDTOs
{
    public class UserProfileDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public List<string> Interests { get; set; }
    }
}
