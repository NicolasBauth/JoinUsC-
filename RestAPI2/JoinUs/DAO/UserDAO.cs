using JoinUs.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinUs.DAO
{
    public static class UserDAO
    {
        public static User GetCurrentUser()
        {
            User currentUser = new User("Nicolas", "Bauthier", "nicolas.bauthier@hotmail.be", new DateTime(1995, 02, 19), "77, Rue des carmes", "Assets/PhotoProfilNicolas.jpg","Relthar","henachallenge");
            currentUser.Interests = CategoryDAO.GetInterestsOfUserByUserName(currentUser.UserName);

            return currentUser;
        }

        public static User GetUserByUserName(string userName)
        {
            User foundUser = new User("Nicolas", "Bauthier", "nicolas.bauthier@hotmail.be", new DateTime(1995, 02, 19), "77, Rue des carmes", "Assets/PhotoProfilNicolas.jpg","Relthar","henachallenge");
            return foundUser;
        }

        public static bool AuthenticateUser(string login, string password)
        {
            if(login == "Relthar" && password == "henachallenge")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool DoesUserAlreadyExists(string userName)
        {
            return false;
        }

        public static bool RegisterUser(User userToRegister)
        {
            if(!DoesUserAlreadyExists(userToRegister.UserName))
            {
                return true;
            }
            return false;
        }
    }
}
