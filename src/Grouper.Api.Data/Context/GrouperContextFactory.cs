using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grouper.Api.Data.Context
{
    public class GrouperContextFactory : IDesignTimeDbContextFactory<GrouperDbContext>
    {
        public GrouperDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<GrouperDbContext>();
            optionsBuilder.UseSqlServer("Server=DESKTOP-2UQRN34\\SQLEXPRESS;Database=GrouperDb;Trusted_Connection=True;Integrated Security=SSPI;MultipleActiveResultSets=true");

            return new GrouperDbContext(optionsBuilder.Options);
        }
    }
}
