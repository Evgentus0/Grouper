using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grouper.Api.Infrastructure.Settings
{
    public class AppSettings
    {
        public DbType DbType { get; set; }
        public AuthorizationSettings Authorization { get; set; }
    }
}
