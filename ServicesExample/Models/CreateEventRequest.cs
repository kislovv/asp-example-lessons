namespace ServicesExample.Models;

/// <summary>
/// Запрос на создание события
/// </summary>
public class CreateEventRequest
{
    /// <summary>
    /// Название события
    /// </summary>
    public required string Name { get; set; }
    /// <summary>
    /// Место события
    /// </summary>
    public required string Place { get; set; }
    /// <summary>
    /// Автор события
    /// </summary>
    public required string Author { get; set; }
    /// <summary>
    /// Время и дата старта события
    /// </summary>
    public required DateTime Start { get; set; }
    /// <summary>
    /// Время окончания события
    /// </summary>
    public required DateTime End { get; set; }
    /// <summary>
    /// Количество доступных мест
    /// </summary>
    public required uint Quota { get; set; }
}