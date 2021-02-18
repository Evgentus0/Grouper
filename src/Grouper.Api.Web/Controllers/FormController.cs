﻿using AutoMapper;
using Grouper.Api.Infrastructure.DTOs;
using Grouper.Api.Infrastructure.Interfaces;
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
        public async Task<ActionResult<ResponseModel>> Create([FromBody]FormModel formModel)
        {
            var formDto = _mapper.Map<FormDto>(formModel);
            await _formService.Create(formDto);

            var response = new ResponseModel { Message = "Form was created" };
            return Ok(response);
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
