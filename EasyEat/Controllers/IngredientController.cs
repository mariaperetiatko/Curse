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
    [Produces("application/json")]
    [Route("api/Ingredient")]
    public class IngredientController : Controller
    {
        IngredientRepository db;
        SpecialProductRepository dbSpecialProduct;
        public IngredientController()
        {
            db = new IngredientRepository();
            this.dbSpecialProduct = new SpecialProductRepository();

        }

        // GET: api/<controller>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Ingredient>), StatusCodes.Status200OK)]
        public IEnumerable<Ingredient> Get()
        {
            return db.GetEntityList();
        }

        // GET: api/<controller>
        [HttpGet("GetIngredientByDish{dishId}")]
        [ProducesResponseType(typeof(IEnumerable<Ingredient>), StatusCodes.Status200OK)]
        public IEnumerable<Ingredient> GetIngredientByDish(int dishId)
        {
            return db.GetIngredientsByDish(dishId);
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        [ActionName("Get")]
        [ProducesResponseType(typeof(Ingredient), StatusCodes.Status200OK)]
        public IActionResult Get([FromQuery]IngredientKey id)
        {
            Ingredient ingredient = db.GetEntity(id);
            if (ingredient == null)
                return NotFound();
            return new ObjectResult(ingredient);
        }

        // POST api/<controller>
        [Authorize(Roles = "Admin, RestaurantOwner")]
        [HttpPost]
        [ProducesResponseType(typeof(Ingredient), StatusCodes.Status200OK)]
        public IActionResult Create([FromBody]Ingredient ingredient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            db.Create(ingredient);
            db.Save();
            return Ok(ingredient);
        }

        // PUT api/<controller>
        [Authorize(Roles = "Admin, RestaurantOwner")]
        [HttpPut]
        [ProducesResponseType(typeof(Ingredient), StatusCodes.Status200OK)]
        public IActionResult Update([FromBody]Ingredient ingredient)
        {
            if (ingredient == null)
            {
                return BadRequest();
            }

            db.Update(ingredient);
            db.Save();
            return Ok(ingredient);
        }

       

        // DELETE api/<controller>/5
        [Authorize(Roles = "Admin, RestaurantOwner")]
        [HttpDelete("{id}")]
        [ActionName("Delete")]
        [ProducesResponseType(typeof(Ingredient), StatusCodes.Status200OK)]
        public IActionResult Delete([FromQuery]IngredientKey id)
        {
            Ingredient ingredient = db.GetEntity(id);
            if (ingredient == null)
            {
                return NotFound();
            }
            db.Delete(id);
            db.Save();
            return Ok(ingredient);
        }
    }
}