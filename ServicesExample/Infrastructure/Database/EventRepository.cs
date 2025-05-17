using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ServicesExample.Domain.Abstractions;
using ServicesExample.Domain.Entities;
using ServicesExample.Domain.Models;

namespace ServicesExample.Infrastructure.Database;

public class EventRepository(AppDbContext appDbContext,IMapper mapper) : IEventRepository
{
    public async Task<EventDto> AddAsync(EventDto entity)
    {
        var result = await appDbContext.Events.AddAsync(
            mapper.Map<Event>(entity));
        
        return mapper.Map<EventDto>(result.Entity);
    }

    public async Task<EventDto> UpdateAsync(EventDto dto)
    {
        var existingEntity = await appDbContext.Events
            .Include(e => e.Students)
            .Include(e => e.Author)
            .FirstOrDefaultAsync(e => e.Id == dto.Id);

        if (existingEntity is null)
            throw new Exception("Сущность не найдена");
        
        mapper.Map(dto, existingEntity);

        return dto;
    }

    public async Task DeleteAsync(EventDto entity)
    {
        var existingEntity = await appDbContext.Events
            .Include(e => e.Students)
            .Include(e => e.Author)
            .FirstOrDefaultAsync(e => e.Id == entity.Id);

        if (existingEntity is null)
            throw new Exception("Сущность не найдена");
        
        existingEntity.IsDeleted = true;
        
    }

    public async Task<EventDto?> GetByIdAsync(Guid id)
    {
        var entity = await appDbContext.Events
            .Include(e => e.Author)
            .Include(e => e.Students)
            .FirstOrDefaultAsync(e => e.Id == id);

        return mapper.Map<EventDto?>(entity);
    }

    public async Task<ICollection<EventDto>> GetAllAsync()
    {
        return mapper.Map<ICollection<EventDto>>(
            await appDbContext.Events
                .Include(ev => ev.Author)
                .Include(ev => ev.Students)
            .ToListAsync());
    }

    public async Task<int> SaveChangesAsync()
    {
        return await appDbContext.SaveChangesAsync();
    }

    public async Task<ICollection<EventDto>> GetByAuthor(int authorId)
    {
        return mapper.Map<ICollection<EventDto>>(await appDbContext.
            Events.Include(ev => ev.Author)
            .Where(ev => ev.AuthorId == authorId).ToListAsync());
    }
    public async Task<ICollection<EventDto>> GetAllWhenEndedLastDay()
    {
        return mapper.Map<ICollection<EventDto>>(await appDbContext.Events
            .Include(ev => ev.Author)
            .Include(ev => ev.Students)
            .Where(ev => ev.DateTimeOfEnd < DateTime.Today && 
                         ev.DateTimeOfEnd > DateTime.Today.AddDays(-2))
            .ToListAsync());
    }
}