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

        public IEnumerable<string> Interests { get; set; }
        public User(long dbId, string fN, string lN, string eM, DateTime birthDate, string path, string userName, string password)
        {
            FirstName = fN;
            LastName = lN;
            Birthdate = birthDate;
            ProfileImagePath = path;
            Interests = new List<string>();
            DbId = dbId;
        }
        public User()
        {
            Interests = new List<string>();
        }
    }
}
