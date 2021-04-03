using Grouper.Api.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grouper.Api.Data.Tests.Infrastructure
{
    public class TestDbContextFactory
    {
        public static GrouperDbContext GetTestDbContext()
        {
            var options = new DbContextOptionsBuilder<GrouperDbContext>()
                .UseInMemoryDatabase("InMemoryDb").Options;

            return new GrouperDbContext(options);
        }
    }
}
