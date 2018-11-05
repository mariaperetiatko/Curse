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
        IRepository<FavouriteDish> db;

        public FavouriteDishController()
        {
            db = new FavouriteDishRepository();
        }

        // GET: api/<controller>
        [Authorize(Roles = "Admin, Member")]
        [HttpGet]
        public IEnumerable<FavouriteDish> Get()
        {
            return db.GetEntityList();
        }

        // GET api/<controller>/5
        [Authorize(Roles = "Admin, Member")]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            FavouriteDish dish = db.GetEntity(id);
            if (dish == null)
                return NotFound();
            return new ObjectResult(dish);
        }

        // POST api/<controller>
        [Authorize(Roles = "Admin, Member")]
        [HttpPost]
        public IActionResult Create([FromBody]FavouriteDish dish)
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
        [Authorize(Roles = "Admin, Member")]
        [HttpPut]
        public IActionResult Update([FromBody]FavouriteDish dish)
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
        [Authorize(Roles = "Admin, Member")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            FavouriteDish dish = db.GetEntity(id);
            if (dish == null)
            {
                return NotFound();
            }
            db.Delete(id);
            db.Save();
            return Ok(dish);
        }
    }
}