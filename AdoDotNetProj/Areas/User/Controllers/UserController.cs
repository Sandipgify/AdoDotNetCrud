using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using User.EntityModels.Dto;
using User.Service.Interface;

namespace AdoDotNetProj.Areas.User.Controllers
{
    [Area("User")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly INotyfService _notyf;

        public UserController(IUserService userService, INotyfService notyf)
        {
            _userService = userService;
            _notyf = notyf;
        }

        public async Task<ActionResult> Index()
        {
            var user =await _userService.GetAllAsync();
            return View(user);
        }

        public async Task<ActionResult> Details(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            return View(user);
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Register(UserDto userDto)
        {
            if (!ModelState.IsValid)
            {
                _notyf.Error("Invalid Data");
                return View(userDto);
            }
            try
            {
                if (await _userService.CheckEmailExist(userDto.Email))
                {
                    _notyf.Error("Email already exist.");
                    return View(userDto);
                }
                await _userService.SaveAsync(userDto);
                _notyf.Information("User Registration Successfully");
                return RedirectToAction(nameof(Index),new { area = "User" });
            }
            catch (Exception ex)
            {
                _notyf.Error("Error while adding user");
                return View(userDto);
            }
        }

        public async Task<ActionResult> Edit(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            //ViewBag.Id = id;
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(int id, UserDto userDto)
        {
            if (!ModelState.IsValid)
            {
                _notyf.Error("Invalid Data");
                return RedirectToAction("Edit", new { id = id });
            }
            try
            {
                var user = _userService.UpdateAsync(id, userDto);
                _notyf.Information("User Updated Successfully");
                return RedirectToAction(nameof(Index), new { area = "User" });
            }
            catch (Exception ex)
            {
                _notyf.Error("Error while updating user");
                return RedirectToAction("Edit", new { id = id });
            }
        }
    }
}
