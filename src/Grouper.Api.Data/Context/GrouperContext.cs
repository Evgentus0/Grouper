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
    public class GrouperContext: IdentityDbContext<ApplicationUser>
    {
        public GrouperContext(DbContextOptions<GrouperContext> options) : base(options)
        { }

        public DbSet<Comment> Comments{ get; set; }
        public DbSet<Form> Forms { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<UserForm> UsersForms { get; set; }
        public DbSet<UserGroup> UsersGroups { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserForm>()
                .ToTable("UserForm")
                .HasKey(o => new { o.UserId, o.FormId});

            builder.Entity<UserGroup>()
                .ToTable("UserGroup")
                .HasKey(o => new { o.UserId, o.GroupId });

            builder.Entity<Group>()
                .HasOne(x => x.ParentGroup)
                .WithMany(x => x.ChildGroups)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
