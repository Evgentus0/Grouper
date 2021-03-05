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
        private readonly IExecutionStrategy _strategy;

        public FormService(IUnitOfWork dataBase, IMapper mapper, IExecutionStrategy strategy)
        {
            _dataBase = dataBase;
            _mapper = mapper;
            _strategy = strategy;
        }
        public async Task Create(FormDto formDto)
        {
            await _strategy.ExecuteAsync(async () =>
            {
                var form = _mapper.Map<Form>(formDto);

                await _dataBase.FormRepository.Create(form);
                await _dataBase.SaveAsync();
            });
        }

        public async Task Delete(int id)
        {
            await _strategy.ExecuteAsync(async () =>
            {
                await _dataBase.FormRepository.Delete(id);
                await _dataBase.SaveAsync();
            });
        }

        public async Task<FormDto> GetById(int id)
        {
            return await _strategy.ExecuteAsync(async () =>
            {
                Form form = await _dataBase.FormRepository.GetById(id);
                var formDto = _mapper.Map<FormDto>(form);

                return formDto;
            });
        }

        public async Task<List<FormDto>> GetByUserId(string userId)
        {
            return await _strategy.ExecuteAsync(async () =>
            {
                List<Form> forms = await _dataBase.FormRepository.GetByUserId(userId);
                var formDtos = _mapper.Map<List<FormDto>>(forms);

                return formDtos;
            });
        }

        public async Task Update(FormDto formDto)
        {
            await _strategy.ExecuteAsync(async () =>
            {
                var form = _mapper.Map<Form>(formDto);

                await _dataBase.FormRepository.Update(form);

                await _dataBase.SaveAsync();
            });
        }
    }
}
