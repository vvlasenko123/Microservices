using Core.Models;

namespace Dal.Repository.Models.Interfaces;

public interface IUserCredentialRepository
{
    UserCredential? Get(Guid userId);
    void Upsert(UserCredential cred);
    bool Delete(Guid userId);
}