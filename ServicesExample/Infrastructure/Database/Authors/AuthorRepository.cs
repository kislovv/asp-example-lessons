using ServicesExample.Domain.Abstractions;
using ServicesExample.Domain.Entities;
using ServicesExample.Domain.Models;

namespace ServicesExample.Infrastructure.Database.Authors;

public class AuthorRepository(AppDbContext dbContext) : IAuthorRepository
{
    public async Task<AuthorDto> AddAsync(AuthorDto entity)
    {
        var result = await dbContext.Authors
            .AddAsync(new Author 
            {
                Name = entity.Name,
                Login = entity.Login,
                Password = entity.Password,
                Role = UserRole.Author 
            });
        
        entity.Id = result.Entity.Id;
        entity.Role = result.Entity.Role;
        
        await dbContext.SaveChangesAsync();
        
        return entity;
    }

    public Task<AuthorDto> UpdateAsync(AuthorDto entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(AuthorDto entity)
    {
        throw new NotImplementedException();
    }

    public Task<AuthorDto?> GetByIdAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<AuthorDto>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<int> SaveChangesAsync()
    {
        throw new NotImplementedException();
    }
}