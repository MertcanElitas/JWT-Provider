using ApiWithToken.Domain.Services;
using ApiWithToken.Security.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiWithToken.Responses
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserService _userService;
        private readonly ITokenHandler _tokenHandler;

        public AuthenticationService(IUserService userService, ITokenHandler tokenHandler)
        {
            _userService = userService;
            _tokenHandler = tokenHandler;
        }
        public AccessTokenResponse CreateAccessToken(string email, string password)
        {
            var userResponse = _userService.FindByEmailAndPassword(email, password);

            if (userResponse.IsSuccess)
            {
                var token = _tokenHandler.CreaeteAccessToken(userResponse.Entity);
                _userService.SaveRefreshToken(userResponse.Entity.Id, token.RefreshToken);

                return new AccessTokenResponse(token);
            }

            return new AccessTokenResponse(userResponse.Message);
        }

        public AccessTokenResponse CreateAccessTokenByRefreshToken(string refreshToken)
        {
            var userResponse = _userService.GetUserWithRefreshToken(refreshToken);

            if (userResponse.IsSuccess)
            {
                if (userResponse.Entity.RefreshTokenEndDate > DateTime.Now)
                {
                    var accessToken = _tokenHandler.CreaeteAccessToken(userResponse.Entity);
                    _userService.SaveRefreshToken(userResponse.Entity.Id, accessToken.RefreshToken);

                    return new AccessTokenResponse(accessToken);
                }
                else
                    return new AccessTokenResponse("refreshisdead");
            }

            return new AccessTokenResponse(userResponse.Message);
        }

        public AccessTokenResponse RemoveRefreshToken(string refreshToken)
        {
            var userResponse = _userService.GetUserWithRefreshToken(refreshToken);

            if (userResponse.IsSuccess)
            {
                _userService.RemoveRefreshToken(userResponse.Entity);

                return new AccessTokenResponse(new AccessToken());
            }
            else
            {
                return new AccessTokenResponse(userResponse.Message);
            }
        }
    }
}
