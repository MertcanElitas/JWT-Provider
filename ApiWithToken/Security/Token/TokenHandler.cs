using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using ApiWithToken.Domain.Model;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ApiWithToken.Security.Token
{
    public class TokenHandler : ITokenHandler
    {
        private readonly TokenOptions _tokenOptions;

        public TokenHandler(IOptions<TokenOptions> tokenOptions)
        {
            _tokenOptions = tokenOptions.Value;
        }
        public AccessToken CreaeteAccessToken(Users user)
        {
            var tokenExpireDate = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
            var securityKey = SignHandle.GetSecurityKey (_tokenOptions.SecurityKey);

            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var userClaims = GetUserClaims(user);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _tokenOptions.Issuer, //Tokenı üreten uygulamanın kim olduğunu set ettik.
                audience: _tokenOptions.Audience, //Tokenı kullanacak olan clientları set ettik.
                notBefore: DateTime.Now, //Şuandan itibaren bu oluşturduğumuz tokenın geçerli olduğunu belirttik. DateTime.Now.AddMinutes(5) dersek 5 dk sonra geçerli olur.
                expires: tokenExpireDate, // Tokenın expire olacağı zamanı set ettik.
                signingCredentials: signingCredentials, //Tokenın oluşturulacağı algoritmayı set ettik.
                claims: userClaims
                );

            var jwtHandler = new JwtSecurityTokenHandler();
            var token = jwtHandler.WriteToken(jwtSecurityToken);

            var result = new AccessToken()
            {
                Expiration = tokenExpireDate,
                RefreshToken = CreateRefreshToken(),
                Token = token
            };

            return result;
        }

        public void RemoveRefreshToken(Users user)
        {
            user.RefreshToken = null;
        }

        private IEnumerable<Claim> GetUserClaims(Users user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim(ClaimTypes.Name,$"{user.Name} {user.Surname}"),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()) //Tokena bir id vermiş olduk.Standart olduğu için yaptık.
            };

            return claims;
        }

        private string CreateRefreshToken()
        {
            var number = new Byte[32];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(number);
                return Convert.ToBase64String(number);
            }
        }
    }
}
