using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grouper.Api.Web.Models
{
    public class PostModel
    {
        public int Id { get; set; }
        public string Caption { get; set; }
        public string Description { get; set; }
        public List<CommentModel> Comments { get; set; }
        public List<FormModel> Forms { get; set; }
        public int GroupId { get; set; }

        public PostModel()
        {
            Comments = new List<CommentModel>();
            Forms = new List<FormModel>();
        }
    }
}
