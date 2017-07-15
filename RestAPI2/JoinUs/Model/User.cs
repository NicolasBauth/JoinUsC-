using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                DateTime now = DateTime.Now;
                _age = now.Year - _birthDate.Year;
                if (now.Month < _birthDate.Month || (now.Month == _birthDate.Month && now.Day < _birthDate.Day))
                {
                    _age--;
                }
                Age = _age;
            }
        }
        public string ProfileImagePath { get; set; }
        public int Age
        {
            get
            {
                return _age;
            }
            set
            {
                _age = value;
            }
        }
        
        public IEnumerable<string> Interests { get; set; }
        public User(long dbId,string fN, string lN, string eM, DateTime birthDate, string path, string userName, string password)
        {
            FirstName = fN;
            LastName = lN;
            Birthdate = birthDate;
            ProfileImagePath = path;
            DateTime now = DateTime.Now;
            int age = now.Year - birthDate.Year;
            if(now.Month < birthDate.Month || (now.Month == birthDate.Month && now.Day < birthDate.Day))
            {
                age--;
            }
            Age = age;
            Interests = new List<string>();
            DbId = dbId;
        }
        public User()
        {
            Interests = new List<string>();
        }
    }
}
