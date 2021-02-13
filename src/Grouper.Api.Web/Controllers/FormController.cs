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
    public class FormController : ControllerBase
    {
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<FormModel>> GetById(int id)
        {
            var f = new FormModel();

            return Ok(f);
        }

        [HttpGet]
        public async Task<ActionResult<List<FormModel>>> GetByUserId([FromQuery] int userId)
        {
            var lf = new List<FormModel>();

            return Ok(lf);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseModel>> Create()
        {
            return Ok(new ResponseModel());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<ResponseModel>> Delete(int id)
        {
            return Ok(new ResponseModel());
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<ResponseModel>> Update(int id, [FromBody] FormModel post)
        {
            return Ok(new ResponseModel());
        }
    }
}
