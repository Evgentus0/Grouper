using Grouper.Api.Data.Context;
using Grouper.Api.Data.Entities;
using Grouper.Api.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grouper.Api.Data.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly GrouperDbContext _context;

        public GroupRepository(GrouperDbContext context)
        {
            _context = context;
        }

        public async Task AddUserToGroup(int groupId, string userId)
        {
            var userGroup = new UserGroup { GroupId = groupId, UserId = userId };
            await _context.UsersGroups.AddAsync(userGroup);
        }

        public async Task Create(Group group)
        {
            await _context.Groups.AddAsync(group);
        }

        public async Task Delete(int id)
        {
            var group = await _context.Groups.FirstOrDefaultAsync(x => x.Id == id);

            if (group is not null)
            {
                _context.Groups.Remove(group);
            }
        }

        public async Task<Group> GetById(int groupId)
        {
            return await _context.Groups.FirstOrDefaultAsync(x => x.Id == groupId);
        }

        public async Task<List<Group>> GetByUserId(string userId)
        {
            return await _context.UsersGroups
                .Include(x => x.Group)
                .Where(x => x.UserId == userId)
                .Select(x => x.Group)
                .ToListAsync();
        }

        public Task Update(Group group)
        {
            _context.Groups.Update(group);

            return Task.CompletedTask;
        }
    }
}
