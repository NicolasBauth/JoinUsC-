using System.Collections.Generic;

namespace DTOModels.UserDTOs
{
    public class UpdateUserInterestForm
    {
        public string Username { get; set; }
        public List<int> NewInterestsIds { get; set; }
    }
}
