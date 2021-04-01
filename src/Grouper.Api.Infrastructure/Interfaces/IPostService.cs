using Grouper.Api.Infrastructure.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grouper.Api.Infrastructure.Interfaces
{
    public interface IPostService
    {
        Task Create(PostDto postDto);
        Task<List<PostDto>> GetByGroupId(int groupId);
        Task<PostDto> GetById(int id);
        Task AddComment(CommentDto commentDto, string senderId);
        Task Delete(int id);
        Task Update(PostDto postDto);
        Task AcknowledgeUser(int postId, string userId);
    }
}
