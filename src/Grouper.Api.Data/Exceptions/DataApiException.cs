using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grouper.Api.Data.Exceptions
{
    public class DataApiException:Exception
    {
        public string DbSetName { get; set; }

        public DataApiException(string dbSetName, 
            string message = "Database error", 
            Exception innerExecption = null): base(message, innerExecption)
        {
            DbSetName = dbSetName;
        }
    }
}
