using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SecretSanta.Web.Api;
using System.Linq;

namespace SecretSanta.Web.Tests.Api
{
    public class TestableUsersClient : IUsersClient
    {
        public List<UpdateUser> DeleteAsyncUsersList { get; set; } = new();
        public int DeleteAsyncInvocationCount { get; set; } = 0;
        public Task DeleteAsync(int id)
        {
            return Task.Run(() =>
            {
                DeleteAsyncInvocationCount++;
                DeleteAsyncUsersList.RemoveAt(id);
            });
        }

        public Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public List<UpdateUser> GetAllUpdateUsersReturnValue { get; set; } = new();
        public int GetAllAsyncInvocationCount { get; set; } = 0;
        public Task<ICollection<UpdateUser>?> GetAllAsync()
        {
            GetAllAsyncInvocationCount++;
            return Task.FromResult<ICollection<UpdateUser>?>(GetAllUpdateUsersReturnValue);
        }

        public Task<ICollection<UpdateUser>> GetAllAsync(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public UpdateUser? GetAsyncReturnValue { get; set; } = new();
        public int GetAsyncInvocationCounter { get; set; } = 0;
        public Task<UpdateUser?> GetAsync(int id)
        {
            GetAsyncInvocationCounter++;
            return Task.FromResult<UpdateUser?>(GetAsyncReturnValue);
        }

        public Task<UpdateUser> GetAsync(int id, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public List<UpdateUser> PostAsyncInvocationParameters { get; } = new();
        public int PostAsyncInvocationCounter { get; set; } = 0;
        public Task<UpdateUser> PostAsync(UpdateUser UpdateUser)
        {
            PostAsyncInvocationCounter++;
            PostAsyncInvocationParameters.Add(UpdateUser);
            return Task.FromResult(UpdateUser);
        }

        public Task<UpdateUser> PostAsync(UpdateUser UpdateUser, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public List<UpdateUser> PutAsyncInvocationParameters { get; } = new();
        public int PutAsyncInvocationCounter { get; set; } = 0;
        public Task PutAsync(int id, UpdateUser UpdateUser)
        {
            PutAsyncInvocationCounter++;
            PutAsyncInvocationParameters[id] = UpdateUser;
            return Task.FromResult(UpdateUser);
        }

        public Task PutAsync(int id, UpdateUser user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}