using FluentAssertions;
using Grouper.Api.Data.Context;
using Grouper.Api.Data.Entities;
using Grouper.Api.Data.Repositories;
using Grouper.Api.Data.Tests.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Grouper.Api.Data.Tests.Scenarios
{
    public class FormRepositoryTests:IDisposable
    {
        private GrouperDbContext _context;
        private FormRepository _repository;

        public FormRepositoryTests()
        {
            _context = TestDbContextFactory.GetTestDbContext();
            _repository = new FormRepository(_context);
        }

        [Fact]
        public void Create_ValidParameter_Success()
        {
            //Arrange
            var formId = 1;

            var expectedForm = new Form() { Id = formId };

            //Act
            _repository.Create(expectedForm).Wait();
            _context.SaveChanges();

            var actualForm = _context.Forms.First(x => x.Id == formId);

            //Assert
            actualForm.Should().Equals(expectedForm);
        }

        [Fact]
        public void Delete_ValidParameter_Success()
        {
            //Arrange
            int formId = 1;

            _context.Forms.Add(new Form { Id = formId });
            _context.SaveChanges();

            //Act
            _repository.Delete(formId).Wait();
            _context.SaveChanges();

            //Assert
            _context.Forms.Count().Should().Be(0);
        }

        [Fact]
        public void GetById_ValidParameter_Success()
        {
            //Arrange
            int formId = 1;

            var excpectedForm = new Form { Id = formId };
            _context.Forms.Add(excpectedForm);
            _context.SaveChanges();

            //Act
            var actualForm = _repository.GetById(formId).Result;

            //Assert
            actualForm.Should().Be(excpectedForm);
        }

        [Fact]
        public void GetByUserId_ValidParameter_Success()
        {
            //Arrange
            int formId = 1;
            var userId = "userId";

            var excpectedForm = new Form { Id = formId };
            var user = new ApplicationUser { Id = userId, Forms = new List<Form> { excpectedForm } };
            _context.Users.Add(user);
            _context.SaveChanges();

            //Act
            var form = _repository.GetByUserId(userId).Result;

            //Assert
            var actualForm = _context.Users.Include(x => x.Forms).First(x => x.Id == userId).Forms.First(x => x.Id == formId);
            actualForm.Should().Be(excpectedForm);
        }

        [Fact]
        public void Update_ValidParameter_Success()
        {
            //Arrange
            int formId = 1;
            var excpectedContet = "Content";

            var form = new Form { Id = formId };
            _context.Forms.Add(form);
            _context.SaveChanges();

            //Act
            var newForm = new Form { Id = formId, Content = excpectedContet };
            _repository.Update(newForm).Wait();
            _context.SaveChanges();

            //Assert
            var actualForm = _context.Forms.First(x => x.Id == formId);
            actualForm.Should().Be(newForm);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
