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
        SpecialProductRepository dbProduct;


        public FoodStyleController()
        {
            db = new FoodStyleRepository();
            dbProduct = new SpecialProductRepository();
        }

        // GET: api/<controller>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<FoodStyle>), StatusCodes.Status200OK)]
        public IEnumerable<FoodStyle> Get()
        {
            return db.GetEntityList();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(FoodStyle), StatusCodes.Status200OK)]
        public IActionResult Get(int id)
        {
            FoodStyle foodStyle = db.GetEntity(id);
            if (foodStyle == null)
                return NotFound();
            return new ObjectResult(foodStyle);
        }

        // GET api/<controller>/5
        [HttpGet("GetByCustomer")]
        [ProducesResponseType(typeof(FoodStyle), StatusCodes.Status200OK)]
        public IActionResult GetByCustomer()
        {
            string userJWTId = User.FindFirst("id")?.Value;
            Customer customer = dbProduct.GetCustomer(userJWTId);
            FoodStyle foodStyle = db.GetEntity(customer.FoodStyleId);
            if (foodStyle == null)
                return NotFound();
            return new ObjectResult(foodStyle);
        }

        // POST api/<controller>
        [Authorize(Roles = "Admin, RestaurantOwner")]
        [ProducesResponseType(typeof(FoodStyle), StatusCodes.Status200OK)]
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
        [ProducesResponseType(typeof(FoodStyle), StatusCodes.Status200OK)]
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
        [ProducesResponseType(typeof(FoodStyle), StatusCodes.Status200OK)]
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
