using AutoMapper;
using ServicesExample.Api.Models;
using ServicesExample.Domain.Abstractions;
using ServicesExample.Domain.Models;

namespace ServicesExample.Api.Endpoints;

public static class AuthorsEndpoint
{
    public static IEndpointRouteBuilder MapAuthors(this IEndpointRouteBuilder endpoints)
    {
        var authorGroup = endpoints.MapGroup("/author").WithTags("Authors");
        
        authorGroup.MapPost("/registration", 
            async (IAuthorRepository authorRepository, IMapper mapper,
                CreateAutorRequest authorRequest) =>
            {
                var result = await authorRepository.AddAsync(
                    mapper.Map<AuthorDto>(authorRequest));
    
                return Results.Ok(result);
            });
        
        return endpoints;
    }
}