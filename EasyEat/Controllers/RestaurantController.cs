using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EasyEat.Models;
using EasyEat.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Localization;
using System.Globalization;




namespace EasyEat.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/Restaurant")]
    public class RestaurantController : Controller
    {
        IRepository<Restaurant> db;
        private IStringLocalizer _localizer;


        public RestaurantController(IStringLocalizer localizer)
        {
            db = new RestaurantRepository();
            _localizer = localizer;
        }

        // GET: api/<controller>
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<Restaurant>), StatusCodes.Status200OK)]
        [HttpGet]
        public IEnumerable<Restaurant> Get()
        {
            List<Restaurant> restaurants = db.GetEntityList().ToList();
            foreach(Restaurant restaurant in restaurants)
            {
                restaurant.City = _localizer[restaurant.City];
                restaurant.Country = _localizer[restaurant.Country];
                restaurant.Streete = _localizer[restaurant.Streete];
            }
            return restaurants.AsEnumerable();
        }

        // GET api/<controller>/5
        [ProducesResponseType(typeof(Restaurant), StatusCodes.Status200OK)]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Restaurant restaurant = db.GetEntity(id);
            if (restaurant == null)
                return NotFound();
            restaurant.City = _localizer[restaurant.City];
            restaurant.Country = _localizer[restaurant.Country];
            restaurant.Streete = _localizer[restaurant.Streete];
            return new ObjectResult(restaurant);
        }

        // POST api/<controller>
        [ProducesResponseType(typeof(Restaurant), StatusCodes.Status200OK)]
        [Authorize(Roles = "Admin, RestaurantOwner")]
        [HttpPost]
        public IActionResult Create([FromBody]Restaurant restaurant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            this._localizer.WithCulture(new CultureInfo("en"));
            restaurant.Streete = _localizer[restaurant.Streete];
            restaurant.City = _localizer[restaurant.City];
            restaurant.Country = _localizer[restaurant.Country];

            db.Create(restaurant);
            db.Save();
            return Ok(restaurant);
        }

        // PUT api/<controller>
        [ProducesResponseType(typeof(Restaurant), StatusCodes.Status200OK)]
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
        [ProducesResponseType(typeof(Restaurant), StatusCodes.Status200OK)]
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
