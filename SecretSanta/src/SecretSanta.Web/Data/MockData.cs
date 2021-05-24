using System.Collections.Generic;
using SecretSanta.Web.ViewModels;

namespace SecretSanta.Web.Data
{
    public static class MockData
    {
        public static List<UserViewModel> Users = new List<UserViewModel>
        {
            new UserViewModel { Id = 1, FirstName = "BoJack", LastName = "Horseman" },
            new UserViewModel { Id = 2, FirstName = "Princess", LastName = "Carolyn" },
            new UserViewModel { Id = 3, FirstName = "Diane", LastName = "Nguyen" },
            new UserViewModel { Id = 4, FirstName = "Mr.", LastName = "Peanutbutter" },
            new UserViewModel { Id = 5, FirstName = "Todd", LastName = "Chavez" },
            new UserViewModel { Id = 6, FirstName = "Charley", LastName = "Witherspoon" },
            new UserViewModel { Id = 7, FirstName = "Judah", LastName = "Mannowdog" },
        };

        public static List<GroupViewModel> Groups = new List<GroupViewModel>
        {
            new GroupViewModel { Id = 1, Name = "Princess and Judah Wedding" },
            new GroupViewModel { Id = 2, Name = "The Lonely One Person Exchange" },
        };

        public static List<GiftViewModel> Gifts = new List<GiftViewModel>
        {
            new GiftViewModel { Id = 1, Title = "D", Description = "From the Hollywoo sign", Url="https://www.google.com", Priority = 2, UserId = 1 },
            new GiftViewModel { Id = 2, Title = "Horsin' Around Disk Set", Description = "All 9 Episodes of the classic family sitcom", Url="https://www.google.com", Priority = 1, UserId = 1 },
            new GiftViewModel { Id = 3, Title = "Ivy Tran, Food Court Detective", Description = "A teal middle-grade fiction book that has the title in purple lettering. It has a picture of a red fast food tray with a hamburger and soda on it.", Url="https://www.google.com", Priority = 2, UserId = 2 },
            new GiftViewModel { Id = 4, Title = "Binoculars", Description = "Good enough to enjoy the view from all the way up", Url="https://www.google.com", Priority = 1, UserId = 2 },
        };
    }
}