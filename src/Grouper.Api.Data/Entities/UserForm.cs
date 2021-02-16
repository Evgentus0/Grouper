using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grouper.Api.Data.Entities
{
    public class UserForm
    {
        public string UserId { get; set; }
        public int FormId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        [ForeignKey("FormId")]
        public Form Form { get; set; }
        public bool IsDone { get; set; }
    }
}
