using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Data;

namespace SecretSanta.Business.Tests
{
    [TestClass]
    public class GroupRepositoryTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_NullItem_ThrowsArgumentException()
        {
            GroupRepository sut = new();

            sut.Create(null!);
        }

        [TestMethod]
        public void Create_WithItem_CanGetItem()
        {
            GroupRepository sut = new();
            Group user = new()
            {
                Id = 42
            };

            Group createdGroup = sut.Create(user);

            Group? retrievedGroup = sut.GetItem(createdGroup.Id);
            Assert.AreEqual(user, retrievedGroup);
        }

        [TestMethod]
        public void GetItem_WithBadId_ReturnsNull()
        {
            GroupRepository sut = new();

            Group? user = sut.GetItem(-1);

            Assert.IsNull(user);
        }

        [TestMethod]
        public void GetItem_WithValidId_ReturnsGroup()
        {
            GroupRepository sut = new();
            sut.Create(new()
            {
                Id = 42,
                Name = "Group",
            });

            Group? user = sut.GetItem(42);

            Assert.AreEqual(42, user?.Id);
            Assert.AreEqual("Group", user!.Name);
        }

        [TestMethod]
        public void List_WithGroups_ReturnsAllGroup()
        {
            GroupRepository sut = new();
            sut.Create(new()
            {
                Id = 42,
                Name = "Group",
            });

            ICollection<Group> users = sut.List();

            Assert.AreEqual(MockData.Groups.Count, users.Count);
            foreach (var mockGroup in MockData.Groups.Values)
            {
                Assert.IsNotNull(users.SingleOrDefault(x => x.Name == mockGroup.Name));
            }
        }

        [TestMethod]
        [DataRow(-1, false)]
        [DataRow(42, true)]
        public void Remove_WithInvalidId_ReturnsTrue(int id, bool expected)
        {
            GroupRepository sut = new();
            sut.Create(new()
            {
                Id = 42,
                Name = "Group"
            });

            Assert.AreEqual(expected, sut.Remove(id));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Save_NullItem_ThrowsArgumentException()
        {
            GroupRepository sut = new();

            sut.Save(null!);
        }

        [TestMethod]
        public void Save_WithValidItem_SavesItem()
        {
            GroupRepository sut = new();

            sut.Save(new Group() { Id = 42 });

            Assert.AreEqual(42, sut.GetItem(42)?.Id);
        }

        [TestMethod]
        public void GenerateAssignments_WithNullId_ReturnsError()
        {
            GroupRepository sut = new();

            AssignmentResult result = sut.GenerateAssignments(42);

            Assert.AreEqual("Group not found", result.ErrorMessage);
        }

        [TestMethod]
        public void GenerateAssignments_WithInvalidId_CreatesAssignments()
        {
            GroupRepository sut = new();
            Group group = sut.Create(new()
            {
                Id = 58961,
                Name = "Characters From a TV Show"
            });
            group.Users.Add(new User { Id = 1, FirstName = "BoJack", LastName = "Horseman" });
            group.Users.Add(new User { Id = 2, FirstName = "Princess", LastName = "Carolyn" });
            group.Users.Add(new User { Id = 3, FirstName = "Diane", LastName = "Nguyen" });
            group.Users.Add(new User { Id = 4, FirstName = "Mr.", LastName = "Peanutbutter" });
            group.Users.Add(new User { Id = 5, FirstName = "Todd", LastName = "Chavez" });

            AssignmentResult result = sut.GenerateAssignments(42);

            Assert.AreEqual("Group not found", result.ErrorMessage);
        }

        [TestMethod]
        public void GenerateAssignments_WithLessThanThreeUsers_ReturnsError()
        {
            GroupRepository sut = new();
            sut.Create(new()
            {
                Id = 42,
                Name = "Group"
            });

            AssignmentResult result = sut.GenerateAssignments(42);

            Assert.AreEqual($"Group Group must have at least three users", result.ErrorMessage);
        }

        [TestMethod]
        public void GenerateAssignments_WithValidGroup_CreatesAssignments()
        {
            GroupRepository sut = new();
            Group group = sut.Create(new()
            {
                Id = 42,
                Name = "Characters From a TV Show"
            });
            group.Users.Add(new User { Id = 1, FirstName = "BoJack", LastName = "Horseman" });
            group.Users.Add(new User { Id = 2, FirstName = "Princess", LastName = "Carolyn" });
            group.Users.Add(new User { Id = 3, FirstName = "Diane", LastName = "Nguyen" });
            group.Users.Add(new User { Id = 4, FirstName = "Mr.", LastName = "Peanutbutter" });
            group.Users.Add(new User { Id = 5, FirstName = "Todd", LastName = "Chavez" });

            AssignmentResult result = sut.GenerateAssignments(42);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(5, group.Assignments.Count);
            Assert.AreEqual(5, group.Assignments.Select(x => x.Giver.FirstName).Distinct().Count());
            Assert.AreEqual(5, group.Assignments.Select(x => x.Receiver.FirstName).Distinct().Count());
            Assert.IsFalse(group.Assignments.Any(x => x.Giver.FirstName == x.Receiver.FirstName));
        }
    }
}