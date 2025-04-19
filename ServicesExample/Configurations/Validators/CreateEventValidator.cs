using FluentValidation;
using ServicesExample.Api.Models;

namespace ServicesExample.Configurations.Validators;

/// <summary>
/// Валидация создания события
/// </summary>
public class CreateEventValidator: AbstractValidator<CreateEventRequest>
{
    public CreateEventValidator()
    {
        RuleFor(cer => cer.Name)
            .Length(3, 50)
            .WithMessage("Name must be between 3 and 50 characters");
        
        RuleFor(cer => cer.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage("Name cannot be empty or null");
        
        RuleFor(cer => cer.Quota)
            .Must(u => u > 0 )
            .WithMessage("Must be greater than zero");
        
        RuleFor(cer => cer.Place)
            .NotEmpty()
            .Length(3, 100)
            .WithMessage("Place must be between 3 and 100 characters");
        
        RuleFor(cer => cer.Score)
            .Must(u => u > 0 )
            .WithMessage("Must be greater than zero");
        
        RuleFor(cer => cer.Start)
            .Must((request, time) => time < request.End)
            .WithMessage("Start must be less than end");

        RuleFor(cer => cer.Start)
            .Must(time => time > DateTime.Now.AddDays(1))
            .WithMessage("""
                         The start date of the event must be 
                         set at least one day before the event.
                         """);
        
        RuleFor(cer => cer.AuthorId)
            .NotEmpty()
            .NotNull()
            .WithMessage("Event must have author!");
    }
}