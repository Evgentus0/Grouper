﻿using Grouper.Api.Data.Context;
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

        public async Task<Form> Create(Form form)
        {
            var entity = await _context.Forms.AddAsync(form);
            return entity.Entity;
        }

        public async Task Delete(int id)
        {
            var form = await _context.Forms.FirstOrDefaultAsync(x => x.Id == id);
            if (form is not null)
            {
                _context.Forms.Remove(form);
            }
        }

        public async Task<Form> GetById(int id)
        {
            return await _context.Forms
                .FirstOrDefaultAsync(x => x.Id == id); ;
        }

        public async Task<List<Form>> GetByUserId(string userId)
        {
            var user = await _context.Users.Include(x => x.Forms)
                                        .FirstOrDefaultAsync(x => x.Id == userId);

            if (user is not null)
            {
                return user.Forms;
            }
            else
            {
                throw new DataApiException(nameof(_context.Users),
                    $"User with id {userId} does not exist");
            }
        }

        public async Task Update(Form form)
        {
            var updateForm = await _context.Forms.FirstOrDefaultAsync(x => x.Id == form.Id);

            if (updateForm is null)
                throw new DataApiException(nameof(_context.Forms));

            if (!string.IsNullOrEmpty(form.Content))
                updateForm.Content = form.Content;

            _context.Forms.Update(updateForm);
        }
    }
}
