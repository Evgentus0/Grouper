using Grouper.Api.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grouper.Api.Data.Interfaces
{
    public interface IPostRepository
    {
        Task<Post> GetById(int id);
        Task AddComment(Comment comment);
        Task<Post> Create(Post post);
        Task Delete(int id);
        Task<List<Post>> GetByGroupId(int groupId);
        Task Update(Post post);
    }
}
