﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grouper.Api.Web.Models
{
    public class UserModel
    {
        public int Id {get; set;}
        public string Name {get; set;}
        public string Login {get; set;}
        public string Email {get; set;}
        public string Password {get; set;}
        public string Role {get; set;}
        public List<GroupModel> Groups {get; set;}
        public List<FormModel> Forms { get; set; }
    }
}
