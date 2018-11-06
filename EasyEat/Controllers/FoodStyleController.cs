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
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EasyEat.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/FoodStyle")]
    public class FoodStyleController : Controller
    {
        IRepository<FoodStyle> db;

        public FoodStyleController()
        {
            db = new FoodStyleRepository();
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<FoodStyle> Get()
        {
            return db.GetEntityList();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            FoodStyle foodStyle = db.GetEntity(id);
            if (foodStyle == null)
                return NotFound();
            return new ObjectResult(foodStyle);
        }

        // POST api/<controller>
        [Authorize(Roles = "Admin, RestaurantOwner")]
        [HttpPost]
        public IActionResult Create([FromBody]FoodStyle foodStyle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            db.Create(foodStyle);
            db.Save();
            return Ok(foodStyle);
        }

        // PUT api/<controller>
        [Authorize(Roles = "Admin, RestaurantOwner")]
        [HttpPut]
        public IActionResult Update([FromBody]FoodStyle foodStyle)
        {
            if (foodStyle == null)
            {
                return BadRequest();
            }

            db.Update(foodStyle);
            db.Save();
            return Ok(foodStyle);
        }

        // DELETE api/<controller>/5
        [Authorize(Roles = "Admin, RestaurantOwner")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            FoodStyle foodStyle = db.GetEntity(id);
            if (foodStyle == null)
            {
                return NotFound();
            }
            db.Delete(id);
            db.Save();
            return Ok(foodStyle);
        }
    }
}
