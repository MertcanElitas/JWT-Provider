using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiWithToken.Domain.Services;
using ApiWithToken.Extensions;
using ApiWithToken.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiWithToken.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public LoginController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        public IActionResult AccessToken(LoginResource loginResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessage());
            }
            else
            {
                var tokenResponse = _authenticationService.CreateAccessToken(loginResource.Email, loginResource.Password);

                if (tokenResponse.IsSuccess)
                {
                    return Ok(tokenResponse.Entity);
                }
                else
                {
                    return BadRequest(tokenResponse.Message);
                }
            }
        }

        [HttpPost]
        public IActionResult RefreshToken(TokenResource refreshToken)
        {
            var result = _authenticationService.CreateAccessTokenByRefreshToken(refreshToken.RefreshToken);

            if (result.IsSuccess)
                return Ok(result.Entity);

            return BadRequest(result.IsSuccess);
        }

        [HttpPost]
        public IActionResult RemoveRefreshToken(TokenResource refreshToken)
        {
            var result = _authenticationService.RemoveRefreshToken(refreshToken.RefreshToken);

            if (result.IsSuccess)
            {
                return Ok(result.Entity);
            }

            return BadRequest(result.Message);
        }
    }
}