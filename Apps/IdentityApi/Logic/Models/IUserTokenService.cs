using Core.Models;

namespace Logic.Models;

public interface IUserTokenService
{
    UserToken Create(Guid userId, string type, string value, DateTime expiresAt);
    UserToken? Get(Guid id);
    IEnumerable<UserToken> GetByUser(Guid userId);
    bool Delete(Guid id);
}