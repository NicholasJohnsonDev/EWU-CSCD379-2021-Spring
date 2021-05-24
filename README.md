Nicholas Johnson

In this assignment we are going to build out some of the logic needed for Secret Santa.
There have been several updates to the SecretSanta repository.
Key changes to be aware of:
- The data project has been expanded and contains users for groups and a new Assignment class.
- There is a new `GroupRepository` added to the business project.
- The gifts section links has been removed from the web project. The pages are still present but should not be needed.
- Opening the "edit" page of a group now also shows the users in a group, and has separate controls for editing the group and adding and removing users from the group.
- No additional Web or E2E tests are needed for this assignment.

# Assignment
- Update the `GroupRepository` with functionality to generate gift assignments. ✔❌
  - A group with with 2 or fewer users should result in an error. This error should be displayed to a user. ✔❌
  - A user is not allowed to be both the Giver and Recipient of the assignment. ✔❌
- Update the GroupsController with an endpoint for generating the assignments. ✔❌
- Update the Groups/Edit page to contain the UI elements for the following
  - A means to generate assignments. ✔❌
  - A means to display any errors that occurs while generating assignments. ✔❌
  - Display of the assignments for each of the users. ✔❌
- Write appropriate unit tests for changes made to the `GroupRepository` ✔❌
- Write appropriate functional tests for the change made to the `GroupsController` ✔❌

## Extra Credit
- There is currently no way to for a user to add gifts. On the edit user page add functionality to be able to add a gift to the user. ✔❌
- Allow for navigating to a user from the Group edit page to make it easy to see the gifts requested by a user. ✔❌
