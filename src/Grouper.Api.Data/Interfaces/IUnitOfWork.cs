using Grouper.Api.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grouper.Api.Data.Interfaces
{
    public interface IUnitOfWork
    {
        IFormRepository FormRepository { get; }
        IGroupRepository GroupRepository { get; }
        IPostRepository PostRepository { get; }

        UserManager<ApplicationUser> UserManager { get; }

        void Save();
        Task SaveAsync();
    }
}
