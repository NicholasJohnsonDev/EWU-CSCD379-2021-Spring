using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using SecretSanta.Api.Controllers;
using System.Collections.Generic;
using SecretSanta.Business;
using SecretSanta.Data;
using Microsoft.AspNetCore.Mvc;
using System;

namespace SecretSanta.Business.Tests
{
    [TestClass]
    public class UserManagerTests
    {
        [TestMethod]
        [DataRow(42, "TestFirstName", "TestLastName")]
        public void Create_WithUserData_ReturnsUserData(int id, String first, String last)
        {
            // Arrange 
            UserManager manager = new();
            User myUser = new User() { Id = id, FirstName = first, LastName = last };

            // Act
            User createdUser = manager.Create(myUser);

            // Assert
            Assert.AreEqual(createdUser.Id, myUser.Id);
            Assert.AreEqual(createdUser.FirstName, myUser.FirstName);
            Assert.AreEqual(createdUser.LastName, myUser.LastName);
        }

        [TestMethod]
        [DataRow(42)]
        public void GetItem_WithId_ReturnsCorrectUser(int id)
        {
            // Arrange 
            UserManager manager = new();

            // Act
            User user = manager.GetItem(id);

            // Assert
            Assert.AreEqual(user.Id, id);
        }

        [TestMethod]
        public void List_WithDataInList_ReturnsDataInList()
        {
            // Arrange 
            UserManager manager = new();
            ICollection<User> expectedData = DeleteMe.Users;

            // Act
            ICollection<User> listData = manager.List();

            // Assert
            Assert.IsTrue(listData == expectedData);
        }

        [TestMethod]
        [DataRow(1)]
        public void Remove_WithId_ReturnsTrue(int id)
        {
            // Arrange 
            UserManager manager = new();

            // Act // Assert
            Assert.IsTrue(manager.Remove(id));
        }

        [TestMethod]
        [DataRow(-1)]
        [DataRow(99999)]
        public void Remove_OutOfRange_ReturnsFalse(int id)
        {
            // Arrange 
            UserManager manager = new();

            // Act // Assert
            Assert.IsFalse(manager.Remove(id));
        }

        [TestMethod]
        [DataRow(1, "TestFirstName", "TestLastName")]
        public void Save_UserData_UpdatesUser(int id, String first, String last)
        {
            // Arrange
            UserManager manager = new();
            User user = new User() { Id = id, FirstName = first, LastName = last };

            // Act
            manager.Save(user);

            // Assert
            Assert.AreEqual(manager.GetItem(1).FirstName, user.FirstName);
            Assert.AreEqual(manager.GetItem(1).LastName, user.LastName);
        }
    }
}