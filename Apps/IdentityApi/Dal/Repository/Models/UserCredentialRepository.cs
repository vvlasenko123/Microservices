using System.Collections.Concurrent;
using Core.Models;
using Dal.Repository.Models.Interfaces;

namespace Dal.Repository.Models;

public class UserCredentialRepository : IUserCredentialRepository
{
    private static readonly ConcurrentDictionary<Guid, UserCredential> _store = new();

    public UserCredential? Get(Guid userId)
    {
        return _store.GetValueOrDefault(userId);
    }

    public void Upsert(UserCredential cred)
    {
        _store[cred.UserId] = cred;
    }

    public bool Delete(Guid userId)
    {
        return _store.TryRemove(userId, out _);
    }
}