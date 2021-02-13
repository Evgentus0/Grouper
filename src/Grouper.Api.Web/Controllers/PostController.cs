using Grouper.Api.Web.Models;
using Grouper.Api.Web.Models.Outbound;
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
    public class PostController : ControllerBase
    {

        [HttpPost]
        public async Task<ActionResult<ResponseModel>> CreatePost([FromQuery] int groupId, [FromBody] PostModel post)
        {
            var r = new ResponseModel();

            return Ok(r);
        }

        [HttpGet]
        public async Task<ActionResult<List<PostModel>>> GetByGroupId([FromQuery] int groupId)
        {
            var lp = new List<PostModel>();

            return Ok(lp);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<PostModel>> GetById(int id)
        {
            var p = new PostModel();
            return Ok(p);
        }

        [HttpPost]
        [Route("{postId}/add-comment")]
        public async Task<ActionResult<ResponseModel>> AddComment(int postId, [FromBody]CommentModel comment)
        {
            var p = new ResponseModel();
            return Ok(p);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<ResponseModel>> Delete(int id)
        {
            return Ok(new ResponseModel());
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<ResponseModel>> Update(int id, [FromBody]PostModel post)
        {
            return Ok(new ResponseModel());
        }
    }
}
