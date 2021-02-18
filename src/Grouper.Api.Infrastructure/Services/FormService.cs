using AutoMapper;
using Grouper.Api.Data.Entities;
using Grouper.Api.Data.Interfaces;
using Grouper.Api.Infrastructure.DTOs;
using Grouper.Api.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grouper.Api.Infrastructure.Services
{
    public class FormService : IFormService
    {
        private readonly IUnitOfWork _dataBase;
        private readonly IMapper _mapper;

        public FormService(IUnitOfWork dataBase, IMapper mapper)
        {
            _dataBase = dataBase;
            _mapper = mapper;
        }
        public async Task Create(FormDto formDto)
        {
            var form = _mapper.Map<Form>(formDto);

            await _dataBase.FormRepository.Create(form);
            await _dataBase.SaveAsync();
        }

        public async Task Delete(int id)
        {
            await _dataBase.FormRepository.Delete(id);
            await _dataBase.SaveAsync();
        }

        public async Task<FormDto> GetById(int id)
        {
            Form form = await _dataBase.FormRepository.GetById(id);
            var formDto = _mapper.Map<FormDto>(form);

            return formDto;
        }

        public async Task<List<FormDto>> GetByUserId(string userId)
        {
            List<Form> forms = await _dataBase.FormRepository.GetByUserId(userId);
            var formDtos = _mapper.Map<List<FormDto>>(forms);

            return formDtos;
        }

        public async Task Update(FormDto formDto)
        {
            var form =_mapper.Map<Form>(formDto);

            await _dataBase.FormRepository.Update(form);

            await _dataBase.SaveAsync();
        }
    }
}
