using Core.Models;
using Core.Models.Interfaces;
using Dal.Repository.Models.Interfaces;
using Logic.Models;

namespace Logic.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _users;
    private readonly IUserCredentialRepository _creds;
    private readonly IPasswordHasher _hasher;

    public UserService(IUserRepository users, IUserCredentialRepository creds, IPasswordHasher hasher)
    {
        _users = users;
        _creds = creds;
        _hasher = hasher;
    }

    public User Create(string firstName, string lastName, string? email, string? phone, string password)
    {
        if (string.IsNullOrWhiteSpace(firstName))
        {
            throw new ArgumentException("Имя не может быть пустым");
        }
        if (string.IsNullOrWhiteSpace(lastName))
        {
            throw new ArgumentException("Фамилия не может быть пустой");
        }
        if (string.IsNullOrWhiteSpace(password))
        {
            throw new ArgumentException("Пароль не может быть пустым");
        }

        var user = new User
        {
            FirstName = firstName.Trim(),
            LastName = lastName.Trim(),
            Email = string.IsNullOrWhiteSpace(email) ? null : email.Trim(),
            Phone = string.IsNullOrWhiteSpace(phone) ? null : phone.Trim()
        };

        _users.Add(user);

        var (hash, salt) = _hasher.Hash(password);
        _creds.Upsert(new UserCredential
        {
            UserId = user.Id,
            PasswordHash = hash,
            PasswordSalt = salt
        });

        return user;
    }

    public User? Get(Guid id)
    {
        return _users.Get(id);
    }

    public User Update(Guid id, string firstName, string lastName, string? email, string? phone)
    {
        var user = _users.Get(id);
        if (user == null)
        {
            throw new InvalidOperationException("Пользователь не найден");
        }

        user.FirstName = string.IsNullOrWhiteSpace(firstName) ? user.FirstName : firstName.Trim();
        user.LastName = string.IsNullOrWhiteSpace(lastName) ? user.LastName : lastName.Trim();
        user.Email = string.IsNullOrWhiteSpace(email) ? user.Email : email.Trim();
        user.Phone = string.IsNullOrWhiteSpace(phone) ? user.Phone : phone.Trim();

        return _users.Update(user);
    }

    public bool Delete(Guid id)
    {
        _creds.Delete(id);
        return _users.Delete(id);
    }
}