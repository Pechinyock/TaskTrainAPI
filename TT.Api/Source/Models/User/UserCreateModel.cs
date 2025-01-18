namespace TT.Models.User
{
    public class UserCreateModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string RepeatPassword { get; set; }
    }
}
