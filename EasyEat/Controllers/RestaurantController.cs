using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EasyEat.Models;
using EasyEat.Repositories;
using Microsoft.AspNetCore.Authorization;


namespace EasyEat.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/Restaurant")]
    public class RestaurantController : Controller
    {
        IRepository<Restaurant> db;

        public RestaurantController()
        {
            db = new RestaurantRepository();
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<Restaurant> Get()
        {
            return db.GetEntityList();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Restaurant restaurant = db.GetEntity(id);
            if (restaurant == null)
                return NotFound();
            return new ObjectResult(restaurant);
        }

        // POST api/<controller>
        [Authorize(Roles = "Admin, RestaurantOwner")]
        [HttpPost]
        public IActionResult Create(Restaurant restaurant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            db.Create(restaurant);
            db.Save();
            return Ok(restaurant);
        }

        // PUT api/<controller>
        [Authorize(Roles = "Admin, RestaurantOwner")]
        [HttpPut]
        public IActionResult Update([FromBody]Restaurant restaurant)
        {
            if (restaurant == null)
            {
                return BadRequest();
            }

            db.Update(restaurant);
            db.Save();
            return Ok(restaurant);
        }

        // DELETE api/<controller>/5
        [Authorize(Roles = "Admin, RestaurantOwner")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Restaurant restaurant = db.GetEntity(id);
            if (restaurant == null)
            {
                return NotFound();
            }
            db.Delete(id);
            db.Save();
            return Ok(restaurant);
        }
    }
}
