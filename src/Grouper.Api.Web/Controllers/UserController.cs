using AutoMapper;
using Grouper.Api.Infrastructure.DTOs;
using Grouper.Api.Infrastructure.Exceptions;
using Grouper.Api.Infrastructure.Interfaces;
using Grouper.Api.Web.Models;
using Grouper.Api.Web.Models.Outbound;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Grouper.Api.Web.Controllers
{
    [Route("api/[controller]")]
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

        [HttpPost]
        [AllowAnonymous]
        [Route("sign-in")]
        public async Task<ActionResult<string>> SignIn([FromBody] UserModel user)
        {
            var userDto = _mapper.Map<UserDto>(user);
            string token = await _userService.SignIn(userDto);

            return Ok(new { token = token });
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("sign-up")]
        public async Task<ActionResult<ResponseModel>> SignUp([FromBody] UserModel user)
        {
            var userDto = _mapper.Map<UserDto>(user);
            await _userService.SignUp(userDto);

            var result = new ResponseModel { Message = "User was created" };

            return Ok(result);
        }

        [HttpGet]
        [Route("user-info")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<UserModel>> GetInfo()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value 
                ?? throw new ApiException(System.Net.HttpStatusCode.InternalServerError, "Can not find name indentifier in claims");

            UserDto user = await _userService.GetInfo(userId);

            var result = _mapper.Map<UserModel>(user);

            return Ok(result);
        }
    }
}
