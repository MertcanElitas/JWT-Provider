using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ApiWithToken.Domain.Model;
using ApiWithToken.Domain.Services;
using ApiWithToken.Resources;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiWithToken.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [Authorize]
        public IActionResult GetUser()
        {
            var claims = User.Claims;

            var userId = Convert.ToInt32(claims.Where(x => x.Type == ClaimTypes.NameIdentifier).First().Value);
            var result = _userService.FindById(userId);

            if (result.IsSuccess)
                return Ok(result.Entity);


            return BadRequest(result.Message);
        }

        [AllowAnonymous]
        public IActionResult AddUser(UserResource userResource)
        {
            var user = _mapper.Map<UserResource, Users>(userResource);
            var response = _userService.AddUser(user);

            if (response.IsSuccess)
                return Ok(response.Entity);

            return BadRequest(response.Message);
        }
    }
}