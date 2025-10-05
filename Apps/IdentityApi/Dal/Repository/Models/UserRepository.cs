using System.Collections.Concurrent;
using Core.Models;
using Dal.Repository.Models.Interfaces;

namespace Dal.Repository.Models;

public class UserRepository : IUserRepository
{
    private static readonly ConcurrentDictionary<Guid, User> _store = new();

    public User? Get(Guid id)
    {
        return _store.GetValueOrDefault(id);
    }

    public User Add(User user)
    {
        if (!_store.TryAdd(user.Id, user))
        {
            throw new InvalidOperationException("Не удалось добавить пользователя");
        }
        return user;
    }

    public User Update(User user)
    {
        _store[user.Id] = user;
        return user;
    }

    public bool Delete(Guid id)
    {
        return _store.TryRemove(id, out _);
    }
}