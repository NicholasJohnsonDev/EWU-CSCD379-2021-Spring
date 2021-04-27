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
        public void Create_WithUserData_ReturnsUserData()
        {
            UserManager manager = new();
            User myUser = new User() { Id = 42, FirstName = "TestFirstName", LastName = "TestLastName" };

            User createdUser = manager.Create(myUser);

            Assert.AreEqual(createdUser.Id, myUser.Id);
            Assert.AreEqual(createdUser.FirstName, myUser.FirstName);
            Assert.AreEqual(createdUser.FirstName, myUser.FirstName);
        }

        [TestMethod]
        public void GetItem_WithId_ReturnsCorrectUser()
        {
            UserManager manager = new();
            User user = manager.GetItem(42);

            Assert.AreEqual(user.Id, 42);
        }

        [TestMethod]
        public void List_WithDataInList_ReturnsDataInList()
        {
            UserManager manager = new();
            ICollection<User> expectedData = DeleteMe.Users;
            ICollection<User> listData = manager.List();

            Assert.IsTrue(listData == expectedData);
        }

        [TestMethod]
        public void Remove_WithId_ReturnsTrue()
        {
            UserManager manager = new();

            Assert.IsTrue(manager.Remove(1));
        }

        [TestMethod]
        public void Remove_OutOfRange_ReturnsFalse()
        {
            UserManager manager = new();

            Assert.IsFalse(manager.Remove(-1));
            Assert.IsFalse(manager.Remove(99999));
        }

        [TestMethod]
        public void Save_UserData_UpdatesUser()
        {
            UserManager manager = new();
            User user = new User() { Id = 1, FirstName = "TestFirstName", LastName = "TestLastName" };

            manager.Save(user);

            Assert.AreEqual(manager.GetItem(1).FirstName, user.FirstName);
            Assert.AreEqual(manager.GetItem(1).LastName, user.LastName);
        }
    }
}