using ServicesExample.Domain.Abstractions;
using ServicesExample.Domain.Models;

namespace ServicesExample.Infrastructure.Database.Students;

public class StudentRepository : IStudentRepository
{
    public Task<StudentDto> AddAsync(StudentDto entity)
    {
        throw new NotImplementedException();
    }

    public Task<StudentDto> UpdateAsync(StudentDto entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(StudentDto entity)
    {
        throw new NotImplementedException();
    }

    public Task<StudentDto?> GetByIdAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<StudentDto>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<int> SaveChangesAsync()
    {
        throw new NotImplementedException();
    }
}