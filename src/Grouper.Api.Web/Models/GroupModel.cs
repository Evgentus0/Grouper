using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grouper.Api.Web.Models
{
    public class GroupModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Identificator { get; set; }
        public List<GroupModel> ChildGroups { get; set; }
        public List<UserModel> Participants { get; set; }
        public List<PostModel> Tasks { get; set; }
        public string Description { get; set; }
        public string UsefulContent { get; set; }
        public int? ParentGroupId { get; set; }

        public GroupModel()
        {
            ChildGroups = new List<GroupModel>();
            Participants = new List<UserModel>();
            Tasks = new List<PostModel>();
        }
    }
}
