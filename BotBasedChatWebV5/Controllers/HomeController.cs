using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotBasedChatInfrastructure.ModelSecurity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BotBasedChatWebV5.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly UserManager<UserIdentity> _userManager;

        public HomeController(UserManager<UserIdentity> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var dataUser = await _userManager.GetUserAsync(HttpContext.User);
            ViewBag.UserName = dataUser.UserName;//"testBurden"; 
            return View();
        }
    }
}