using FluentAssertions;
using Grouper.Api.Data.Context;
using Grouper.Api.Data.Entities;
using Grouper.Api.Data.Repositories;
using Grouper.Api.Data.Tests.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Grouper.Api.Data.Tests.Scenarios
{
    public class GroupRepositoryTests: IDisposable
    {
        private GrouperDbContext _context;
        private GroupRepository _repository;

        public GroupRepositoryTests()
        {
            _context = TestDbContextFactory.GetTestDbContext();
            _repository = new GroupRepository(_context);
        }
        
        [Fact]
        public void AddUserToGroup_ValidParameter_Success()
        {
            //Arrange
            int groupId = 1;
            string userId = "userId";

            var userGroup = new UserGroup { UserId = userId, GroupId = groupId};

            //Act
            _repository.AddUserToGroup(groupId, userId).Wait();
            _context.SaveChanges();

            //Assert
            _context.UsersGroups
                .First(x => x.UserId == userId && x.GroupId == groupId)
                .Should().Be(userGroup);
        }

        [Fact]
        public void Create_ValidParameters_Success()
        {
            //Arrange
            var groupdId = 1;
            var excpectedGroup = new Group { Id = groupdId };

            //Act
            _repository.Create(excpectedGroup).Wait();
            _context.SaveChanges();

            //Assert
            _context.Groups.Where(x => x.Id == groupdId).Count().Should().Be(1);
        }

        [Fact]
        public void Delete_ValidParamameter_Success()
        {
            //Arrange
            var groupId = 1;
            var group = new Group { Id = groupId };

            _context.Groups.Add(group);
            _context.SaveChanges();

            //Act
            _repository.Delete(groupId).Wait();
            _context.SaveChanges();

            //Assert
            _context.Groups.Count().Should().Be(0);
        }

        [Fact]
        public void GetById_ValidParameter_Success()
        {
            //Arrange
            var groupdId = 1;
            var groupName = "name";
            var group = new Group { Id = groupdId, Name = groupName };

            _context.Groups.Add(group);
            _context.SaveChanges();

            //Act
            var actualGroup = _repository.GetById(groupdId).Result.group;

            //Assert
            actualGroup.Should().Be(group);
        }

        [Fact]
        public void GetByIdentificator_ValidParameter_Success()
        {
            //Arrange
            var groupId = 1;
            var groupIdentificator = "identificator";
            var groupName = "name";

            var group = new Group { Id = groupId, Identificator = groupIdentificator, Name = groupName };
            _context.Groups.Add(group);
            var count = _context.SaveChanges();

            //Act
            var actualGroup = _repository.GetByIdentificator(groupIdentificator).Result.group;

            //Assert
            actualGroup.Should().Be(group);
            
        }

        [Fact]
        public void GetByUserId_ValidParameter_Success()
        {
            //Arrange
            var groupId1 = 1;
            var groupId2 = 2;
            var userId = "userId";

            var group1 = new Group { Id = groupId1 };
            var group2 = new Group { Id = groupId2 };
            var excpectedGroupList = new List<Group> { group1, group2 };

            _context.Groups.AddRange(excpectedGroupList);
            _context.SaveChanges();

            _context.Users.Add(new ApplicationUser { Id = userId });

            _context.UsersGroups.Add(new UserGroup { GroupId = groupId1, UserId = userId });
            _context.UsersGroups.Add(new UserGroup { GroupId = groupId2, UserId = userId });
            _context.SaveChanges();

            //Act
            var actualGroups = _repository.GetByUserId(userId).Result;

            //Assert
            actualGroups.Select(x => x.Id).Should().Equal(excpectedGroupList.Select(x => x.Id));
        }

        [Fact]
        public void Update_ValidParameter_Success()
        {
            //Arrange
            var groupId = 1;
            var excpectedName = "new name";

            _context.Groups.Add(new Group { Id = groupId });
            _context.SaveChanges();

            //Act
            var newGroup = new Group { Id = groupId, Name = excpectedName };
            _repository.Update(newGroup).Wait();
            _context.SaveChanges();

            //Assert
            var actualGroup = _context.Groups.First(x => x.Id == groupId);

            actualGroup.Should().Be(newGroup);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
