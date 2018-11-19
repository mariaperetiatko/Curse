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
    [Route("api/FoodStyleProduct")]
    public class FoodStyleProductController : Controller
    {
        FoodStyleProductRepository db;
        SpecialProductRepository dbSpecialProduct;

        public FoodStyleProductController()
        {
            db = new FoodStyleProductRepository();
            this.dbSpecialProduct = new SpecialProductRepository();

        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<FoodStyleProduct> Get()
        {
            return db.GetEntityList();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        [ActionName("Get")]
        public IActionResult Get([FromQuery]FoodStyleProductKey id)
        {
            FoodStyleProduct foodStyleProduct = db.GetEntity(id);
            if (foodStyleProduct == null)
                return NotFound();
            return new ObjectResult(foodStyleProduct);
        }

        // POST api/<controller>
        [Authorize(Roles = "Admin, RestaurantOwner")]
        [HttpPost]
        public IActionResult Create([FromBody]FoodStyleProduct foodStyleProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            db.Create(foodStyleProduct);
            db.Save();
            return Ok(foodStyleProduct);
        }

        // PUT api/<controller>
        [Authorize(Roles = "Admin, RestaurantOwner")]
        [HttpPut]
        public IActionResult Update([FromBody]FoodStyleProduct foodStyleProduct)
        {
            if (foodStyleProduct == null)
            {
                return BadRequest();
            }

            db.Update(foodStyleProduct);
            db.Save();
            return Ok(foodStyleProduct);
        }

        // GET: api/<controller>
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
        [HttpGet("ProductByFoodStyle/{foodStyleId}")]
        public IEnumerable<Product> ProductByFoodStyle(int foodStyleId)
        {
            List<Product> products = db.GetProductsByFoodStyle(foodStyleId);
            for (int i = 0; i < products.Count(); i++)
            {
                products[i].Ingredient = null;
                products[i].SpecialProduct = null;
                products[i].FoodStyleProduct = null;
            }
            return products.AsEnumerable();
        }

        // DELETE api/<controller>/5
        [Authorize(Roles = "Admin, RestaurantOwner")]
        [HttpDelete("{id}")]
        [ActionName("Delete")]
        public IActionResult Delete([FromQuery]FoodStyleProductKey id)
        {
            FoodStyleProduct foodStyleProduct = db.GetEntity(id);
            if (foodStyleProduct == null)
            {
                return NotFound();
            }
            db.Delete(id);
            db.Save();
            return Ok(foodStyleProduct);
        }
    }
 }