using JoinUs.StaticServices;
using System;
using System.Collections.Generic;

namespace JoinUs.Model
{
    public class AuthenticatedUser
    {
        private int _age;
        private DateTime _birthdate;
        public string AccessToken { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public DateTime BirthDate
        {
            get
            {
                return _birthdate;
            }
            set
            {
                _birthdate = value;
                _age = UserService.CalculateAge(value);
            }
        }
        public List<Category> Interests { get; set; }
        public List<User> Followings { get; set; }
        public int FollowersCount { get; set; }

        public string ProfileImagePath { get; set; }

        public int Age
        {
            get
            {
                return _age;
            }
        }
        public int DbId { get; set; }
    }
}
