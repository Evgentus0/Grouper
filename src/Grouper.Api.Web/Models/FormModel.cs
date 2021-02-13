using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grouper.Api.Web.Models
{
    public class FormModel
    {
        public int Id { get; set; }
        public PostModel Pos {get;set;}
        public FromContentModel Content {get;set;}
        public List<UserModel> Users {get;set;}
    }
}
