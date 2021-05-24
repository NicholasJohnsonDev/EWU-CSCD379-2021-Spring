using System;
using System.Collections.Generic;
using SecretSanta.Data;
using System.Linq;

namespace SecretSanta.Business
{
    public class GroupRepository : IGroupRepository
    {
        public Group Create(Group item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            MockData.Groups[item.Id] = item;
            return item;
        }

        public Group? GetItem(int id)
        {
            if (MockData.Groups.TryGetValue(id, out Group? user))
            {
                return user;
            }
            return null;
        }

        public ICollection<Group> List()
        {
            return MockData.Groups.Values;
        }

        public bool Remove(int id)
        {
            return MockData.Groups.Remove(id);
        }

        public AssignmentResult GenerateAssignments(int id)
        {
            if (MockData.Groups[id].Users.Count() <= 2)
                return AssignmentResult.Error("There are not enough users in this group");

            for (int i = 0; i < MockData.Groups[id].Users.Count(); i++)
            {
                MockData.Groups[id].Assignments[i] = new(MockData.Groups[id].Users[i], MockData.Groups[id].Users[i]);
            }
            return AssignmentResult.Success();
        }

        public void Save(Group item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            MockData.Groups[item.Id] = item;
        }
    }
}