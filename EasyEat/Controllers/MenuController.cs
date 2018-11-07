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
    [Route("api/Menu")]
    public class MenuController : Controller
    {
        MenuRepository db;

        public MenuController()
        {
            this.db = new MenuRepository();
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<Menu> Get()
        {
            return db.GetEntityList();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Menu menu = db.GetEntity(id);
            if (menu == null)
                return NotFound();
            return new ObjectResult(menu);
        }

        // POST api/<controller>
        [Authorize(Roles = "Admin, RestaurantOwner")]
        [HttpPost]
        public IActionResult Create([FromBody]Menu menu)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            db.Create(menu);
            db.Save();
            return Ok(menu);
        }

        // PUT api/<controller>
        [Authorize(Roles = "Admin, RestaurantOwner")]
        [HttpPut]
        public IActionResult Update([FromBody]Menu menu)
        {
            if (menu == null)
            {
                return BadRequest();
            }

            db.Update(menu);
            db.Save();
            return Ok(menu);
        }

        // DELETE api/<controller>/5
        [Authorize(Roles = "Admin, RestaurantOwner")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Menu menu = db.GetEntity(id);
            if (menu == null)
            {
                return NotFound();
            }
            db.Delete(id);
            db.Save();
            return Ok(menu);
        }
    }
}
