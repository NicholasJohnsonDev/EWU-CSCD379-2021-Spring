using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Api.Controllers;
using SecretSanta.Api.Dto;
using SecretSanta.Api.Tests.Business;
using SecretSanta.Business;
using SecretSanta.Data;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class UserControllerTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_GivenNullUserRepository_ThrowsArgumentNullException()
        {
            UsersController usersController = new(null!);
        }

        [TestMethod]
        public void Get_WithData_ReturnsUsers()
        {
            //Arrange
            UsersController usersController = new(new UserRepository());

            //Act
            IEnumerable<UpdateUser> users = usersController.Get();

            //Assert
            Assert.IsTrue(users.Any());
        }

        [TestMethod]
        [DataRow(0, "First0", "Last0")]
        [DataRow(1, "First1", "Last1")]
        public void Get_WithValidId_ReturnsUser(int id, string firstName, string lastName)
        {
            //Arrange
            TestableUserRepository userRepository = new();
            UsersController controller = new(userRepository);
            User expectedUser = new()
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName
            };
            userRepository.GetItemUser = expectedUser;

            //Act
            ActionResult<UpdateUser?> result = controller.Get(id);

            //Assert
            Assert.AreEqual(id, userRepository.GetItemId);
            Assert.AreEqual(expectedUser.FirstName, result.Value.FirstName);
        }

        [TestMethod]
        [DataRow(9999, "First0", "Last0")]
        [DataRow(-9999, "First1", "Last1")]
        public void Get_WithInvalidId_ReturnsNotFound(int id, string firstName, string lastName)
        {
            //Arrange
            TestableUserRepository userRepository = new();
            UsersController usersController = new(userRepository);
            User expectedUser = new();
            userRepository.GetItemUser = expectedUser;

            //Act
            ActionResult<UpdateUser?> result = usersController.Get(id);

            //Assert
            Assert.IsTrue(result.Result is NotFoundResult);
        }

        [TestMethod]
        [DataRow(0, "First0", "Last0")]
        [DataRow(1, "First1", "Last1")]
        public void Delete_WithValidId_DeletesUser(int id, string firstName, string lastName)
        {
            //Arrange
            WebApplicationFactory factory = new();
            TestableUserRepository userRepository = factory.Manager;
            User user = new User
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName
            };
            userRepository.GetItemUser = user;
            UsersController usersController = new(userRepository);

            //Act
            ActionResult<UpdateUser?> result = usersController.Delete(id);

            //Assert
            Assert.IsTrue(result.Result is OkResult);
        }

        [TestMethod]
        [DataRow(0, "First0", "Last0")]
        [DataRow(1, "First1", "Last1")]
        public void Delete_WithInvalidId_ReturnsNotFound(int id, string firstName, string lastName)
        {
            //Arrange
            WebApplicationFactory factory = new();
            TestableUserRepository userRepository = factory.Manager;
            User user = new User
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName
            };
            userRepository.GetItemUser = user;
            UsersController usersController = new(userRepository);

            //Act
            ActionResult<UpdateUser?> result = usersController.Delete(-1);

            //Assert
            Assert.IsTrue(result.Result is NotFoundResult);
        }

        [TestMethod]
        [DataRow(0, "First0", "Last0")]
        [DataRow(1, "First1", "Last1")]
        public async Task Post_WithValidData_UpdatesUser(int id, string firstName, string lastName)
        {
            //Arrange
            WebApplicationFactory factory = new();
            TestableUserRepository userRepository = factory.Manager;
            UsersController usersController = new(userRepository);
            userRepository.UserList = new();
            UpdateUser newUser = new()
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName
            };

            HttpClient client = factory.CreateClient();

            //Act
            HttpResponseMessage response = await client.PostAsJsonAsync("/api/users", newUser);
            response.EnsureSuccessStatusCode();

            //Assert
            Assert.AreEqual(firstName, userRepository.UserList[0].FirstName);
        }

        [TestMethod]
        public async Task Post_WithInvalidData_UpdatesUser()
        {
            //Arrange
            WebApplicationFactory factory = new();
            TestableUserRepository userRepository = factory.Manager;
            UpdateUser newUser = null!;

            HttpClient client = factory.CreateClient();

            //Act
            HttpResponseMessage response = await client.PostAsJsonAsync("/api/users", newUser);

            //Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [TestMethod]
        [DataRow(0, "First0", "Last0")]
        [DataRow(1, "First1", "Last1")]
        public async Task Put_WithValidData_UpdatesUser(int id, string firstName, string lastName)
        {
            //Arrange
            WebApplicationFactory factory = new();
            TestableUserRepository userRepository = factory.Manager;
            User foundUser = new User
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName
            };
            userRepository.GetItemUser = foundUser;

            HttpClient client = factory.CreateClient();
            UpdateUser updateUser = new()
            {
                Id = id,
                FirstName = firstName + "put",
                LastName = lastName + "put"
            };

            //Act
            HttpResponseMessage response = await client.PutAsJsonAsync("/api/users/" + id, updateUser);

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(firstName + "put", userRepository.SavedUser?.FirstName);
        }

        [TestMethod]
        [DataRow(0, "First0", "Last0")]
        [DataRow(1, "First1", "Last1")]
        public async Task Put_WithValidIdInvalidData_ReturnsBadRequest(int id, string firstName, string lastName)
        {
            //Arrange
            WebApplicationFactory factory = new();
            TestableUserRepository userRepository = factory.Manager;
            User foundUser = new User
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName
            };
            userRepository.GetItemUser = foundUser;

            HttpClient client = factory.CreateClient();
            UpdateUser updateUser = null!;

            //Act
            HttpResponseMessage response = await client.PutAsJsonAsync("/api/users/" + id, updateUser);

            //Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [TestMethod]
        [DataRow(0, "First0", "Last0")]
        [DataRow(1, "First1", "Last1")]
        public async Task Put_WithInvalidIdValidData_ReturnsNotFound(int id, string firstName, string lastName)
        {
            //Arrange
            WebApplicationFactory factory = new();
            TestableUserRepository userRepository = factory.Manager;
            User foundUser = new User
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName
            };
            userRepository.GetItemUser = foundUser;

            HttpClient client = factory.CreateClient();
            UpdateUser updateUser = new()
            {
                Id = id,
                FirstName = firstName + "put",
                LastName = lastName + "put"
            };

            //Act
            HttpResponseMessage response = await client.PutAsJsonAsync("/api/users/-1", updateUser);

            //Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

    }
}