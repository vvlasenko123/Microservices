using Core.Models;
using Dal.Repository.Models.Interfaces;
using Logic.Models;

namespace Logic.Services;

public class UserTokenService : IUserTokenService
{
    private readonly IUserTokenRepository _repo;

    public UserTokenService(IUserTokenRepository repo)
    {
        _repo = repo;
    }

    public UserToken Create(Guid userId, string type, string value, DateTime expiresAt)
    {
        if (expiresAt <= DateTime.UtcNow)
        {
            throw new ArgumentException("Время истечения должно быть в будущем");
        }
        var token = new UserToken
        {
            UserId = userId,
            Type = string.IsNullOrWhiteSpace(type) ? "refresh" : type,
            Value = value ?? string.Empty,
            ExpiresAt = expiresAt
        };
        return _repo.Add(token);
    }

    public UserToken? Get(Guid id)
    {
        return _repo.Get(id);
    }

    public IEnumerable<UserToken> GetByUser(Guid userId)
    {
        return _repo.GetByUser(userId);
    }

    public bool Delete(Guid id)
    {
        return _repo.Delete(id);
    }
}