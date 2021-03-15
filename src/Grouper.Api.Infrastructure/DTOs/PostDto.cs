using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grouper.Api.Infrastructure.DTOs
{
    public class PostDto
    {
        public int Id { get; set; }
        public string Caption { get; set; }
        public string Description { get; set; }
        public List<CommentDto> Comments { get; set; }
        public List<FormDto> Forms { get; set; }
        public int GroupId { get; set; }

        public PostDto()
        {
            Comments = new List<CommentDto>();
            Forms = new List<FormDto>();
        }
    }
}
