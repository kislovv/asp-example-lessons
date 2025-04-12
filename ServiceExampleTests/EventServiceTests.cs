using AutoFixture;
using Moq;
using ServicesExample.Abstractions;
using ServicesExample.Models;
using ServicesExample.Services;

namespace ServiceExampleTests;

public class EventServiceTests
{
    private Mock<IEventRepository> _eventRepositoryMock;
    private Mock<IQuotesService> _quotesServiceMock;
    private readonly Fixture _fixture;
    public EventServiceTests()
    {
        _eventRepositoryMock = new Mock<IEventRepository>();
        _quotesServiceMock = new Mock<IQuotesService>();
        _fixture = new Fixture();
    }
    
    [Fact]
    public async Task Event_Has_Free_Quota_Should_Be_Return_True()
    {
        //Arrange
        var eventDto = _fixture.Build<EventDto>()
            .With(dto => dto.Quota, 2u)
            .Create();
        
        _eventRepositoryMock.Setup(repository => repository.GetByIdAsync(eventDto.Id))
            .ReturnsAsync(eventDto);

        var eventService = new EventService(_eventRepositoryMock.Object, _quotesServiceMock.Object);
        
        //Act
        var result = await eventService.RegistrationEventAsync(eventDto.Id);
        
        //Assert
        Assert.True(result);
        Assert.True(eventDto.Quota == 1);
    }
    
    [Fact]
    public async Task Event_Has_Not_Free_Quota_Should_Be_Return_True()
    {
        //Arrange
        var eventDto = _fixture.Build<EventDto>()
            .With(dto => dto.Quota, 0u)
            .Create();
        
        _eventRepositoryMock.Setup(repository => repository.GetByIdAsync(eventDto.Id))
            .ReturnsAsync(eventDto);

        var eventService = new EventService(_eventRepositoryMock.Object, _quotesServiceMock.Object);
        
        //Act
        var result = await eventService.RegistrationEventAsync(eventDto.Id);
        
        //Assert
        Assert.False(result);
    }
    
}