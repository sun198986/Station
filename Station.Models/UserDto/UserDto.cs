namespace Station.Models.UserDto
{
    public class UserDto
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string MyToken { get; set; }

        public bool LoginResult { get; set; }
    }
}