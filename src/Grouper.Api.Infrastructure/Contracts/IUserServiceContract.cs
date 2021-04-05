using Grouper.Api.Infrastructure.DTOs;
using Grouper.Api.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grouper.Api.Infrastructure.Contracts
{
    [ContractClassFor(typeof(IUserService))]
    class IUserServiceContract : IUserService
    {
        public Task<UserDto> GetInfo(string id)
        {
            Contract.Requires(!string.IsNullOrEmpty(id));
            Contract.Ensures(!string.IsNullOrEmpty(Contract.Result<UserDto>()?.Id));

            return default;
        }

        public Task<string> SignIn(UserDto userDto)
        {
            Contract.Requires(!string.IsNullOrEmpty(userDto.Email) && !string.IsNullOrEmpty(userDto.Password), "email or password is null");

            return default;
        }

        public Task SignUp(UserDto userDto)
        {
            Contract.Requires(!string.IsNullOrEmpty(userDto.Email) && !string.IsNullOrEmpty(userDto.Password));

            return default;
        }
    }
}
