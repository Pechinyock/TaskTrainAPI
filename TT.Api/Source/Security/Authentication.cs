using System.Text;
using Microsoft.IdentityModel.Tokens;
using TT.Models.Authentication;

namespace TT.Security;

public static class Authentication
{
    public static AuthOptionsModel Options;

    public static SymmetricSecurityKey GetSymmetricKey(string key) => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
}
