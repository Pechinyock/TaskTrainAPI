using System.Text;
using Microsoft.IdentityModel.Tokens;
using TT.Models.Authentication;

namespace TT.Security;

public static class Authentication
{
    private static bool IsOptionsValueSet = false;

    private static AuthOptionsModel Options;

    public static void SetAuthOptions(AuthOptionsModel value) 
    {
        if (IsOptionsValueSet)
            throw new InvalidOperationException();

        Options = value;
        IsOptionsValueSet = true;
    }

    public static AuthOptionsModel GetAuthOptions() => Options;

    public static SymmetricSecurityKey GetSymmetricKey(string key) => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
}
