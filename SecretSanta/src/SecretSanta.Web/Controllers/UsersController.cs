using System;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Web.Data;
using SecretSanta.Web.ViewModels;
using SecretSanta.Web.Api;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace SecretSanta.Web.Controllers
{
    public class UsersController : Controller
    {
        public IUsersClient Client { get; }

        public UsersController(IUsersClient client)
        {
            Client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<IActionResult> Index()
        {
            ICollection<UpdateUser> users = await Client.GetAllAsync();
            List<UserViewModel> viewModelUsers = new();
            int counter = users.Select(item => item.Id).Max() ?? 0;
            foreach (UpdateUser user in users)
            {
                viewModelUsers.Add(new UserViewModel
                {
                    Id = user.Id ?? counter,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                });
            }
            return View(viewModelUsers);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await Client.PostAsync(new UpdateUser
                {
                    Id = viewModel.Id,
                    FirstName = viewModel.FirstName,
                    LastName = viewModel.LastName
                });
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            UpdateUser updateUser = await Client.GetAsync(id);
            UserViewModel userViewModel = new()
            {
                Id = id,
                FirstName = updateUser.FirstName,
                LastName = updateUser.LastName
            };
            return View(userViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await Client.PutAsync(viewModel.Id, new UpdateUser
                {
                    Id = viewModel.Id,
                    FirstName = viewModel.FirstName,
                    LastName = viewModel.LastName
                });
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await Client.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}