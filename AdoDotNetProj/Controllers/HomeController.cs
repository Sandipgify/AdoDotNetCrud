using AdoDotNetProj.Models;
using Database.Provider;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using User.Service.Interface;

namespace AdoDotNetProj.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDbAccessManager _dbAccessManager;
        private readonly IUserService _userService;

        public HomeController(ILogger<HomeController> logger, 
            IDbAccessManager connectionProvider, IUserService userService)
        {
            _logger = logger;
            _dbAccessManager = connectionProvider;
            _userService = userService;
        }
        public IActionResult Index()
        {

            ViewBag.UserName = _userService.GetCurrentUserName();

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
