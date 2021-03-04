using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grouper.Api.Web.Models
{
    public class UserModel
    {
        public string Id {get; set;}
        public string FirstName {get; set;}
        public string LastName { get; set; }
        public string Email {get; set;}
        public string Password {get; set;}
        public string Role {get; set;}
    }
}
