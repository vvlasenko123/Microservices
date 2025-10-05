namespace Api.Controllers.UserTokensController.Models;

public class CreateTokenRequest
{
    public Guid UserId { get; set; }
    public string Type { get; set; } = "refresh";
    public string Value { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
}