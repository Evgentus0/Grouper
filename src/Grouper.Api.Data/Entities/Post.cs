using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grouper.Api.Data.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public string Caption { get; set; }
        public string Description { get; set; }
        public List<Comment> Comments { get; set; }
        public int GroupId { get; set; }
        [ForeignKey("GroupId")]
        public Group Group { get; set; }
        public List<Form> Forms { get; set; }

        public Post()
        {
            Comments = new List<Comment>();
            Forms = new List<Form>();
        }
    }
}
