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

            if (post is not null)
            {
                _context.Posts.Remove(post);
            }
        }

        public async Task<List<Post>> GetByGroupId(int groupId)
        {
            var posts = await GetWithParentsPosts(groupId);
            return posts.ToList();
        }

        private async Task<IEnumerable<Post>> GetWithParentsPosts(int groupId)
        {
            var posts = _context.Posts
                           .Include(x => x.Comments)
                           .ThenInclude(x => x.Sender)
                           .Include(x => x.Forms)
                           .ThenInclude(x => x.User)
                           .Include(x => x.Group)
                           .Include(x => x.AcknowledgeUsers)
                           .Where(x => x.GroupId == groupId);

            var group = await _context.Groups
                .FirstAsync(x => x.Id == groupId);

            if (group.ParentGroupId.HasValue)
            {
                posts = posts.Union(await GetWithParentsPosts(group.ParentGroupId.Value));
            }

            return posts;
        }

        public async Task<Post> GetById(int id)
        {
            return await _context.Posts
                    .Include(x => x.Comments)
                    .ThenInclude(x => x.Sender)
                    .Include(x => x.Forms)
                    .ThenInclude(x => x.User)
                    .Include(x => x.Group)
                    .Include(x => x.AcknowledgeUsers)
                    .FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task Update(Post post)
        {
            _context.Posts.Update(post);

            return Task.CompletedTask;
        }
    }
}
