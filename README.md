Assingnment 7 - Nicholas Johnson

In this assignment we are going to create some automated web tests as well as convert Gifts screens to use AlpineJS instead of the MVC model

# Assignment

- Using PlaywrightSharp, automate the following items of the website
  - Verify that when landing on the homepage, Secret Santa is displayed in the navigation banner ✔❌
  - Verify that all 3 pages (Users, Groups, Gifts) can be reached via clicking on the navigation elements ✔❌
  - Create a User (should assert that the number of users after the creation is one greater than the number of users before the user was created) ✔❌
  - Modify the last user in the list (should assert that the updated data was successfully saved/displayed to the screen) ✔❌
  - Delete the last user in the list (should assert that the number of users after the delete is one less than the number of users before the deletion) ✔❌
  - All of these tests should be able to be executed without having to manually start the Web and Api projects ✔❌
- Using AlpineJS, convert the CRUD pages for Gifts to be javascript based instead of MVC based
  - Index page should fetch the users via javascript directly from the API ✔❌
  - Create page should submit the gifts information via javascript directly to the API. Upon successful save, the user should be redirected back to the Gifts index page ✔❌
  - Edit page should submit the gifts information via javascript directly to the API. Upon successful update, the user should be redirected back to the Gifts index page ✔❌
  - Delete opration should submit the request directly to the API via javascript and then refresh the Index page via a javascript call directly to the API ✔❌
