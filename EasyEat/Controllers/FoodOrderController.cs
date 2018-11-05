using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EasyEat.Models;
using EasyEat.Repositories;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;


namespace EasyEat.Controllers
{
    [Produces("application/json")]
    [Route("api/FoodOrder")]
    public class FoodOrderController : Controller
    {
        IRepository<FoodOrder> dbFoodOrder;
        IRepository<Menu> dbMenu;


        public FoodOrderController()
        {
            dbFoodOrder = new FoodOrderRepository();
            dbMenu = new MenuRepository();
        }
    }
}