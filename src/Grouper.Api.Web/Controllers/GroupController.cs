using AutoMapper;
using Grouper.Api.Infrastructure.Core;
using Grouper.Api.Infrastructure.DTOs;
using Grouper.Api.Infrastructure.Interfaces;
using Grouper.Api.Web.Models;
using Grouper.Api.Web.Models.Outbound;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grouper.Api.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer",
        Roles = "teacher")]
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
        [Authorize(Roles = "student,teacher")]
        public async Task<ActionResult<List<GroupModel>>> Get([FromQuery] string userId)
        {
            List<GroupDto> groupDtos = await _groupService.GetByUserId(userId);
            var result = _mapper.Map<List<GroupModel>>(groupDtos);

            return Ok(result);  
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = "student,teacher")]
        public async Task<ActionResult<GroupModel>> GetById(int id)
        {
            GroupDto groupDto = await _groupService.GetById(id);
            var result = _mapper.Map<GroupModel>(groupDto);

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseModel>> Create([FromBody] GroupModel group)
        {
            var groupDto = _mapper.Map<GroupDto>(group);
            await _groupService.Create(groupDto);

            var response = new ResponseModel { Message = "Group was created" };
            return Ok(response);
        }
        
        [HttpPost]
        [Route("{groupId}/add-user")]
        public async Task<ActionResult<ResponseModel>> AddUser(int groupId, [FromQuery] string userId)
        {
            await _groupService.AddUser(groupId, userId);

            var response = new ResponseModel { Message = "User was added" };
            return Ok(response);
        }

        [HttpPost]
        [Route("{groupId}/add-links")]
        public async Task<ActionResult<ResponseModel>> AddLinks(int groupId, [FromBody] List<string> links)
        {
            await _groupService.AddLinks(groupId, links);

            var response = new ResponseModel { Message = "Links was added" };
            return Ok(response);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<ResponseModel>> Delete(int id)
        {
            await _groupService.Delete(id);

            var response = new ResponseModel { Message = "Group was deleted" };
            return Ok(response);
        }

        [HttpPut]
        [Route("")]
        public async Task<ActionResult<ResponseModel>> Update([FromBody] GroupModel group)
        {
            var groupDto = _mapper.Map<GroupDto>(group);
            await _groupService.Update(groupDto);

            var response = new ResponseModel { Message = "Group was updated" };
            return Ok(response);
        }
    }
}
