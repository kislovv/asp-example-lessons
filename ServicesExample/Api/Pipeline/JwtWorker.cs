using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ServicesExample.Configurations.Options;
using ServicesExample.Domain.Abstractions;
using ServicesExample.Domain.Models;

namespace ServicesExample.Api.Pipeline;

public class JwtWorker(IOptionsMonitor<JwtOptions> options)
{
    public string? CreateJwtToken(UserDto userDto)
    {
        var claims = new List<Claim>
        {
            new (ClaimTypes.Role, userDto.Role.ToString()),
            new (ClaimTypes.Name, userDto.Login)
        };
        
        var jwtOptions = options.CurrentValue;
        var jwt = new JwtSecurityToken(
            issuer: jwtOptions.Issuer,
            audience: jwtOptions.Audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(jwtOptions.ExpiredAtMinutes),
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.IssuerSecretKey)), 
                SecurityAlgorithms.HmacSha256));
            
        var jwtToken = new JwtSecurityTokenHandler().WriteToken(jwt);
        
        return jwtToken;
    }
}