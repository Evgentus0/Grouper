using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grouper.Api.Web.Models
{
    public class CommentModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public UserModel Sender { get; set; }
        public int PostId { get; set; }
    }
}
