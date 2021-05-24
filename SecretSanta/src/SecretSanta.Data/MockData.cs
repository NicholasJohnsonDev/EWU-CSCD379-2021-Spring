using System.Collections.Generic;

namespace SecretSanta.Data
{
    public static class MockData
    {
        public static Dictionary<int, User> Users { get; } = new()
        {
            {
                1,
                new User
                {
                    Id = 1,
                    FirstName = "BoJack",
                    LastName = "Horseman"
                }
            },
            {
                2,
                new User
                {
                    Id = 2,
                    FirstName = "Princess",
                    LastName = "Carolyn"
                }
            },
            {
                3,
                new User
                {
                    Id = 3,
                    FirstName = "Diane",
                    LastName = "Nguyen"
                }
            },
            {
                4,
                new User
                {
                    Id = 4,
                    FirstName = "Mr.",
                    LastName = "Peanutbutter"
                }
            },
            {
                5,
                new User
                {
                    Id = 5,
                    FirstName = "Todd",
                    LastName = "Chavez"
                }
            },
            {
                6,
                new User
                {
                    Id = 6,
                    FirstName = "Charley",
                    LastName = "Witherspoon"
                }
            },
            {
                7,
                new User
                {
                    Id = 7,
                    FirstName = "Judah",
                    LastName = "Mannowdog"
                }
            }
        };

        public static Dictionary<int, Group> Groups { get; } = new()
        {
            {
                1,
                new Group
                {
                    Id = 1,
                    Name = "Princess and Judah Wedding"
                }
            },
            {
                2,
                new Group
                {
                    Id = 2,
                    Name = "The Lonely One Person Exchange"
                }
            }
        };

        static MockData()
        {
            PutUserInGroup(Users[1], Groups[1]);
            PutUserInGroup(Users[2], Groups[1]);
            PutUserInGroup(Users[3], Groups[1]);
            PutUserInGroup(Users[4], Groups[1]);
            PutUserInGroup(Users[5], Groups[1]);
            PutUserInGroup(Users[6], Groups[1]);
            PutUserInGroup(Users[7], Groups[1]);
            PutUserInGroup(Users[1], Groups[2]);

            static void PutUserInGroup(User user, Group group)
            {
                user.Groups.Add(group);
                group.Users.Add(user);
            }
        }
    }
}