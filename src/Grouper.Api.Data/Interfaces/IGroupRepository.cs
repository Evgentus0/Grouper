using Grouper.Api.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grouper.Api.Data.Interfaces
{
    public interface IGroupRepository
    {
        Task<(Group group, List<ApplicationUser> participants)> GetById(int groupId);
        Task Update(Group group);
        Task AddUserToGroup(int groupId, string userId);
        Task<Group> Create(Group group);
        Task Delete(int id);
        Task<List<Group>> GetByUserId(string userId);
        Task<(Group group, List<ApplicationUser> participants)> GetByIdentificator(string identificator);
        Task DeleteUser(int groupId, string userId);
    }
}
