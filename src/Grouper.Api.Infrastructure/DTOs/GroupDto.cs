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
        public List<GroupDto> ChildGroups { get; set; }
        public List<UserDto> Participants { get; set; }
        public List<PostDto> Tasks { get; set; }
        public List<string> UsefulLinks { get; set; }

        public GroupDto()
        {
            ChildGroups = new List<GroupDto>();
            Participants = new List<UserDto>();
            Tasks = new List<PostDto>();
            UsefulLinks = new List<string>();
        }
    }
}
