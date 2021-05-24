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

        // https://stackoverflow.com/questions/273313/randomize-a-listt
        private void RandomizeListOrder<user>(IList<user> list)
        {
            Random rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                user value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public AssignmentResult GenerateAssignments(int id)
        {

            if (!MockData.Groups.ContainsKey(id))
            {
                return AssignmentResult.Error("Group not found");
            }
            else if (MockData.Groups[id].Users.Count() <= 2)
            {
                return AssignmentResult.Error("Group Group must have at least three users");
            }


            List<User> users = MockData.Groups[id].Users;
            RandomizeListOrder(users);

            for (int i = 0; i < MockData.Groups[id].Users.Count(); i++)
            {
                if (i < users.Count - 1)
                {
                    MockData.Groups[id].Assignments.Add(new Assignment(users[i], users[i + 1]));
                }
                else
                {
                    MockData.Groups[id].Assignments.Add(new Assignment(users[i], users[0]));
                }
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