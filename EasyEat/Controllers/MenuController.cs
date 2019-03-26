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
   /* [Authorize]*/
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
        [ProducesResponseType(typeof(IEnumerable<Menu>), StatusCodes.Status200OK)]
        public IEnumerable<Menu> Get()
        {
            return db.GetEntityList();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Menu), StatusCodes.Status200OK)]
        public IActionResult Get(int id)
        {
            Menu menu = db.GetEntity(id);
            if (menu == null)
                return NotFound();
            return new ObjectResult(menu);
        }

        // GET api/<controller>/5
        [HttpGet("GetMenuesByRestaurant/{id}")]
        [ProducesResponseType(typeof(IEnumerable<Menu>), StatusCodes.Status200OK)]
        public IEnumerable<Menu> GetMenuesByRestaurant(int id)
        {
            return db.GetMenuesByRestaurant(id);
        }

        // POST api/<controller>
        /*
         * [Authorize(Roles = "Admin, RestaurantOwner")]
         * */
        [HttpPost]
        [ProducesResponseType(typeof(Menu), StatusCodes.Status200OK)]
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
        /*
        [Authorize(Roles = "Admin, RestaurantOwner")]
        */
        [HttpPut]
        [ProducesResponseType(typeof(Menu), StatusCodes.Status200OK)]
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
        /*
        [Authorize(Roles = "Admin, RestaurantOwner")]
        */
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Menu), StatusCodes.Status200OK)]
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
