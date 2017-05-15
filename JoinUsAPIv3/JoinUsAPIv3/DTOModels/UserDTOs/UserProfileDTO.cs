using System.Collections.Generic;

namespace DTOModels.UserDTOs
{
    public class UserProfileDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BirthDate { get; set; }
        public List<string> Interests { get; set; }
    }
}
