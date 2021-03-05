﻿using AutoMapper;
using Grouper.Api.Data.Entities;
using Grouper.Api.Data.Interfaces;
using Grouper.Api.Infrastructure.DTOs;
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

        public async Task AddLinks(int groupId, List<string> links)
        {
            await _strategy.ExecuteAsync(async () =>
            {
                var usefulContent = links.Aggregate((x, y) => $"{x}{_setting.UsefulLinksSeparator}{y}");
                usefulContent += _setting.UsefulLinksSeparator;
                var group = (await _dataBase.GroupRepository.GetById(groupId)).group;

                group.UsefulContent += usefulContent;

                await _dataBase.GroupRepository.Update(group);

                await _dataBase.SaveAsync();
            });
        }

        public async Task AddUser(int groupId, string userId)
        {
            await _strategy.ExecuteAsync(async () =>
            {
                await _dataBase.GroupRepository.AddUserToGroup(groupId, userId);
                await _dataBase.SaveAsync();
            });
        }

        public async Task Create(GroupDto groupDto)
        {
            await _strategy.ExecuteAsync(async () =>
            {
                var group = _mapper.Map<Group>(groupDto);

                await _dataBase.GroupRepository.Create(group);
                await _dataBase.SaveAsync();
            });
        }

        public async Task Delete(int id)
        {
            await _strategy.ExecuteAsync(async () =>
            {
                await _dataBase.GroupRepository.Delete(id);
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
