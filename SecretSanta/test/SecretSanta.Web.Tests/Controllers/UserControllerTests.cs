using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc.Testing;
using SecretSanta.Web.Api;
using SecretSanta.Web.Tests.Api;
using SecretSanta.Web.ViewModels;

namespace SecretSanta.Web.Tests
{
    [TestClass]
    public class UsersControllersTests

    {
        private WebApplicationFactory Factory { get; } = new();
        private TestableUsersClient Client { get; } = new();

        [TestMethod]
        public async Task Index_WithUsers_InvokesGetAllAsync()
        {
            // Arrange
            User user1 = new()
            {
                Id = 1,
                FirstName = "FirstName1",
                LastName = "LastName1"
            };

            User user2 = new()
            {
                Id = 2,
                FirstName = "FirstName2",
                LastName = "LastName2"
            };

            TestableUsersClient usersClient = Factory.Client;
            usersClient.GetAllUsersReturnValue = new List<User>() { user1, user2 };
            HttpClient client = Factory.CreateClient();

            // Act
            HttpResponseMessage response = await client.GetAsync("/Users/");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(1, usersClient.GetAllAsyncInvocationCount);
        }


        [TestMethod]
        public async Task Create_WithValidModel_InvokePostAsync()
        {
            // Arrange
            HttpClient client = Factory.CreateClient();
            TestableUsersClient usersClient = Factory.Client;


            Dictionary<int, int?> values = new()
            {
                { nameof(UserViewModel.Id), 0 }
            };

            FormUrlEncodedContent content = new(values!);

            // Act
            HttpResponseMessage response = await client.PostAsync("/Users/Create", content);
            // Assert
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(0, usersClient.PostAsyncInvocationCount);
            Assert.AreEqual(0, usersClient.PostAsyncInvokedParameters.Count);
            Assert.AreEqual(newUser.FirstName, usersClient.PostAsyncInvokedParameters[0].FirstName);
            Assert.AreEqual(newUser.LastName, usersClient.PostAsyncInvokedParameters[0].LastName);
        }

    }
}