using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using TT.Models.Authentication;
using TT.Security;

namespace TT.ExtensionMethods;

public static class SerivceConfigureExtensions
{
    public static void AddJwt(this IServiceCollection services, AuthOptionsModel authModel) 
    {
        var authConfig = authModel ?? throw new ArgumentNullException(nameof(authModel));
        if (String.IsNullOrEmpty(authModel.Key))
            throw new ArgumentNullException(nameof(authModel.Key));

        var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(authModel.Key));

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(jwtOptinons =>
                {
                    jwtOptinons.RequireHttpsMetadata = false;
                    jwtOptinons.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = authModel.Issuer ?? throw new ArgumentNullException(nameof(authModel.Issuer)),
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = key,

                        ValidateAudience = true,
                        ValidAudience = authModel.Audience ?? throw new ArgumentNullException(nameof(authModel.Audience)),

                        ValidateLifetime = true,
                    };
                });

        Authentication.SetAuthOptions(authConfig);
    }
}
