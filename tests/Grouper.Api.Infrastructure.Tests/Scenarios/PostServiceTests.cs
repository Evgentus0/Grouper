using AutoMapper;
using FluentAssertions;
using Grouper.Api.Data.Entities;
using Grouper.Api.Data.Interfaces;
using Grouper.Api.Infrastructure.Automapper;
using Grouper.Api.Infrastructure.Core;
using Grouper.Api.Infrastructure.DTOs;
using Grouper.Api.Infrastructure.Interfaces;
using Grouper.Api.Infrastructure.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Grouper.Api.Infrastructure.Tests.Scenarios
{
    public class PostServiceTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private IMapper _mapper;
        private IExecutionStrategy _executionStrategy;

        private IPostService _service;
        public PostServiceTests()
        {
            if (_mapper is null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new EntiyDtoMapperProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }

            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _executionStrategy = new ExecutionStrategy(new Settings.AppSettings());

            _service = new PostService(_unitOfWorkMock.Object, _mapper, _executionStrategy);
        }

        [Fact]
        public void Create_ValidParameter_Success()
        {
            //Arrange
            var postId = 1;
            var postDto = new PostDto { Id = postId };
            var post = _mapper.Map<Post>(postDto);

            var isCalled = false;
            _unitOfWorkMock.Setup(x => x.PostRepository.Create(It.Is<Post>(x => x.Id == postId)))
                .Callback(() => isCalled = true);

            //Act
            _service.Create(postDto);

            //Assert
            isCalled.Should().Be(true);
            _unitOfWorkMock.Verify(x => x.SaveAsync(), Times.Once());
        }

        [Fact]
        public void GetByGroupId_ValidParameter_Success()
        {
            var groupId = 1;
            var postId1 = 1;
            var postId2 = 2;
            var postList = new List<Post>
                {
                    new Post{Id = postId1},
                    new Post{Id = postId2}
                };

            var excpectedPosts = _mapper.Map<List<PostDto>>(postList).Select(x => x.Id);

            _unitOfWorkMock.Setup(x => x.PostRepository.GetByGroupId(groupId))
                .Returns<int>(x => Task.FromResult(postList));

            //Act
            var actual = _service.GetByGroupId(groupId).Result.Select(x => x.Id);

            //Assert
            actual.Should().Equal(excpectedPosts);
        }
    }
}
