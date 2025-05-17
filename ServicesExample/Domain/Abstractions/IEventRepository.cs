using ServicesExample.Domain.Models;

namespace ServicesExample.Domain.Abstractions;

public interface IEventRepository : IRepository<Guid, EventDto>
{
    Task<ICollection<EventDto>> GetByAuthor(int authorId);
    Task<ICollection<EventDto>> GetAllWhenEndedLastDay();
}