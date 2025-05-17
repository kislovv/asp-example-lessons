using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ServicesExample.Domain.Abstractions;
using ServicesExample.Domain.Models;

namespace ServicesExample.Infrastructure.Database.Users;

public class UserRepository(AppDbContext dbContext, IMapper mapper): IUserRepository
{
    public Task<UserDto> AddAsync(UserDto entity)
    {
        throw new NotImplementedException();
    }

    public Task<UserDto> UpdateAsync(UserDto entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(UserDto entity)
    {
        throw new NotImplementedException();
    }

    public Task<UserDto?> GetByIdAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<UserDto>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<int> SaveChangesAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<UserDto> GetByLogin(string login)
    {
        var user = await dbContext.Users.SingleOrDefaultAsync(u => u.Login == login);
        
        return mapper.Map<UserDto>(user);
    }
}