using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EasyEat.Models;
using EasyEat.Repositories;
using Microsoft.AspNetCore.Authorization;
using EasyEat.BusinessLogic;

namespace EasyEat.Controllers
{
    [Produces("application/json")]
    [Route("api/FavouriteDish")]
    public class FavouriteDishController : Controller
    {
        FavouriteDishRepository db;
        DishRepository dishRepository;

        public FavouriteDishController()
        {
            db = new FavouriteDishRepository();
            dishRepository = new DishRepository();
        }

        // GET: api/<controller>
        /*
        [Authorize(Roles = "Admin, Member")]
        */
        [HttpGet]
        public IEnumerable<FavouriteDish> Get()
        {
            string userJWTId = User.FindFirst("id")?.Value;
            Customer customer = db.GetCustomer(userJWTId);
            if (customer == null)
            {
                return db.GetEntityList();
            }
            List<FavouriteDish> favouriteDish = db.GetFavouriteDishByCustomer(customer.Id).ToList();
            for (int i = 0; i < favouriteDish.Count(); i++)
                favouriteDish[i].Customer = null;
            return favouriteDish.AsEnumerable();
        }

        // GET api/<controller>/5
        /*
        [Authorize(Roles = "Admin, Member")]
        */
        [HttpGet("{id}")]
        [ActionName("Get")]
        public IActionResult Get([FromQuery]FavouriteDishKey id)
        {
            FavouriteDish dish = db.GetEntity(id);
            if (dish == null)
                return NotFound();
            return new ObjectResult(dish);
        }

        // POST api/<controller>
        /*
        [Authorize(Roles = "Admin, Member")]
        */
        [HttpPost]
        [ProducesResponseType(typeof(FavouriteDish), StatusCodes.Status200OK)]
        public IActionResult Create([FromBody]FavouriteDish dish)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            string userJWTId = User.FindFirst("id")?.Value;
            Customer customer = db.GetCustomer(userJWTId);
            if (customer != null)
            {
                dish.CustomerId = customer.Id;
            }
            db.Create(dish);
            db.Save();
            dish.Customer = null;
            dish.Dish = null;
            return Ok(dish);
        }

        // PUT api/<controller>
        /*
        [Authorize(Roles = "Admin, Member")]
        */
        [HttpPut]
        public IActionResult Update([FromBody]FavouriteDish dish)
        {
            if (dish == null)
            {
                return BadRequest();
            }
            string userJWTId = User.FindFirst("id")?.Value;
            Customer customer = db.GetCustomer(userJWTId);
            if (customer != null)
            {
                dish.CustomerId = customer.Id;
            }
            db.Update(dish);
            db.Save();
            return Ok(dish);
        }

        // DELETE api/<controller>/5
        /*
        [Authorize(Roles = "Admin, Member")]
        */
        [HttpDelete("{id}")]
        [ActionName("Delete")]
        [ProducesResponseType(typeof(FavouriteDish), StatusCodes.Status200OK)]
        public IActionResult Delete([FromQuery]FavouriteDishKey id)
        {
            string userJWTId = User.FindFirst("id")?.Value;
            Customer customer = db.GetCustomer(userJWTId);
            if (customer != null)
            {
                id.CustomerId = customer.Id;
                
            }
            FavouriteDish dish = db.GetEntity(id);
            if (dish == null)
            {
                return NotFound();
            }

            db.Delete(id);
            db.Save();
            return Ok(dish);
        }

        // GET: api/<controller>
        /*
        [Authorize(Roles = "Admin, Member")]
        */
        [ProducesResponseType(typeof(IEnumerable<Dish>), StatusCodes.Status200OK)]
        [HttpGet("GetFavouriteDishesByCustomer")]
        public IEnumerable<Dish> GetFavouriteDishesByCustomer()
        {
            string userJWTId = User.FindFirst("id")?.Value;
            Customer customer = db.GetCustomer(userJWTId);
            if (customer == null)
            {
                return dishRepository.GetEntityList();
            }
            List<int> favouriteDishesIds = db.GetFavouriteDishByCustomer(customer.Id)
                .Select(x => x.DishId).ToList();

            List<Dish> favouriteDishes = new List<Dish>();
            for (int i = 0; i < favouriteDishesIds.Count(); i++)
            {
                favouriteDishes.Add(dishRepository.GetEntity(favouriteDishesIds[i]));
                favouriteDishes.Last().Ingredient = null;
                favouriteDishes.Last().Menu = null;
            }
            return favouriteDishes.AsEnumerable();

        }

        // GET: api/<controller>
        /*
        [Authorize(Roles = "Admin, Member")]
        */
        [ProducesResponseType(typeof(IEnumerable<Dish>), StatusCodes.Status200OK)]
        [HttpGet("GetNotFavouriteDishesByCustomer")]
        public IEnumerable<Dish> GetNotFavouriteDishesByCustomer()
        {
            string userJWTId = User.FindFirst("id")?.Value;
            Customer customer = db.GetCustomer(userJWTId);
            if (customer == null)
            {
                return dishRepository.GetEntityList();
            }
            List<int> favouriteDishesIds = db.GetFavouriteDishByCustomer(customer.Id)
                .Select(x => x.DishId).ToList();
            List<int> allDishesIds = dishRepository.GetEntityList().Select(x => x.Id).ToList();
            List<Dish> notFavouriteDishes = new List<Dish>();
            for (int i = 0; i < allDishesIds.Count(); i++)
            {
                if (!favouriteDishesIds.Contains(allDishesIds[i]))
                {
                    notFavouriteDishes.Add(dishRepository.GetEntity(allDishesIds[i]));
                    notFavouriteDishes.Last().Ingredient = null;
                    notFavouriteDishes.Last().Menu = null;
                }
            }
            return notFavouriteDishes.AsEnumerable();
        }
    }
}