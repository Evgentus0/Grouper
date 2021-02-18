﻿using AutoMapper;
using Grouper.Api.Infrastructure.DTOs;
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
using System.Threading.Tasks;

namespace Grouper.Api.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
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
        [Route("sign-in")]
        public async Task<ActionResult<JwtSecurityToken>> SignIn([FromBody] UserModel user)
        {
            var userDto = _mapper.Map<UserDto>(user);
            JwtSecurityToken token = await _userService.SignIn(userDto);

            return Ok(token);
        }

        [HttpPost]
        [Route("sign-up")]
        public async Task<ActionResult<ResponseModel>> SignUp([FromBody] UserModel user)
        {
            var userDto = _mapper.Map<UserDto>(user);
            await _userService.SignUp(userDto);

            var result = new ResponseModel { Message = "User was created" };

            return Ok(result);
        }
    }
}
