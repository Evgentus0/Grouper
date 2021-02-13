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
    public class GroupController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<GroupModel>>> Get([FromQuery] int userId)
        {
            var groups = new List<GroupModel>();
            return Ok(groups);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<GroupModel>> GetById(int id)
        {
            var group = new GroupModel();
            return Ok(group);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseModel>> Create([FromBody] GroupModel group)
        {
            var resp = new ResponseModel();

            return Ok(resp);
        }
        
        [HttpPost]
        [Route("{groupId}/add-user")]
        public async Task<ActionResult<ResponseModel>> AddUser(int groupId, [FromQuery]int userId)
        {
            var r = new ResponseModel();

            return Ok(r);
        }

        [HttpPost]
        [Route("{groupId}/add-links")]
        public async Task<ActionResult<ResponseModel>> AddUser(int groupId, [FromBody] List<string> links)
        {
            var r = new ResponseModel();

            return Ok(r);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<ResponseModel>> Delete(int id)
        {
            return Ok(new ResponseModel());
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<ResponseModel>> Update(int id, [FromBody] GroupModel post)
        {
            return Ok(new ResponseModel());
        }
    }
}
