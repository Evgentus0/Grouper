using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grouper.Api.Infrastructure.DTOs
{
    public class GroupDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Identificator { get; set; }
        public List<GroupDto> ChildGroups { get; set; }
        public List<UserDto> Participants { get; set; }
        public List<PostDto> Tasks { get; set; }
        public string Description { get; set; }
        public string UsefulContent { get; set; }
        public int? ParentGroupId { get; set; }
        public string CreatorId { get; set; }
        public UserDto Creator { get; set; }


        public GroupDto()
        {
            ChildGroups = new List<GroupDto>();
            Participants = new List<UserDto>();
            Tasks = new List<PostDto>();
        }
    }
}
