using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grouper.Api.Web.Models
{
    public class FromContentModel
    {
        public string Header {get;set;}
        public List<QuestionModel> Questions { get; set; }
    }
}
