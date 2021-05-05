using System.Collections.Generic;
using SecretSanta.Business;
using SecretSanta.Data;

namespace SecretSanta.Api.Tests.Business
{
    public class TestableUserRepository : IUserRepository
    {

        public List<User>? UserList { get; set; }
        public ICollection<User> List()
        {
            if (UserList is null)
            {
                UserList = new List<User>();
            }
            return UserList;
        }

        public User Create(User newUser)
        {
            if (UserList is null)
            {
                UserList = new List<User>();
            }
            UserList.Add(newUser);
            return newUser;
        }

        public User? GetItemUser { get; set; }
        public int GetItemId { get; set; }
        public User? GetItem(int id)
        {
            if (GetItemUser is not null && GetItemUser.Id == id)
            {
                GetItemId = id;
                return GetItemUser;
            }
            return null;
        }

        public bool Remove(int id)
        {
            if (GetItemUser is not null && GetItemUser.Id == id)
            {
                return true;
            }
            return false;
        }

        public User? SavedUser { get; set; }
        public void Save(User item)
        {
            SavedUser = item;
        }
    }
}