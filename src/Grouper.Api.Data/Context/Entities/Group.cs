using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grouper.Api.Data.Entities
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ParentGroupId { get; set; }
        [ForeignKey("ParentGroupId")]
        public Group ParentGroup { get; set; }
        public List<Post> Tasks { get; set; }
        public string UsefulContent { get; set; }
        public List<Group> ChildGroups { get; set; }
    }
}
