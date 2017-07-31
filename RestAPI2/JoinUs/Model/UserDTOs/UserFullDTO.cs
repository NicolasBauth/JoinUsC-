using System;
using System.Collections.Generic;

namespace JoinUs.Model.UserDTOs
{
    public class UserFullDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthdate { get; set; }
        //lien recursif follower-followed
        public List<string> FollowerNames { get; set; }
        public List<string> FollowingNames { get; set; }
        //lien User-Event
        public List<string> CreatedEvents { get; set; }
        public List<string> JoinedEvents { get; set; }
        //lien User-Categorie
        public List<string> Interests { get; set; }
    }
}
