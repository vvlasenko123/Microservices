namespace Core.Models;

public class UserToken
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public required string Type { get; set; } = "refresh";
    public required string Value { get; set; }
    public DateTime ExpiresAt { get; set; }
    public DateTime? RevokeAt { get; set; }
}