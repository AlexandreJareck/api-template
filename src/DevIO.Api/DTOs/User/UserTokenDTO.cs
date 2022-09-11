namespace DevIO.Api.DTOs.User;

public class UserTokenDTO
{
    public string Id { get; set; }
    public string Email { get; set; }
    public IEnumerable<ClaimDTO> Claims { get; set; }
}
