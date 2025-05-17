namespace ServicesExample.Api.Endpoints;

public static class StudentEndpoints
{
    public static IEndpointRouteBuilder MapStudents(this IEndpointRouteBuilder endpoints)
    {
        var studentGroup = endpoints.MapGroup("/students").WithTags("Students");
        
        
        return endpoints;
    }
}