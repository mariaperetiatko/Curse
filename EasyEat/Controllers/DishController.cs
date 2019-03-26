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
    /*[Authorize]*/
    [Produces("application/json")]
    [Route("api/Dish")]
    public class DishController : Controller
    {
        DishRepository db;
        MainLogic ml;

        public DishController()
        {
            db = new DishRepository();
            ml = new MainLogic();
        }

        // GET: api/<controller>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Dish>), StatusCodes.Status200OK)]
        public IEnumerable<Dish> Get()
        {
            return db.GetEntityList();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Dish), StatusCodes.Status200OK)]
        public IActionResult Get(int id)
        {
            Dish dish = db.GetEntity(id);
            if (dish == null)
                return NotFound();
            return new ObjectResult(dish);
        }

        // POST api/<controller>
        /*
        [Authorize(Roles = "Admin, RestaurantOwner")]
        */
        [HttpPost]
        [ProducesResponseType(typeof(Dish), StatusCodes.Status200OK)]
        public IActionResult Create([FromBody]Dish dish)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            db.Create(dish);
            db.Save();
            return Ok(dish);
        }

        // PUT api/<controller>
        /*
        [Authorize(Roles = "Admin, RestaurantOwner")]
        */
        [HttpPut]
        [ProducesResponseType(typeof(Dish), StatusCodes.Status200OK)]
        public IActionResult Update([FromBody]Dish dish)
        {
            if (dish == null)
            {
                return BadRequest();
            }
            db.Update(dish);
            db.Save();
            return Ok(dish);
        }

        // DELETE api/<controller>/5
        /*
        [Authorize(Roles = "Admin, RestaurantOwner")]
        */
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Dish), StatusCodes.Status200OK)]
        public IActionResult Delete(int id)
        {
            Dish dish = db.GetEntity(id);
            if (dish == null)
            {
                return NotFound();
            }
            db.Delete(id);
            db.Save();
            return Ok(dish);
        }

        // DELETE api/<controller>/5
        /*
        [Authorize]
        */
        [HttpGet("CaloricValue/{dishid}")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public int CaloricValue(int dishid)
        {
            return ml.GetDishCaloricValue(dishid);
        }
    }
}