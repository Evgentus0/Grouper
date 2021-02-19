using Grouper.Api.Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grouper.Api.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtentions
    {
        public static IApplicationBuilder InitDbIfNotExist(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<GrouperDbContext>();
                DbInitializer.Initialize(context);
            }

            return app;
        }
    }
}
