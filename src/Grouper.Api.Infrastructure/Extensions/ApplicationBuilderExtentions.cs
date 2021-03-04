using Grouper.Api.Data.Context;
using Grouper.Api.Data.Interfaces;
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
        public static IApplicationBuilder CreateAndInitIfNotExist(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<GrouperDbContext>();
                var unitOfWork = serviceScope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                DbInitializer.Initialize(context, unitOfWork);
            }

            return app;
        }
    }
}
