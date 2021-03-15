﻿using AutoMapper;
using Grouper.Api.Data.Entities;
using Grouper.Api.Data.Interfaces;
using Grouper.Api.Infrastructure.DTOs;
using Grouper.Api.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grouper.Api.Infrastructure.Services
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _dataBase;
        private readonly IMapper _mapper;
        private readonly IExecutionStrategy _strategy;

        public PostService(IUnitOfWork dataBase, IMapper mapper, IExecutionStrategy strategy)
        {
            _dataBase = dataBase;
            _mapper = mapper;
            _strategy = strategy;
        }
        public async Task AddComment(CommentDto commentDto)
        {
            await _strategy.ExecuteAsync(async () =>
            {
                var comment = _mapper.Map<Comment>(commentDto);

                await _dataBase.PostRepository.AddComment(comment);
            });
        }

        public async Task Create(PostDto postDto)
        {
            await _strategy.ExecuteAsync(async () =>
            {
                var post = _mapper.Map<Post>(postDto);

                await _dataBase.PostRepository.Create(post);
            });
        }
        
        public async Task Delete(int id)
        {
            await _strategy.ExecuteAsync(async () =>
            {
                await _dataBase.PostRepository.Delete(id);
            });
        }

        public async Task<List<PostDto>> GetByGroupId(int groupId)
        {
            return await _strategy.ExecuteAsync(async () =>
            {
                List<Post> posts = await _dataBase.PostRepository.GetByGroupId(groupId);

                var postDtos = _mapper.Map<List<PostDto>>(posts);

                return postDtos;
            });
        }

        public async Task<PostDto> GetById(int id)
        {
            return await _strategy.ExecuteAsync(async () =>
            {
                Post post = await _dataBase.PostRepository.GetById(id);

                var postDto = _mapper.Map<PostDto>(post);

                return postDto;
            });
        }

        public async Task Update(PostDto postDto)
        {
            await _strategy.ExecuteAsync(async () =>
            {
                var post = _mapper.Map<Post>(postDto);

                await _dataBase.PostRepository.Update(post);
            });
        }
    }
}
