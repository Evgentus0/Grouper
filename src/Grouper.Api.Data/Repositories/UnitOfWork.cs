using Grouper.Api.Data.Context;
using Grouper.Api.Data.Entities;
using Grouper.Api.Data.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grouper.Api.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool _disposed = false;

        private readonly GrouperDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IFormRepository _formRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IPostRepository _postRepository;

        public UnitOfWork(GrouperDbContext context, 
            UserManager<ApplicationUser> userManager,
            IFormRepository formRepository,
            IGroupRepository groupRepository,
            IPostRepository postRepository)
        {
            _context = context;
            _userManager = userManager;
            _formRepository = formRepository;
            _groupRepository = groupRepository;
            _postRepository = postRepository;
        }

        public IFormRepository FormRepository => _formRepository;

        public IGroupRepository GroupRepository => _groupRepository;

        public IPostRepository PostRepository => _postRepository;

        public UserManager<ApplicationUser> UserManager => _userManager;

        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
