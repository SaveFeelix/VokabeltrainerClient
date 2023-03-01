namespace Client.Utils.Dto.User;

public class UserLoginDto
{
    public UserLoginDto(string userName, string password)
    {
        UserName = userName;
        Password = password;
    }

    public string UserName { get; set; }
    public string Password { get; set; }
}