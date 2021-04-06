using AutoMapper;
using Grouper.Api.Data.Entities;
using Grouper.Api.Data.Interfaces;
using Grouper.Api.Infrastructure.DTOs;
using Grouper.Api.Infrastructure.Exceptions;
using Grouper.Api.Infrastructure.Interfaces;
using Grouper.Api.Infrastructure.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grouper.Api.Infrastructure.Services
{
    public class GroupService : IGroupService
    {
        private readonly IUnitOfWork _dataBase;
        private readonly IMapper _mapper;
        private readonly AppSettings _setting;
        private readonly IExecutionStrategy _strategy;

        public GroupService(IUnitOfWork dataBase, IMapper mapper, AppSettings setting, IExecutionStrategy strategy)
        {
            _dataBase = dataBase;
            _mapper = mapper;
            _setting = setting;
            _strategy = strategy;
        }

        public async Task AddUser(int groupId, string userEmail)
        {
            await _strategy.ExecuteAsync(async () =>
            {
                var user = await _dataBase.UserManager.FindByEmailAsync(userEmail);

                if (user is null)
                    throw new ApiException(System.Net.HttpStatusCode.BadRequest, $"User with email: {userEmail} does not exist");

                await _dataBase.GroupRepository.AddUserToGroup(groupId, user.Id);
                await _dataBase.SaveAsync();
            });
        }

        public async Task<GroupDto> AddUserWithIdentificator(string identificator, string userId)
        {
            return await _strategy.ExecuteAsync(async () =>
            {
                Group group = (await _dataBase.GroupRepository.GetByIdentificator(identificator)).group;

                await _dataBase.GroupRepository.AddUserToGroup(group.Id, userId);

                await _dataBase.SaveAsync();

                var groupDto = _mapper.Map<GroupDto>(group);

                return groupDto;
            });
        }

        public async Task<GroupDto> Create(GroupDto groupDto)
        {
            return await _strategy.ExecuteAsync(async () =>
            {
                var group = _mapper.Map<Group>(groupDto);
                group.Identificator = Guid.NewGuid().ToString();

                var newGroup = await _dataBase.GroupRepository.Create(group);

                await _dataBase.SaveAsync();

                foreach (var participant in groupDto.Participants)
                {
                    await _dataBase.GroupRepository.AddUserToGroup(group.Id, participant.Id);
                }

                await _dataBase.SaveAsync();

                var newGroupDto = _mapper.Map<GroupDto>(newGroup);

                return newGroupDto;
            });
        }

        public async Task Delete(int id)
        {
            await _strategy.ExecuteAsync(async () =>
            {
                await _dataBase.GroupRepository.Delete(id);

                await _dataBase.SaveAsync();
            });
        }

        public async Task DeleteUsers(int groupId, List<string> userEmails)
        {
            await _strategy.ExecuteAsync(async () =>
            {
                foreach (var emails in userEmails)
                {
                    var user = await _dataBase.UserManager.FindByEmailAsync(emails);
                    if (user is null)
                        continue;

                    await _dataBase.GroupRepository.DeleteUser(groupId, user.Id);
                }

                await _dataBase.SaveAsync();
            });
        }

        public async Task<GroupDto> GetById(int id)
        {
            return await _strategy.ExecuteAsync(async () =>
            {
                var result = await _dataBase.GroupRepository.GetById(id);

                var groupDto = _mapper.Map<GroupDto>(result.group);

                groupDto.Participants = _mapper.Map<List<UserDto>>(result.participants);

                return groupDto;
            });
        }

        public async Task<List<GroupDto>> GetByUserId(string userId)
        {
            return await _strategy.ExecuteAsync(async () =>
            {
                List<Group> groups = await _dataBase.GroupRepository.GetByUserId(userId);
                var groupDtos = _mapper.Map<List<GroupDto>>(groups);

                return groupDtos;
            });
        }

        public async Task Update(GroupDto groupDto)
        {
            await _strategy.ExecuteAsync(async () =>
            {
                var group = _mapper.Map<Group>(groupDto);

                await _dataBase.GroupRepository.Update(group);
                await _dataBase.SaveAsync();
            });
        }
    }
}
