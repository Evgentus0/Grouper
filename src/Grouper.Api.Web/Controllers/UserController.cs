using Grouper.Api.Web.Models;
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
    [AllowAnonymous]
    public class UserController : ControllerBase
    {
        [HttpPost]
        [Route("sign-in")]
        public async Task<ActionResult<UserModel>> SignIn([FromBody] UserModel user)
        {
            var u = new UserModel();

            return Ok(u);
        }

        [HttpPost]
        [Route("sign-up")]
        public async Task<ActionResult<UserModel>> SignUp([FromBody] UserModel user)
        {
            var u = new UserModel();

            return Ok(u);
        }
    }
}
