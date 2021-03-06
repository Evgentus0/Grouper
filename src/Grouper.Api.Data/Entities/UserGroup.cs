﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grouper.Api.Data.Entities
{
    public class UserGroup
    {
        public string UserId { get; set; }
        public int GroupId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        [ForeignKey("GroupId")]
        public Group Group { get; set; }

        public override bool Equals(object obj)
        {
            if(obj is UserGroup userGroup)
            {
                return UserId == userGroup.UserId
                    && GroupId == userGroup.GroupId;
            }

            return false;
        }
    }
}
