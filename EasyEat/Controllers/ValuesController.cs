﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace EasyEat.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        //[Authorize]
        //[Route("getlogin")]
        //public IActionResult GetLogin()
        //{
        //    return Ok($"Ваш логин: {User.Identity.Name}");
        //}

        //[Authorize(Roles = "admin")]
        //[Route("getrole")]
        //public IActionResult GetRole()
        //{
        //    return Ok("Ваша роль: администратор");
        //}
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Server works");
        }
    }
}
