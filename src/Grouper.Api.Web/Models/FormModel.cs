using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grouper.Api.Web.Models
{
    public class FormModel
    {
        public int Id { get; set; }
        public PostModel Post {get;set;}
        public FormContentModel Content {get;set;}
    }
}
