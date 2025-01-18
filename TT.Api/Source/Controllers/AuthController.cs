using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TT.Models.Authentication;
using TT.Security;

namespace TT.Source.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class AuthController : ControllerBase
{
    #region DeleteMe
    private class LikeUser 
    {
        public string UserName { get; set; } 
        public string Password { get; set; }
    }
    private List<LikeUser> _users = new List<LikeUser>()
    {
        new LikeUser { UserName = "Bob", Password="123" }
    };
    #endregion

    [HttpGet]
    public TokentModel GetTokent([Required] string userName, [Required] string password) 
    {
        var user = _users.FirstOrDefault(x => x.UserName == userName && x.Password == password);
        if (user == default)
        {
            return null;
        }
        var myClaims = new List<Claim>() { new Claim(ClaimTypes.Name, userName) };
        var authOptions = Authentication.Options;
        var jwt = new JwtSecurityToken(issuer: authOptions.Issuer
            , audience: authOptions.Audience
            , claims: myClaims
            , expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(authOptions.Lifetime))
            , signingCredentials: new SigningCredentials(Authentication.GetSymmetricKey(authOptions.Key), SecurityAlgorithms.HmacSha256)
        );

        return new TokentModel() { AccessToken = new JwtSecurityTokenHandler().WriteToken(jwt) };
    }
}
