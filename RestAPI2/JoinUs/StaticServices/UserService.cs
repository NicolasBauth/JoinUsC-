using System;

namespace JoinUs.StaticServices
{
    public static class UserService
    {
        public static int CalculateAge(DateTime birthDate)
        {
            DateTime now = DateTime.Now;
            int age = now.Year - birthDate.Year;
            if (now.Month < birthDate.Month || (now.Month == birthDate.Month && now.Day < birthDate.Day))
            {
                age--;
            }
            return age;
        }
    }
}
