using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotBasedChatInfrastructure.ModelSecurity;
using BotBasedChatWebV5.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BotBasedChatWebV5.Controllers
{
    [Authorize]
    public class SecurityController : Controller
    {
        private readonly UserManager<UserIdentity> _userManager;
        private readonly SignInManager<UserIdentity> _signInManager;

        public SecurityController(UserManager<UserIdentity> userManager,
            SignInManager<UserIdentity> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string user, string pass)
        {
            var rp = new ResponseBasicVm();
            try
            {
                var result = await _signInManager.PasswordSignInAsync(user, pass, true, false);
                if (result.Succeeded)
                {
                    rp.Status = true;
                }
                else
                {
                    rp.Status = false;
                    rp.MsgBad.Add("Invalid login attempt.");
                }
            }
            catch (Exception e)
            {
                rp.Status = false;
                rp.MsgBad.Add(e.ToString());
            }
            return Json(rp);
        }

        [AllowAnonymous]
        public async Task<IActionResult> SignUp(string mail, string pass, string profileMetadata)
        {
            var rp = new ResponseBasicVm();
            if (mail != null && mail != string.Empty &&
               pass != null && pass != string.Empty &&
               profileMetadata != null && profileMetadata != string.Empty)
            {
                var user = new UserIdentity { UserName = mail, Email = mail };
                var result = await _userManager.CreateAsync(user, pass);
                if (result.Succeeded)
                {
                    await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("profile", profileMetadata));
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    rp.Status = true;
                }
                else
                {
                    rp.Status = false;
                    foreach (var error in result.Errors)
                    {
                        rp.MsgBad.Add(error.Description);
                    }
                    return Json(rp);
                }
            }
            else
            {
                rp.Status = false;
                rp.MsgBad.Add("Wrong parameters sended!");
            }

            return Json(rp);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

    }
}