using AutoMapper;
using ServicesExample.Api.Models;
using ServicesExample.Api.Pipeline;
using ServicesExample.Domain.Abstractions;
using ServicesExample.Domain.Models;

namespace ServicesExample.Api.Endpoints;

public static class UserEndpoint
{
    public static IEndpointRouteBuilder MapUsers(this IEndpointRouteBuilder endpoints)
    {
        var userGroup = endpoints.MapGroup("/user").WithTags("Users");
        
        userGroup.MapPost("auth", async (UserLoginModel userLoginModel,
            IAuthService authService) =>
        {
            var result = await authService.AuthenticateAsync(userLoginModel.Login, 
                userLoginModel.Password);
            
            return result.IsSuccess
                ? Results.Ok(new { token = result.Value })
                : Results.Unauthorized();
        });
        
        
        userGroup.MapPost("registration", async (CreateUserRequest createUserRequest,
            IKeyedServiceFactory keyedServiceFactory, IMapper mapper) =>
        {
            var registrationService =
                keyedServiceFactory.GetService<IUserRegistrationService>(createUserRequest.Role.ToString());

            var result = await registrationService.RegistrationUser(mapper.Map<UserDto>(createUserRequest));

            return Results.Ok(result);
        });
        
        return endpoints;

       
    }
}