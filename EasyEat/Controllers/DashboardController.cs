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

namespace EasyEat.Controllers
{
    [Authorize(Policy = "User")]
    [Route("api/[controller]/[action]")]
    public class DashboardController : Controller
    {
        private readonly ClaimsPrincipal _caller;
        private readonly EatContext _appDbContext;

        public DashboardController(UserManager<User> userManager, EatContext appDbContext, IHttpContextAccessor httpContextAccessor)
        {
            _caller = httpContextAccessor.HttpContext.User;
            _appDbContext = appDbContext;
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