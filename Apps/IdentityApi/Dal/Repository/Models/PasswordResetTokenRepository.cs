using System.Collections.Concurrent;
using Core.Models;
using Dal.Repository.Models.Interfaces;

namespace Dal.Repository.Models;

public class PasswordResetTokenRepository : IPasswordResetTokenRepository
{
    private static readonly ConcurrentDictionary<Guid, PasswordResetToken> _store = new();

    public PasswordResetToken? Get(Guid id)
    {
        return _store.GetValueOrDefault(id);
    }

    public IEnumerable<PasswordResetToken> GetByUser(Guid userId)
    {
        return _store.Values.Where(x => x.UserId == userId).ToArray();
    }

    public PasswordResetToken Add(PasswordResetToken token)
    {
        if (!_store.TryAdd(token.Id, token))
        {
            throw new InvalidOperationException("Не удалось добавить токен сброса пароля");
        }
        return token;
    }

    public bool Delete(Guid id)
    {
        return _store.TryRemove(id, out _);
    }
}