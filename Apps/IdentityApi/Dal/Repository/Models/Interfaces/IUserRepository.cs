using Core.Models;

namespace Dal.Repository.Models.Interfaces;

public interface IUserRepository
{
    User? Get(Guid id);
    User Add(User user);
    User Update(User user);
    bool Delete(Guid id);
}