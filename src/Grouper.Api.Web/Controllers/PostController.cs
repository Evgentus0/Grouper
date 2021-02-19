using AutoMapper;
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
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IMapper _mapper;

        public PostController(IPostService postService, IMapper mapper)
        {
            _postService = postService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<ResponseModel>> CreatePost([FromBody] PostModel post)
        {
            var postDto = _mapper.Map<PostDto>(post);
            await _postService.Create(postDto);

            var response = new ResponseModel { Message = "Post was created" };
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<List<PostModel>>> GetByGroupId([FromQuery] int groupId)
        {
            List<PostDto> postDtos = await _postService.GetByGroupId(groupId);
            var result = _mapper.Map<List<PostModel>>(postDtos);

            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<PostModel>> GetById(int id)
        {
            PostDto postDto = await _postService.GetById(id);
            var result = _mapper.Map<PostModel>(postDto);

            return Ok(result);
        }

        [HttpPost]
        [Route("add-comment")]
        public async Task<ActionResult<ResponseModel>> AddComment([FromBody]CommentModel comment)
        {
            var commentDto = _mapper.Map<CommentDto>(comment);
            await _postService.AddComment(commentDto);

            var response = new ResponseModel { Message = "Comment was added" };
            return Ok(response);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<ResponseModel>> Delete(int id)
        {
            await _postService.Delete(id);

            var response = new ResponseModel { Message = "Post was deleted" };
            return Ok(response);
        }

        [HttpPut]
        [Route("")]
        public async Task<ActionResult<ResponseModel>> Update([FromBody]PostModel post)
        {
            var postDto = _mapper.Map<PostDto>(post);
            await _postService.Update(postDto);

            var response = new ResponseModel { Message = "Post was updated" };
            return Ok(response);
        }
    }
}
