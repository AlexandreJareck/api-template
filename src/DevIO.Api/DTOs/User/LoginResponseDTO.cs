namespace DevIO.Api.DTOs.User;

public class LoginResponseDTO
{
    public string AccessToken { get; set; }
    public double ExpiresIn { get; set; }
    public UserTokenDTO UserToken { get; set; }
}
