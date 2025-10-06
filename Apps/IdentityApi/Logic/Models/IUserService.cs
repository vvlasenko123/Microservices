using Core.Models;

namespace Logic.Models;

public interface IUserService
{
    User Create(string firstName, string lastName, string? email, string? phone, string password);
    User? Get(Guid id);
    User Update(Guid id, string firstName, string lastName, string? email, string? phone);
    bool Delete(Guid id);
}