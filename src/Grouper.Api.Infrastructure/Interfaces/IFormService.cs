using Grouper.Api.Infrastructure.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grouper.Api.Infrastructure.Interfaces
{
    public interface IFormService
    {
        Task<FormDto> GetById(int id);
        Task<List<FormDto>> GetByUserId(string userId);
        Task Create(FormDto formDto, string userId);
        Task Delete(int id);
        Task Update(FormDto formDto);
    }
}
