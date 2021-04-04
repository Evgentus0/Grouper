using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grouper.Api.Web.Models.Outbound
{
    public class CreateResponseModel<T>
    {
        public string Message { get; set; }
        public T NewlyCreatedId { get; set; }
    }
}
