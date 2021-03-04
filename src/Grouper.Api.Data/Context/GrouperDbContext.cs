using Grouper.Api.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grouper.Api.Data.Context
{
    public class GrouperDbContext: IdentityDbContext<ApplicationUser>
    {
        public GrouperDbContext(DbContextOptions<GrouperDbContext> options) : base(options)
        { }

        public DbSet<Comment> Comments{ get; set; }
        public DbSet<Form> Forms { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<UserGroup> UsersGroups { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserGroup>()
                .ToTable("UserGroup")
                .HasKey(o => new { o.UserId, o.GroupId });
        }
    }
}
