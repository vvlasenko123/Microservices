using Core.Models;

namespace Dal.Repository.Models.Interfaces;

public interface IUserTokenRepository
{
    UserToken? Get(Guid id);
    IEnumerable<UserToken> GetByUser(Guid userId);
    UserToken Add(UserToken token);
    bool Delete(Guid id);
}