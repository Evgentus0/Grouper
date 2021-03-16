using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Grouper.Api.Data.Entities
{
    public class ApplicationUser: IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Form> Forms { get; set; }
        public List<Post> CheckedPosts { get; set; }
    }
}
