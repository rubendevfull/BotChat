﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BotBaseChatDomain.MessagesAggregate;
using BotBasedChatInfrastructure.ModelSecurity;
using BotBasedChatWebV5.Application.Queries.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BotBasedChatWebV5.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly UserManager<UserIdentity> _userManager;
        private readonly IMessageQueries _messageQueries;

        

        public HomeController(UserManager<UserIdentity> userManager,
            IMessageQueries messageQueries)
        {
            _messageQueries = messageQueries;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var dataUser = await _userManager.GetUserAsync(HttpContext.User);
            ViewBag.UserName = dataUser.UserName;//"testBurden"; 
            return View();
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Message>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetMsgRoom(int roomId)
        {
            if (roomId <= 0)
                return BadRequest();

            var result = await _messageQueries.GetMessagesByRoom(roomId, false);
            if (result == null)
                return NotFound();

            return Json(result);
        }
    }
}