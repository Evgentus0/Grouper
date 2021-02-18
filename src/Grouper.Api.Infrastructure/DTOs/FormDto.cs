using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grouper.Api.Infrastructure.DTOs
{
    public class FormDto
    {
        public int Id { get; set; }
        public PostDto Post { get; set; }
        public FormContentDto Content { get; set; }
    }
}
