﻿using System;
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
        public string Identificator { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? ParentGroupId { get; set; }
        [ForeignKey("ParentGroupId")]
        public Group ParentGroup { get; set; }
        public List<Post> Tasks { get; set; }
        public string UsefulContent { get; set; }
        public List<Group> ChildGroups { get; set; }
        public string CreatorId { get; set; }
        [ForeignKey("CreatorId")]
        public ApplicationUser Creator { get; set; }

        public Group()
        {
            Tasks = new List<Post>();
            ChildGroups = new List<Group>();
        }

        public override bool Equals(object obj)
        {
            if(obj is Group group)
            {
                return Id == group.Id
                    && Identificator == group.Identificator
                    && Name == group.Name
                    && Description == group.Description
                    && ParentGroupId == group.ParentGroupId
                    && UsefulContent == group.UsefulContent;
            }

            return false;
        }
    }
}
