using JoinUs.Model;
using System.Collections.Generic;

namespace JoinUs.ViewModel
{
    public class EditInterestPayload
    {
        public AuthenticatedUser CurrentUser { get; set; }
        public List<Category> AllCategories { get; set; }
    }
}
