using AutoMapper;
using Grouper.Api.Data.Entities;
using Grouper.Api.Data.Interfaces;
using Grouper.Api.Infrastructure.DTOs;
using Grouper.Api.Infrastructure.Exceptions;
using Grouper.Api.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Grouper.Api.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _dataBase;
        private readonly IMapper _mapper;
        private readonly IJwtTokenGenerator _tokenGenerator;

        public UserService(IUnitOfWork dataBase, IMapper mapper, IJwtTokenGenerator tokenGenerator)
        {
            _dataBase = dataBase;
            _mapper = mapper;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<JwtSecurityToken> SignIn(UserDto userDto)
        {
            var appUser = await _dataBase.UserManager.FindByEmailAsync(userDto.Email);

            if (appUser != null)
            {
                bool isAuthenticate = await _dataBase.UserManager.CheckPasswordAsync(appUser, userDto.Password);
                if (isAuthenticate)
                {
                    var verifiedUser = _mapper.Map<UserDto>(appUser);
                    var token = await _tokenGenerator.GenerateToken(verifiedUser);

                    return token;
                }
                else
                {
                    throw new ApiException(HttpStatusCode.BadRequest, "Incorrect login and/or password");
                }
            }
            else
            {
                throw new ApiException(HttpStatusCode.BadRequest, "Incorrect login and/or password");
            }
        }

        public async Task SignUp(UserDto userDto)
        {
            var user = _mapper.Map<ApplicationUser>(userDto);
            var password = userDto.Password;

            var result = await _dataBase.UserManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                var errorMessage = result.Errors.Select(x => $"{x.Code} - {x.Description}").ToList();

                throw new ApiException(HttpStatusCode.InternalServerError, "Can not create user", errorMessage);
            }
        }
    }
}
