using ServicesExample.Domain.Models;

namespace ServicesExample.Domain.Abstractions;

public interface IUserRepository : IRepository<long, UserDto>
{
    Task<UserDto?> GetByLogin(string login);
}