using Grouper.Api.Data.Context;
using Grouper.Api.Data.Entities;
using Grouper.Api.Data.Exceptions;
using Grouper.Api.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grouper.Api.Data.Repositories
{
    public class FormRepository : IFormRepository
    {
        private readonly GrouperDbContext _context;

        public FormRepository(GrouperDbContext context)
        {
            _context = context;
        }

        public async Task Create(Form form)
        {
            await _context.Forms.AddAsync(form);
        }

        public async Task Delete(int id)
        {
            var form = await _context.Forms.FirstOrDefaultAsync(x => x.Id == id);
            if (form != null)
            {
                _context.Forms.Remove(form);
            }
        }

        public async Task<Form> GetById(int id)
        {
            return await _context.Forms.FirstOrDefaultAsync(x => x.Id == id); ;
        }

        public async Task<List<Form>> GetByUserId(string userId)
        {
            var user = await _context.Users.Include(x => x.Forms)
                .FirstOrDefaultAsync(x => x.Id == userId);

            if (user != null)
            {
                return user.Forms;
            }
            else
            {
                throw new DataApiException(nameof(_context.Users),
                    $"User with id {userId} does not exist");
            }
        }

        public Task Update(Form form)
        {
            _context.Forms.Update(form);

            return Task.CompletedTask;
        }
    }
}
