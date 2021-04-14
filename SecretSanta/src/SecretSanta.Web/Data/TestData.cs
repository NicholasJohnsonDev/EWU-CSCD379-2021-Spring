using System.Collections.Generic;
using SecretSanta.Web.ViewModels;

namespace SecretSanta.Web.Data {
  public static class TestData {
    public static List<UserViewModel> Users = new List<UserViewModel>() {
      new UserViewModel {ID = 0, FirstName = "Luke", LastName = "Skywalker"},
      new UserViewModel {ID = 1, FirstName = "Han", LastName = "Solo"},
      new UserViewModel {ID = 2, FirstName = "Jar Jar", LastName = "Binks"}
    };

    public static List<GroupViewModel> Groups = new List<GroupViewModel>() {
      new GroupViewModel {ID = 0, Name = "Work"},
      new GroupViewModel {ID = 1, Name = "Family"},
      new GroupViewModel {ID = 2, Name = "Starwars"}
    };

    public static List<GiftViewModel> Gifts = new List<GiftViewModel>() {
      new GiftViewModel {
        ID = 0, 
        Title = "Lightsaber", 
        Description = "Light Up Laser Sword, 2-in-1 with 7 Adjustable Colors.", 
        URL = "https://www.amazon.com/Lightsaber-Sensitive-Adjustable-Fighters-Warriors/dp/B08XTSH59G/ref=sr_1_6?dchild=1&keywords=lightsaber&qid=1618365116&sr=8-6", 
        Priority = 1,
        UserID = 0
      },
      new GiftViewModel {
        ID = 1, 
        Title = "Millennium Falcon", 
        Description = "LEGO Star Wars Millennium Falcon Microfighter.", 
        URL = "https://www.amazon.com/LEGO-Millennium-Microfighter-Building-Construction/dp/B08HVZYX76/ref=sr_1_16?dchild=1&keywords=millennium+falcon&qid=1618365237&sr=8-16", 
        Priority = 1,
        UserID = 1
      },
      new GiftViewModel {
        ID = 2, 
        Title = "Frog", 
        Description = "Oh, Mooie-Mooie! Theesa look real tasty!", 
        URL = "https://www.amazon.com/Toysmith-8601-Ginormous-Grow-Frog/dp/B000ICXLY2/ref=sr_1_37?dchild=1&keywords=frog+in+a+jar&qid=1618365299&sr=8-37", 
        Priority = 1,
        UserID = 2
      }
    };
  }
}
