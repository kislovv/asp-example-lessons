using ServicesExample.Domain.Abstractions;

namespace ServicesExample.Domain.Services;

public class ScoreEndedEventsByLastDayJob(IEventRepository eventRepository)
{
    public async Task UpdateScoreAllStudentsByLastDay()
    {
        var events = await eventRepository.GetAllWhenEndedLastDay();
        foreach (var ev in events)
        {
            ev.Students.ForEach(st => st.Score+= ev.Score);
            await eventRepository.UpdateAsync(ev);
        }
        
        await eventRepository.SaveChangesAsync();
    }
}