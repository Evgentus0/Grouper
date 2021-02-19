using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Grouper.Api.Infrastructure.Exceptions
{
    public class ApiException: Exception
    {
        public readonly HttpStatusCode Code;
        public readonly List<string> Fields;

        public ApiException(HttpStatusCode statusCode) : base(statusCode.ToString())
        {
            Code = statusCode;
        }

        public ApiException(HttpStatusCode statusCode, string message) : base(message)
        {
            Code = statusCode;
        }

        public ApiException(HttpStatusCode statusCode, Exception exception) : base(exception.Message, exception)
        {
            Code = statusCode;
        }

        public ApiException(HttpStatusCode statusCode, string message, Exception exception) : base(message, exception)
        {
            Code = statusCode;
        }

        public ApiException(HttpStatusCode statusCode, string message, List<string> fields) : base(message)
        {
            Code = statusCode;
            Fields = fields;
        }

        public ApiException(HttpStatusCode statusCode, string message, Exception exception, List<string> fields) : base(message, exception)
        {
            Code = statusCode;
            Fields = fields;
        }
    }
}
