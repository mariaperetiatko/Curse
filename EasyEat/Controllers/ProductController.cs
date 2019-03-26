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
    /*[Authorize]*/
    [Produces("application/json")]
    [Route("api/Product")]
    public class ProductController : Controller
    {
        IRepository<Product> db;

        public ProductController()
        {
            db = new ProductRepository();
        }

        // GET: api/<controller>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
        public IEnumerable<Product> Get()
        {
            return db.GetEntityList();
        }

        // GET api/<controller>/5
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Product product = db.GetEntity(id);
            if (product == null)
                return NotFound();
            return new ObjectResult(product);
        }

        // POST api/<controller>
        /*
         * [Authorize(Roles = "Admin, RestaurantOwner")]
         * */
        [HttpPost]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        public IActionResult Create([FromBody]Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            db.Create(product);
            db.Save();
            return Ok(product);
        }

        // PUT api/<controller>
        /*
        [Authorize(Roles = "Admin, RestaurantOwner")]
        */
        [HttpPut]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        public IActionResult Update([FromBody]Product product)
        {
            if (product == null)
            {
                return BadRequest();
            }

            db.Update(product);
            db.Save();
            return Ok(product);
        }

        // DELETE api/<controller>/5
        /*
        [Authorize(Roles = "Admin, RestaurantOwner")]
        */
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        public IActionResult Delete(int id)
        {
            Product product = db.GetEntity(id);
            if (product == null)
            {
                return NotFound();
            }
            db.Delete(id);
            db.Save();
            return Ok(product);
        }
    }
}
