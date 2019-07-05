using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWT.api.Security
{
    internal class TokenServices
    {
        private static readonly string _audience = "member";
        private static readonly string _issuer = "The_Authority";
        // https://www.allkeysgenerator.com/Random/Security-Encryption-Key-Generator.aspx
        private static readonly string _signingKey = "858719B8E4CE5FF5E6B5B7D67DC45";
        private static readonly string _securityKey256 = "KbPeShVmYq3t6v9y$B&E)H@McQfTjWnZ";
        public static readonly int _expirationInMinute = 5;

        public static void AddAuthenticationService(IServiceCollection services, IConfiguration configuration)
        {
            var authBuilder = services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            });

            authBuilder.AddJwtBearer(option =>
            {
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(_signingKey)),
                    TokenDecryptionKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(_securityKey256)),
                    ValidAudience = _audience,
                    ValidIssuer = _issuer,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
        }

        public static string CreateToken(IEnumerable<Claim> claims)
        {
            var keySigning = new SymmetricSecurityKey(Encoding.Default.GetBytes(_signingKey));
            var signingCredentials = new SigningCredentials(keySigning, SecurityAlgorithms.HmacSha256);

            var keyEncrypting = new SymmetricSecurityKey(Encoding.Default.GetBytes(_securityKey256));
            var encryptingCredentials = new EncryptingCredentials(keyEncrypting, SecurityAlgorithms.Aes256KW, SecurityAlgorithms.Aes256CbcHmacSha512);

            var handler = new JwtSecurityTokenHandler();

            var token = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _issuer,
                Audience = _audience,
                Subject = new ClaimsIdentity(claims),
                NotBefore = DateTime.Now,
                Expires = DateTime.Now.AddMinutes(_expirationInMinute),
                IssuedAt = DateTime.Now,
                SigningCredentials = signingCredentials,
                EncryptingCredentials = encryptingCredentials,
            });

            return handler.WriteToken(token);
        }
    }
}
