using Core.Models;

namespace Dal.Repository.Models.Interfaces;

public interface IPasswordResetTokenRepository
{
    PasswordResetToken? Get(Guid id);
    IEnumerable<PasswordResetToken> GetByUser(Guid userId);
    PasswordResetToken Add(PasswordResetToken token);
    bool Delete(Guid id);
}