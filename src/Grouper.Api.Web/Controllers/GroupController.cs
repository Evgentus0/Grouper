using AutoMapper;
using Grouper.Api.Infrastructure.Core;
using Grouper.Api.Infrastructure.DTOs;
using Grouper.Api.Infrastructure.Exceptions;
using Grouper.Api.Infrastructure.Interfaces;
using Grouper.Api.Web.Models;
using Grouper.Api.Web.Models.Outbound;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Grouper.Api.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;
        private readonly IMapper _mapper;

        public GroupController(IGroupService groupService, IMapper mapper)
        {
            _groupService = groupService;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "student,teacher", AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<List<GroupModel>>> GetByUserId([FromQuery] string userId)
        {
            List<GroupDto> groupDtos = await _groupService.GetByUserId(userId);
            var result = _mapper.Map<List<GroupModel>>(groupDtos);

            return Ok(result);  
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = "student,teacher", AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<GroupModel>> GetById(int id)
        {
            GroupDto groupDto = await _groupService.GetById(id);
            var result = _mapper.Map<GroupModel>(groupDto);

            return Ok(result);
        }

        [HttpPost]
        [Route("add-with-identificator")]
        [Authorize(Roles = "student,teacher", AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<ResponseModel>> AddUserWithIdentificator([FromBody] string identificator)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? throw new ApiException(System.Net.HttpStatusCode.InternalServerError, "Can not find name indentifier in claims");

            await _groupService.AddUserWithIdentificator(identificator, userId);

            var response = new ResponseModel { Message = "User was added" };
            return Ok(response);
        }

        [HttpPost]
        [Authorize(Roles = "teacher", AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<ResponseModel>> Create([FromBody] GroupModel group)
        {
            var groupDto = _mapper.Map<GroupDto>(group);

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? throw new ApiException(System.Net.HttpStatusCode.InternalServerError, 
                "Can not find name indentifier in claims");

            groupDto.Participants.Add(new UserDto { Id = userId });
            groupDto.CreatorId = userId;

            var newId = await _groupService.Create(groupDto);

            var response = new CreateResponseModel<int> 
            { 
                Message = "Group was created", 
                NewlyCreatedId = newId 
            };
            return Ok(response);
        }
        
        [HttpPost]
        [Route("{groupId}/add-user")]
        [Authorize(Roles = "teacher", AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<ResponseModel>> AddUser(int groupId, [FromQuery] string userEmail)
        {
            await _groupService.AddUser(groupId, userEmail);

            var response = new ResponseModel { Message = "User was added" };
            return Ok(response);
        }

        [HttpPost]
        [Route("{groupId}/delete-user")]
        [Authorize(Roles = "teacher", AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<ResponseModel>> DeleteUser(int groupId, [FromBody] List<string> userEmails)
        {
            await _groupService.DeleteUsers(groupId, userEmails);

            var response = new ResponseModel { Message = "User was deleted" };
            return Ok(response);
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "teacher", AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<ResponseModel>> Delete(int id)
        {
            await _groupService.Delete(id);

            var response = new ResponseModel { Message = "Group was deleted" };
            return Ok(response);
        }

        [HttpPut]
        [Route("")]
        [Authorize(Roles = "teacher", AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<ResponseModel>> Update([FromBody] GroupModel group)
        {
            var groupDto = _mapper.Map<GroupDto>(group);
            await _groupService.Update(groupDto);

            var response = new ResponseModel { Message = "Group was updated" };
            return Ok(response);
        }
    }
}
