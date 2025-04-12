using System.Collections;
using ServicesExample.Domain.Models;

namespace ServicesExample.Abstractions;

public interface IEventRepository : IRepository<Guid, EventDto>
{
    Task<ICollection<EventDto>> GetByAuthor(int authorId);
}