using AutoMapper;
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
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class FormController : ControllerBase
    {
        private readonly IFormService _formService;
        private readonly IMapper _mapper;

        public FormController(IFormService formService, IMapper mapper)
        {
            _formService = formService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<FormModel>> GetById(int id)
        {
            FormDto form = await _formService.GetById(id);
            var result = _mapper.Map<FormModel>(form);

            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<List<FormModel>>> GetByUserId([FromQuery] string userId)
        {
            List<FormDto> forms = await _formService.GetByUserId(userId);
            var result = _mapper.Map<List<FormDto>>(forms);

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<FormModel>> Create([FromBody] FormModel formModel)
        {
            var formDto = _mapper.Map<FormDto>(formModel);

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? throw new ApiException(System.Net.HttpStatusCode.InternalServerError, "Can not find name indentifier in claims");

            var newForm = await _formService.Create(formDto, userId);

            var newFormModel = _mapper.Map<FormModel>(newForm);

            return Ok(newFormModel);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<ResponseModel>> Delete(int id)
        {
            await _formService.Delete(id);

            var response = new ResponseModel { Message = "Form was deleted" };
            return Ok(response);
        }

        [HttpPut]
        [Route("")]
        public async Task<ActionResult<ResponseModel>> Update([FromBody] FormModel form)
        {
            var formDto = _mapper.Map<FormDto>(form);
            await _formService.Update(formDto);

            var response = new ResponseModel { Message = "Form was updated" };
            return Ok(response);
        }
    }
}
