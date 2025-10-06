using Core.Models;
using Dal.Repository.Models.Interfaces;
using Logic.Models;

namespace Logic.Services;

public class PasswordResetService : IPasswordResetService
{
    private readonly IPasswordResetTokenRepository _repo;

    public PasswordResetService(IPasswordResetTokenRepository repo)
    {
        _repo = repo;
    }

    public PasswordResetToken Create(Guid userId, string token, DateTime expiresAt)
    {
        if (string.IsNullOrWhiteSpace(token))
        {
            throw new ArgumentException("Токен не может быть пустым.");
        }
        if (expiresAt <= DateTime.UtcNow)
        {
            throw new ArgumentException("Время истечения должно быть в будущем.");
        }

        var item = new PasswordResetToken
        {
            UserId = userId,
            Token = token,
            ExpiresAt = expiresAt
        };
        return _repo.Add(item);
    }

    public PasswordResetToken? Get(Guid id)
    {
        return _repo.Get(id);
    }

    public IEnumerable<PasswordResetToken> GetByUser(Guid userId)
    {
        return _repo.GetByUser(userId);
    }

    public bool Delete(Guid id)
    {
        return _repo.Delete(id);
    }
}