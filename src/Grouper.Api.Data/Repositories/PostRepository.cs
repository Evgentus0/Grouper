﻿using Grouper.Api.Data.Context;
using Grouper.Api.Data.Entities;
using Grouper.Api.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grouper.Api.Data.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly GrouperDbContext _context;

        public PostRepository(GrouperDbContext context)
        {
            _context = context;
        }

        public async Task AddComment(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
        }

        public async Task Create(Post post)
        {
            await _context.Posts.AddAsync(post);
        }

        public async Task Delete(int id)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(x => x.Id == id);

            if (post != null)
            {
                _context.Posts.Remove(post);
            }
        }

        public async Task<List<Post>> GetByGroupId(int groupId)
        {
            var group = await _context.Groups
                .Include(x => x.Tasks)
                .FirstOrDefaultAsync(x => x.Id == groupId);

            return group.Tasks;
        }

        public async Task<Post> GetById(int id)
        {
            return await _context.Posts.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task Update(Post post)
        {
            _context.Posts.Update(post);

            return Task.CompletedTask;
        }
    }
}
