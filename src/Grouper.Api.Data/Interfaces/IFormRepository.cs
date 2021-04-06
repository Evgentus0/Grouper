using Grouper.Api.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grouper.Api.Data.Interfaces
{
    public interface IFormRepository
    {
        Task<Form> Create(Form form);
        Task Delete(int id);
        Task<Form> GetById(int id);
        Task<List<Form>> GetByUserId(string userId);
        Task Update(Form form);
    }
}
