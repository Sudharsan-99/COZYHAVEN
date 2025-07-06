using CozyHaven.MVC.Models;
using DAL.Models.Main;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.Interfaces;

namespace CozyHaven.MVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetAllUsersAsync();

            var viewModels = users.Select(u => new UserViewModel
            {
                Id = u.Id,
                FullName = u.FullName,
                Email = u.Email,
                Role = u.Role,
                Gender = u.Gender,
                ContactNumber = u.ContactNumber,
                Address = u.Address
            }).ToList();

            return View(viewModels);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(UserViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = new User
            {
                FullName = model.FullName,
                Email = model.Email,
                Role = model.Role,
                Gender = model.Gender,
                ContactNumber = model.ContactNumber,
                Address = model.Address,
                PasswordHash = "default" // handle password securely elsewhere
            };

            await _userService.AddUserAsync(user);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound();

            var model = new UserViewModel
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role,
                Gender = user.Gender,
                ContactNumber = user.ContactNumber,
                Address = user.Address
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = new User
            {
                Id = model.Id,
                FullName = model.FullName,
                Email = model.Email,
                Role = model.Role,
                Gender = model.Gender,
                ContactNumber = model.ContactNumber,
                Address = model.Address
            };

            await _userService.UpdateUserAsync(user);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound();

            var model = new UserViewModel
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role,
                Gender = user.Gender,
                ContactNumber = user.ContactNumber,
                Address = user.Address
            };

            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound();

            var model = new UserViewModel
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role
            };

            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _userService.DeleteUserAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
