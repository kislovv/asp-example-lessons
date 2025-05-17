using ServicesExample.Api.Models;
using ServicesExample.Api.Pipeline;
using ServicesExample.Domain.Abstractions;

namespace ServicesExample.Api.Endpoints;

public static class UserEndpoint
{
    public static IEndpointRouteBuilder MapUsers(this IEndpointRouteBuilder endpoints)
    {
        var userGroup = endpoints.MapGroup("/user").WithTags("Users");
        
        userGroup.MapPost("auth", async (UserLoginModel userLoginModel,
            IUserRepository userRepository, JwtWorker jwtWorker) =>
        {
            var user = await userRepository.GetByLogin(userLoginModel.Login);
            if (user == null)
            {
                return Results.Unauthorized();
            }

            var jwt = jwtWorker.CreateJwtToken(user);
            
            return Results.Ok(new
            {
                token = jwt
            });
        });
        
        return endpoints;
    }
}