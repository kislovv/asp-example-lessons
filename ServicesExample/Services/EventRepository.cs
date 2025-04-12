using System.Collections;
using System.Text.Json;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ServicesExample.Abstractions;
using ServicesExample.Database;
using ServicesExample.Entities;
using ServicesExample.Models;

namespace ServicesExample.Services;

public class EventRepository(AppDbContext appDbContext,IMapper mapper) : IEventRepository
{
    public Task<EventDto> AddAsync(EventDto entity)
    {
        throw new NotImplementedException();
    }

    public Task<EventDto> UpdateAsync(EventDto entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(EventDto entity)
    {
        throw new NotImplementedException();
    }

    public Task<EventDto?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<ICollection<EventDto>> GetAllAsync()
    {
        return mapper.Map<ICollection<EventDto>>(
            await appDbContext.Events.Include(ev => ev.Author)
            .ToListAsync());
    }

    public async Task<ICollection<EventDto>> GetByAuthor(int authorId)
    {
        return mapper.Map<ICollection<EventDto>>(await appDbContext.
            Events.Where(ev => ev.AuthorId == authorId).ToListAsync());
    }

    public async Task<EventDto> GetById(Guid id)
    {
        return mapper.Map<EventDto>(await appDbContext.Events.Include(ev => ev.Author)
            .FirstOrDefaultAsync(ev => ev.Id == id));
    }
    
}