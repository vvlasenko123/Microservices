using Core.Models;

namespace Logic.Models;

public interface IPasswordResetService
{
    PasswordResetToken Create(Guid userId, string token, DateTime expiresAt);
    PasswordResetToken? Get(Guid id);
    IEnumerable<PasswordResetToken> GetByUser(Guid userId);
    bool Delete(Guid id);
}