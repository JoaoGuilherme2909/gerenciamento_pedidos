using gerenciamento_pedidos.api.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace gerenciamento_pedidos.api.Services;

public class TokenService
{
    public string GenerateToken(User user) 
    {
        Claim[] claims = new Claim[]
        {
            new Claim("username", user.UserName),
            new Claim("role", user.Role),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("AENFIASGIJWGFVUIQEFB0WFYB12345678"));


        var signingCredetials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256); 

        var token = new JwtSecurityToken(
            expires: DateTime.Now.AddHours(72),
            claims: claims,
            signingCredentials: signingCredetials
            );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
