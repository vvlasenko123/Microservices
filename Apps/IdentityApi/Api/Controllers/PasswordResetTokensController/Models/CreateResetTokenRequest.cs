namespace Api.Controllers.PasswordResetTokensController.Models;

public class CreateResetTokenRequest
{
    public Guid UserId { get; set; }
    public string Token { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
}