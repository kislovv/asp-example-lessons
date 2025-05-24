using ServicesExample.Domain.Models;

namespace ServicesExample.Domain.Abstractions;

public interface IStudentRepository : IRepository<long, StudentDto>
{
    
}