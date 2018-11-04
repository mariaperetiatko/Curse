using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EasyEat.Models;
using Microsoft.Extensions.Localization;
using EasyEat.Helpers;

namespace EasyEat.Controllers
{
    [Authorize(Roles = "Member")]
    [Route("api/[controller]/[action]")]
    public class DashboardController : Controller
    {
        private readonly ClaimsPrincipal _caller;
        private readonly EatContext _appDbContext;
        private readonly IStringLocalizer _localizer;

        public DashboardController(UserManager<User> userManager, EatContext appDbContext,
            IHttpContextAccessor httpContextAccessor, IStringLocalizer localizer)
        {
            _caller = httpContextAccessor.HttpContext.User;
            _appDbContext = appDbContext;
            _localizer = localizer;

        }

        public string  Test()
        {
            string message = _localizer["Message"];
            return message;
        }

        // GET api/dashboard/home
        [HttpGet]
        public async Task<IActionResult> Home()
        {
            // retrieve the user info
            //HttpContext.User
            var userId = _caller.Claims.Single(c => c.Type == "id");
            var customer = await _appDbContext.Customer.Include(c => c.Identity).SingleAsync(c => c.Identity.Id == userId.Value);

            return new OkObjectResult(new
            {
                Message = "This is secure API and user data!"
                
            });
        }
    }
}