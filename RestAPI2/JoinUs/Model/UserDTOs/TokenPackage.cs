namespace JoinUs.Model.UserDTOs
{
    public class TokenPackage
    {
        public string Access_Token { get; set; }
        public int Expires_in { get; set; }
        public string token_type { get; set; }
        public string UserName { get; set; }

    }
}
