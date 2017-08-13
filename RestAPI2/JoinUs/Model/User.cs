using JoinUs.StaticServices;
using System;
using System.Collections.Generic;

namespace JoinUs.Model
{
    public class User
    {
        private int _age;
        private DateTime _birthDate;
        public long DbId;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public DateTime Birthdate
        {
            get
            {
                return _birthDate;
            }
            set
            {
                _birthDate = value;
                _age = UserService.CalculateAge(value);
            }
        }
        public string ProfileImagePath { get; set; }
        public int Age
        {
            get
            {
                return _age;
            }
        }

        public List<Category> Interests { get; set; }
        
        public User()
        {
            Interests = new List<Category>();
        }
    }
}
