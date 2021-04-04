using Grouper.Api.Data.Entities;
using Grouper.Api.Data.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grouper.Api.Data.Context
{
    public class DbInitializer
    {
        public static void Initialize(GrouperDbContext context, IUnitOfWork unitOfWork)
        {
            var alreadyExis = !context.Database.EnsureCreated();

            if (alreadyExis)
            {
                return;
            }

            #region Roles
            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "student",
                    NormalizedName = "Student"
                },
                new IdentityRole
                {
                    Name = "teacher",
                    NormalizedName = "Teacher"
                },
                new IdentityRole
                {
                    Name = "admin",
                    NormalizedName = "Administrator"
                }
            };

            roles.ForEach(role =>
            {
                unitOfWork.RoleManager.CreateAsync(role).Wait();
            });
            unitOfWork.Save();
            #endregion


            #region Users
            var users = new List<ApplicationUser>
            {
                new ApplicationUser
                {
                    FirstName = "TestFirstNameStudent",
                    LastName = "TestLastNameStudent",
                    Email = "testStudent@test.com",
                    UserName = "testStudent@test.com"

                },
                new ApplicationUser
                {
                    FirstName = "TestFirstNameTeacher",
                    LastName = "TestLastNameTeacher",
                    Email = "testTeacher@test.com",
                    UserName = "testTeacher@test.com"
                },
                new ApplicationUser
                {
                    FirstName = "TestFirstNameAdmin",
                    LastName = "TestLastNameAdmin",
                    Email = "testAdmin@test.com",
                    UserName = "testAdmin@test.com"
                },
            };

            users.ForEach(user =>
            {
                unitOfWork.UserManager.CreateAsync(user, "Password1").Wait();
            });
            unitOfWork.Save();

            var studentUser = unitOfWork.UserManager.FindByEmailAsync("testStudent@test.com").Result;
            var teacherUser = unitOfWork.UserManager.FindByEmailAsync("testTeacher@test.com").Result;
            var adminUser = unitOfWork.UserManager.FindByEmailAsync("testAdmin@test.com").Result;

            unitOfWork.UserManager.AddToRoleAsync(studentUser, "student").Wait();
            unitOfWork.UserManager.AddToRoleAsync(teacherUser, "teacher").Wait();
            unitOfWork.UserManager.AddToRoleAsync(adminUser, "admin").Wait();

            unitOfWork.Save();
            #endregion


            #region Groups
            var group = new Group
            {
                Name = "TestParentGroup",
                Identificator = Guid.NewGuid().ToString(),
                CreatorId = unitOfWork.UserManager.FindByEmailAsync("testTeacher@test.com").Result.Id
            };
            context.Groups.Add(group);
            context.SaveChanges();

            var childGroup = new Group
            {
                Name = "TestChildGroup",
                ParentGroupId = context.Groups.First(x => x.Name == "TestParentGroup").Id,
                Identificator = Guid.NewGuid().ToString(),
                CreatorId = unitOfWork.UserManager.FindByEmailAsync("testTeacher@test.com").Result.Id
            };
            context.Groups.Add(childGroup);
            context.SaveChanges();

            var userGroup1 = new UserGroup
            {
                GroupId = context.Groups.First(x => x.Name == "TestChildGroup").Id,
                UserId = unitOfWork.UserManager.FindByEmailAsync("testStudent@test.com").Result.Id
            };

            var userGroup2 = new UserGroup
            {
                GroupId = context.Groups.First(x => x.Name == "TestChildGroup").Id,
                UserId = unitOfWork.UserManager.FindByEmailAsync("testTeacher@test.com").Result.Id
            };

            var userGroup3 = new UserGroup
            {
                GroupId = context.Groups.First(x => x.Name == "TestParentGroup").Id,
                UserId = unitOfWork.UserManager.FindByEmailAsync("testTeacher@test.com").Result.Id
            };
            context.AddRange(userGroup1, userGroup2, userGroup3);
            context.SaveChanges();
            #endregion


            #region Posts
            var post = new Post
            {
                Caption = "TestPost",
                Description = "TestPostDescription",
                GroupId = context.Groups.First(x => x.Name == "TestChildGroup").Id
            };
            context.Posts.Add(post);
            context.SaveChanges();

            var comment = new Comment
            {
                Text = "TestComment",
                PostId = context.Posts.First(x => x.Caption == "TestPost").Id,
                UserId = unitOfWork.UserManager.FindByEmailAsync("testTeacher@test.com").Result.Id
            };
            context.Comments.Add(comment);
            context.SaveChanges();

            var form = new Form
            {
                Content = "TestContent",
                PostId = context.Posts.First(x => x.Caption == "TestPost").Id,
                UserId = unitOfWork.UserManager.FindByEmailAsync("testStudent@test.com").Result.Id
            };
            context.Forms.Add(form);
            context.SaveChanges();
            #endregion
        }
    }
}
