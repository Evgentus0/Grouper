using Grouper.Api.Infrastructure.Contracts;
using Grouper.Api.Infrastructure.DTOs;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grouper.Api.Infrastructure.Interfaces
{
    [ContractClass(typeof(IGroupServiceContract))]
    public interface IGroupService
    {
        Task<List<GroupDto>> GetByUserId(string userId);
        Task<GroupDto> GetById(int id);
        Task<int> Create(GroupDto groupDto);
        Task AddUser(int groupId, string userEmail);
        Task Delete(int id);
        Task Update(GroupDto groupDto);
        Task AddUserWithIdentificator(string identificator, string userId);
        Task DeleteUsers(int groupId, List<string> userEmails);
    }
}
