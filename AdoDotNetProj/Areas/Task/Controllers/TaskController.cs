using AdoDotNetProj.EntityModels.Dto;
using AdoDotNetProj.Manager;
using AdoDotNetProj.Services.Interface;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using User.EntityModels.Dto;
using User.Service.Interface;

namespace AdoDotNetProj.Areas.Task.Controllers
{
    [Area("Task")]
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;
        private readonly IUserService _userService;
        private readonly INotyfService _notyf;

        public TaskController(ITaskService taskService, IUserService userService, INotyfService notyf)
        {
            _taskService = taskService;
            _userService = userService;
            _notyf = notyf;
        }
        public async Task<ActionResult> Index()
        {
            var task = await _taskService.GetAllAsync();
            ViewBag.userList = (await _userService.UserList()).ToList();
            return View(task);
        }
        [HttpPost]
        public async Task<ActionResult> Index(int AssignedTo)
        {
            var task = await _taskService.GetAssignedTaskAsync(AssignedTo);
            ViewBag.userList = (await _userService.UserList()).ToList();
            return View(task);
        }


        public async Task<ActionResult> Details(int id)
        {
            var task = await _taskService.GetByIdAsync(id);
            return View(task);
        }

        public async Task<ActionResult> Create()
        {
            ViewBag.userList = (await _userService.UserList()).ToList();
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(TaskDto taskDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _notyf.Error("Data Not Valid");
                    return View();
                }

                ViewBag.userList = (await _userService.UserList()).ToList();

                taskDto.CreatedBy = _userService.GetCurrentUserId();

                await _taskService.SaveAsync(taskDto);
                _notyf.Information("Task Created Successfully");
                return RedirectToAction(nameof(Index), new { area = "Task" });
            }
            catch (Exception)
            {
                _notyf.Error("error while creating task");
                return View(taskDto);
            }
        }


        public async Task<ActionResult> Edit(int id)
        {
            var user = await _taskService.GetByIdAsync(id);
           
            return View(user);
        }


        [HttpPost]
        public ActionResult Edit(int id, TaskDto taskDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _notyf.Error("Data Not Valid");
                    return RedirectToAction("Edit", new { id = id });
                }

                var user = _taskService.UpdateAsync(id, taskDto);
              
                    _notyf.Information("Task Updated Successfully");
                return RedirectToAction(nameof(Index), new { area = "Task" });
            }
            catch (Exception)
            {
                _notyf.Error("Error while updating task");
                return RedirectToAction("Edit", new { id = id });
            }
        }
        
        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                var user = _taskService.DeleteTaskAsync(id);
                _notyf.Information("Task Deleted Successfully");
               
            }
            catch (Exception)
            {
                _notyf.Error("Error while deleting task");
            }

            return RedirectToAction(nameof(Index), new { area = "Task" });
        }

        public async Task<ActionResult> AssignTask(int id)
        {
            var user = await _taskService.GetByIdAsync(id);
            ViewBag.userList = (await _userService.UserList()).ToList();
            return View(user);
        }


        [HttpPost]
        public async Task<ActionResult> AssignTask(int id, TaskDto taskDto)
        {
            if(!ModelState.IsValid)
            {
                _notyf.Error("Invalid Data");
                return RedirectToAction("AssignTask", new { id = id });
            }
            try
            {
                if(taskDto.AssignTo==null)
                {
                    _notyf.Error("Please select AssignTo");
                    return RedirectToAction("AssignTask", new { id = id });
                }
                ViewBag.userList = (await _userService.UserList()).ToList();
                var user =await _taskService.AsignTaskAsync(id, taskDto);
                _notyf.Information("Task Assigned Successfully");
                return RedirectToAction(nameof(Index), new { area = "Task" });
            }
            catch (Exception ex)
            {
                _notyf.Error("Error while assigning task");
                return RedirectToAction("AssignTask", new { id = id });
            }
        }
    }
}
