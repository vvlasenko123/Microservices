using System.Collections.Concurrent;
using Core.Models;
using Dal.Repository.Models.Interfaces;

namespace Dal.Repository.Models;

public class UserTokenRepository : IUserTokenRepository
{
    private static readonly ConcurrentDictionary<Guid, UserToken> _store = new();

    public UserToken? Get(Guid id)
    {
        return _store.GetValueOrDefault(id);
    }

    public IEnumerable<UserToken> GetByUser(Guid userId)
    {
        return _store.Values.Where(x => x.UserId == userId).ToArray();
    }

    public UserToken Add(UserToken token)
    {
        if (!_store.TryAdd(token.Id, token))
        {
            throw new InvalidOperationException("Не удалось добавить токен");
        }
        return token;
    }

    public bool Delete(Guid id)
    {
        return _store.TryRemove(id, out _);
    }
}