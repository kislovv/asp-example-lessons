using ServicesExample.Domain.Models;

namespace ServicesExample.Domain.Abstractions;

public interface IAuthorRepository: IRepository<long, AuthorDto>
{
    
}