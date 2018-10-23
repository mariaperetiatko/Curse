using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EasyEat.Models;


namespace EasyEat.Controllers
{


    [Produces("application/json")]
    [Route("api/Restaurant")]
    public class RestaurantController : Controller
    {

        EatContext db;

        public RestaurantController(EatContext context)
        {
            this.db = context;
        }
        // GET: api/Restaurant
        [HttpGet]
        public IEnumerable<Restaurant> Get()
        {
            return db.Restaurant.ToList();
        }

        // GET: api/Restaurant/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            Restaurant restaurant = db.Restaurant.FirstOrDefault(x => x.Id == id);
            if (restaurant == null)
                return NotFound();
            return new ObjectResult(restaurant);
        }

        // POST: api/Restaurant
        [HttpPost]
        public IActionResult Create([FromBody]Restaurant restaurant)
        {
            if (restaurant == null)
            {
                return BadRequest();
            }
            db.Restaurant.Add(restaurant);
            db.SaveChanges();
            return Ok(restaurant);
        }


        // PUT: api/Restaurant/5
        [HttpPut("{id}")]
        public IActionResult Update([FromBody]Restaurant restaurant)
        {
            if (restaurant == null)
            {
                return BadRequest();
            }
            if (!db.Restaurant.Any(x => x.Id == restaurant.Id))
            {
                return NotFound();
            }

            db.Update(restaurant);
            db.SaveChanges();
            return Ok(restaurant);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Restaurant restaurant = db.Restaurant.FirstOrDefault(x => x.Id == id);
            if (restaurant == null)
            {
                return NotFound();
            }
            restaurant.IsDeleted = 1;
            db.SaveChanges();
            return Ok(restaurant);
        }
    }
}
