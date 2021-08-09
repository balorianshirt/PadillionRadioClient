using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PadillionRadio.Business.Models;
using PadillionRadio.Business.Services;
using PadillionRadio.Helpers;
using PadillionRadio.Models;

namespace PadillionRadio.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly IUserService userService;

        public HomeController(ILogger<HomeController> logger, IUserService userService)
        {
            this.logger = logger;
            this.userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var users = await userService.GetList();
            if (users.Any())
            {
                return View(users);
            }

            return View(new List<UserModel>());
        }

        public IActionResult AddUser() => View();

        [HttpPost]
        public async Task<IActionResult> AddUser(UserModel model)
        {
            var user = await userService.Create(await model.CodeGenerator(userService));

            if (user == null)
            {
                logger.LogInformation( "error occured while adding user");
                return Error();
            }

            return RedirectToAction("Index");
        }

        public IActionResult EditUser() => View();
        
        [HttpPost]
        public async Task<IActionResult> EditUser(UserModel model)
        {
            var didUserUpdate = await userService.Update(model);
            
            if (didUserUpdate)
            {
                logger.LogInformation( "error occured while editing user");
                return Error();
            }
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        public async Task<IActionResult> DeleteUser(long id)
        {
            var didUserUpdate = await userService.Delete(id);
            
            if (didUserUpdate == null)
            {
                logger.LogInformation( "error occured while deleting user");
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}