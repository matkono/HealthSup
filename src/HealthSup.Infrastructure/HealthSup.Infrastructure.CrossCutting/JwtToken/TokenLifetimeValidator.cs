using Microsoft.IdentityModel.Tokens;
using System;

namespace HealthSup.Infrastructure.CrossCutting.JwtToken
{
    public static class TokenLifetimeValidator
    {
        public static bool Validate
        (
            DateTime? notBefore,
            DateTime? expires,
            SecurityToken tokenToValidate,
            TokenValidationParameters @param
        )
        {
            return (expires != null && expires > DateTime.UtcNow);
        }
    }
}
