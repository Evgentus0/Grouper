using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grouper.Api.Data.Context
{
    public class DbInitializer
    {
        public static void Initialize(GrouperContext context)
        {
            var notExist = context.Database.EnsureCreated();
            var alreadyExis = !notExist;

            if (alreadyExis)
            {
                return;
            }

            // add init data
        }
    }
}
