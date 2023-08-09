namespace CoreApi.Models
{
    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        
    }

    public class loginresponse
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }
}
