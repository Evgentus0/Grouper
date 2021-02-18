using Grouper.Api.Infrastructure.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grouper.Api.Infrastructure.Interfaces
{
    public interface IGroupService
    {
        Task<List<GroupDto>> GetByUserId(string userId);
        Task<GroupDto> GetById(int id);
        Task Create(GroupDto groupDto);
        Task AddUser(int groupId, string userId);
        Task AddLinks(int groupId, List<string> links);
        Task Delete(int id);
        Task Update(GroupDto groupDto);
    }
}
