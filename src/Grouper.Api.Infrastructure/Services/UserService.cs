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
        private readonly JwtSecurityTokenHandler _tokenHandler;

        public UserService(IUnitOfWork dataBase, IMapper mapper, IJwtTokenGenerator tokenGenerator, JwtSecurityTokenHandler tokenHandler)
        {
            _dataBase = dataBase;
            _mapper = mapper;
            _tokenGenerator = tokenGenerator;
            _tokenHandler = tokenHandler;
        }

        public async Task<UserDto> GetInfo(string id)
        {
            var appUser = await _dataBase.UserManager.FindByIdAsync(id);
            var userDto = _mapper.Map<UserDto>(appUser);

            userDto.Role = (await _dataBase.UserManager.GetRolesAsync(appUser)).FirstOrDefault();
            return userDto;
        }

        public async Task<string> SignIn(UserDto userDto)
        {
            var appUser = await _dataBase.UserManager.FindByEmailAsync(userDto.Email);

            if (appUser is not null)
            {
                bool isAuthenticate = await _dataBase.UserManager.CheckPasswordAsync(appUser, userDto.Password);
                if (isAuthenticate)
                {
                    var verifiedUser = _mapper.Map<UserDto>(appUser);
                    verifiedUser.Role = (await _dataBase.UserManager.GetRolesAsync(appUser)).FirstOrDefault();

                    var token = await _tokenGenerator.GenerateToken(verifiedUser);

                    return _tokenHandler.WriteToken(token);
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
