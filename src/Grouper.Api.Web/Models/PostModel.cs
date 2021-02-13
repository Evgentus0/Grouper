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
        public GroupModel Group { get; set; }
        public List<FormModel> Forms { get; set; }
    }
}
