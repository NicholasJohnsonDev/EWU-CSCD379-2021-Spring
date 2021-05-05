using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Threading.Tasks;
using SecretSanta.Web.Api;
using SecretSanta.Web.Tests.Api;
using System.Net.Http;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Linq;
using SecretSanta.Web.ViewModels;


namespace SecretSanta.Web.Tests
{
    [TestClass]
    public class UsersControllerTests
    {
        private WebApplicationFactory Factory { get; } = new();
        private TestableUsersClient Client { get; } = new();

        [TestMethod]
        [DataRow(0, "First0", "Last0")]
        public async Task Index_WithEvents_InvokesGetAllAsync(int id, string firstName, string lastName)
        {
            // Arrange
            UpdateUser user = new() { Id = id, FirstName = firstName, LastName = lastName };
            List<UpdateUser> usersList = new() { user };

            // Act
            TestableUsersClient usersClient = Factory.Client;
            HttpClient client = Factory.CreateClient();
            usersClient.GetAllUpdateUsersReturnValue = usersList;

            HttpResponseMessage response = await client.GetAsync("/Users/");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(1, usersClient.GetAllAsyncInvocationCount);
        }

        [TestMethod]
        [DataRow(0, "First0", "Last0")]
        public async Task CreatePost_WithValidModel_InvokesPostAsync(int id, string firstName, string lastName)
        {
            // Arrange
            TestableUsersClient usersClient = Factory.Client;
            HttpClient client = Factory.CreateClient();

            Dictionary<string, string?> values = new()
            {
                { nameof(UserViewModel.FirstName), firstName }
            };
            FormUrlEncodedContent content = new(values!);

            // Act
            HttpResponseMessage response = await client.PostAsync("/Users/Create", content);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.AreEqual<int>(1, usersClient.PostAsyncInvocationCounter);
            Assert.IsTrue(usersClient.PostAsyncInvocationParameters.Select(item => item.FirstName).Contains(firstName));
        }

        [TestMethod]
        [DataRow(0, "First0", "Last0")]
        public async Task EditPost_WithValidModelState_InvokesPutAsync(int id, string firstName, string lastName)
        {
            // Arrange
            TestableUsersClient usersClient = Factory.Client;
            HttpClient client = Factory.CreateClient();

            usersClient.PutAsyncInvocationParameters.Add(new UpdateUser()
            { FirstName = firstName, Id = id });

            Dictionary<string, string?> values = new()
            {
                { nameof(UserViewModel.FirstName), firstName },
                { nameof(UserViewModel.Id), id.ToString() },
            };
            FormUrlEncodedContent content = new(values!);

            // Act
            HttpResponseMessage response = await client.PostAsync("/Users/Edit", content);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.AreEqual<int>(1, usersClient.PutAsyncInvocationCounter);
            Assert.IsTrue(usersClient.PutAsyncInvocationParameters.Select(item => item.FirstName).Contains("First0"));
        }

        [TestMethod]
        [DataRow(0, "First0", "Last0")]
        public async Task EditGet_WithValidId_InvokesGetAsync(int id, string firstName, string lastName)
        {
            // Arrange
            UpdateUser user = new() { Id = id, FirstName = firstName, LastName = lastName };

            TestableUsersClient usersClient = Factory.Client;
            HttpClient client = Factory.CreateClient();

            usersClient.GetAsyncReturnValue = user;

            // Act
            HttpResponseMessage response = await client.GetAsync("/Users/Edit/0");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.AreEqual<int>(1, usersClient.GetAsyncInvocationCounter);
            Assert.AreEqual<string>("First0", usersClient.GetAsyncReturnValue.FirstName);
        }

        [TestMethod]
        [DataRow(1, "First1", "Last1")]
        public async Task Delete_GivenPositiveId_InvokesDelete(int id, string firstName, string lastName)
        {
            // Arrange
            TestableUsersClient usersClient = Factory.Client;
            HttpClient client = Factory.CreateClient();

            List<UpdateUser> usersList = new()
            {
                new UpdateUser { Id = 0, FirstName = "firstname0", LastName = "lastname0" },
                new UpdateUser { Id = 1, FirstName = "firstname1", LastName = "lastname1" },
                new UpdateUser { Id = 2, FirstName = "firstname2", LastName = "lastname2" },
            };

            usersClient.DeleteAsyncUsersList = usersList;

            // Act
            HttpResponseMessage response = await client.PostAsync("/Users/Delete/1", new StringContent("{}"));

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.AreEqual<int>(1, usersClient.DeleteAsyncInvocationCount);
            Assert.IsFalse(usersClient.DeleteAsyncUsersList.Select(item => item.FirstName).Contains("firstname1"));
        }
    }
}