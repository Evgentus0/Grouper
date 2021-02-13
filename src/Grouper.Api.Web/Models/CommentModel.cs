using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grouper.Api.Web.Models
{
    public class CommentModel
    {
        public string Text { get; set; }
        public PostModel Post { get; set; }
        public UserModel Sender { get; set; }
    }
}
