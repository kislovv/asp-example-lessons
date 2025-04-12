using System.Collections;
using ServicesExample.Entities;
using ServicesExample.Models;

namespace ServicesExample.Abstractions;

public interface IEventRepository : IRepository<Guid, EventDto>
{
    Task<ICollection<EventDto>> GetByAuthor(int authorId);
}