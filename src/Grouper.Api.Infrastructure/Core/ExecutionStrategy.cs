using Grouper.Api.Data.Exceptions;
using Grouper.Api.Infrastructure.Exceptions;
using Grouper.Api.Infrastructure.Interfaces;
using Grouper.Api.Infrastructure.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grouper.Api.Infrastructure.Core
{
    public class ExecutionStrategy : IExecutionStrategy
    {
        private readonly AppSettings _settings;

        public ExecutionStrategy(AppSettings settings)
        {
            _settings = settings;
        }

        public void Execute(Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                if(ex is DataApiException dbException)
                {
                    throw new ApiException(System.Net.HttpStatusCode.InternalServerError, 
                        $"Database error in {dbException.DbSetName} Db set.", dbException);
                }
                throw;
            }
        }

        public T Execute<T>(Func<T> action)
        {
            try
            {
                return action();
            }
            catch (Exception ex)
            {

                if (ex is DataApiException dbException)
                {
                    throw new ApiException(System.Net.HttpStatusCode.InternalServerError,
                        $"Database error in {dbException.DbSetName} Db set.", dbException);
                }
                throw;
            }
        }

        public async Task ExecuteAsync(Func<Task> action)
        {
            try
            {
                await action();
            }
            catch (Exception ex)
            {

                if (ex is DataApiException dbException)
                {
                    throw new ApiException(System.Net.HttpStatusCode.InternalServerError,
                        $"Database error in {dbException.DbSetName} Db set.", dbException);
                }
                throw;
            }
        }

        public async Task<T> ExecuteAsync<T>(Func<Task<T>> action)
        {
            try
            {
                return await action();
            }
            catch (Exception ex)
            {

                if (ex is DataApiException dbException)
                {
                    throw new ApiException(System.Net.HttpStatusCode.InternalServerError,
                        $"Database error in {dbException.DbSetName} Db set.", dbException);
                }
                throw;
            }
        }
    }
}
