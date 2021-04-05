using Grouper.Api.Infrastructure.DTOs;
using Grouper.Api.Infrastructure.Exceptions;
using Grouper.Api.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grouper.Api.Infrastructure.Contracts
{
    [ContractClassFor(typeof(IGroupService))]
    internal abstract class IGroupServiceContract : IGroupService
    {
        public Task AddUser(int groupId, string userEmail)
        {
            Contract.Requires<ApiException>(groupId > 0, $"{nameof(groupId)} less than 0");
            Contract.Requires<ApiException>(!string.IsNullOrEmpty(userEmail), 
                $"{nameof(userEmail)} is null or empty");

            return default;
        }

        public Task AddUserWithIdentificator(string identificator, string userId)
        {
            Contract.Requires<ApiException>(!string.IsNullOrEmpty(identificator), 
                $"{nameof(identificator)} is null or empty");
            Contract.Requires<ApiException>(!string.IsNullOrEmpty(userId),
                $"{nameof(userId)} is null or empty");

            return default;
        }

        public Task<int> Create(GroupDto groupDto)
        {
            Contract.Requires<ApiException>(groupDto is not null,
                $"{nameof(groupDto)} is null");

            Contract.Ensures(Contract.Result<int>() > 0);

            return default;
        }

        public Task Delete(int id)
        {
            Contract.Requires<ApiException>(id > 0, $"{nameof(id)} less than 0");

            return default;
        }

        public Task DeleteUsers(int groupId, List<string> userEmails)
        {
            Contract.Requires<ApiException>(groupId > 0, $"{nameof(groupId)} less than 0");
            Contract.Requires(Contract.ForAll(userEmails, userEmail => !string.IsNullOrEmpty(userEmail)));

            return default;
        }

        public Task<GroupDto> GetById(int id)
        {
            Contract.Requires<ApiException>(id > 0, $"{nameof(id)} less than 0");
            Contract.Ensures(Contract.Result<GroupDto>().Id > 0);

            return default;
        }

        public Task<List<GroupDto>> GetByUserId(string userId)
        {
            Contract.Requires(!string.IsNullOrEmpty(userId));
            Contract.Ensures(Contract.ForAll(Contract.Result<List<GroupDto>>(), group => group.Id > 0));

            return default;
        }

        public Task Update(GroupDto groupDto)
        {
            Contract.Requires(groupDto.Id > 0);

            return default;
        }
    }
}
