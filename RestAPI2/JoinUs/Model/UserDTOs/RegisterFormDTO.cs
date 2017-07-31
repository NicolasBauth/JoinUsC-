using System;

namespace JoinUs.Model.UserDTOs
{
    public class RegisterFormDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        public DateTime Birthdate { get; set; }
        public string Password { get; set; }

    }
}
