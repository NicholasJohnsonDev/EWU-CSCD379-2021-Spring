using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SecretSanta.Web.Api;
using SecretSanta.Web.Controllers;

namespace SecretSanta.Web.Tests.Api
{
    public class TestableUsersClient : IUsersClient
    {
        public Task DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public List<UpdateUser> GetAllUsersReturnValue { get; set; } = new();
        public int GetAllAsyncInvocationCount { get; set; }
        public Task<ICollection<UpdateUser>?> GetAllAsync()
        {
            GetAllAsyncInvocationCount++;
            return Task.FromResult<ICollection<User>?>(GetAllUsersReturnValue);
        }
        public Task<ICollection<UpdateUser>> GetAllAsync(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<UpdateUser> GetAsync(int id)
        {
            throw new System.NotImplementedException();
        }
        public Task<UpdateUser> GetAsync(int id, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public int PostAsyncInvocationCount { get; set; }
        public List<UpdateUser> PostAsyncInvokedParameters { get; } = new();
        public Task<UpdateUser> PostAsync(UpdateUser myUser)
        {
            PostAsyncInvocationCount++;
            PostAsyncInvokedParameters.Add(myUser);
            return Task.FromResult(myUser);
        }

        public Task<User> PostAsync(UpdateUser user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }


        public Task<FileResponse> PutAsync(int id, UpdateUser user)
        {
            throw new System.NotImplementedException();
        }

        public Task<FileResponse> PutAsync(int id, UpdateUser user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }


    }
}