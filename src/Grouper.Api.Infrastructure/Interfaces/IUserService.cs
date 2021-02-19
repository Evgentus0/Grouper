using Grouper.Api.Infrastructure.DTOs;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grouper.Api.Infrastructure.Interfaces
{
    public interface IUserService
    {
        Task<string> SignIn(UserDto userDto);
        Task SignUp(UserDto userDto);
    }
}
