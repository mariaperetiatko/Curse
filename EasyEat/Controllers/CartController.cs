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
    [Route("api/Cart")]
    public class CartController : Controller
    {
        IRepository<Cart> db;

        public CartController()
        {
            db = new CartRepository();
        }

        // GET: api/<controller>
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IEnumerable<Cart> Get()
        {
            return db.GetEntityList();
        }

        // GET api/<controller>/5
        [Authorize(Roles = "Admin, Member")]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Cart cart = db.GetEntity(id);
            if (cart == null)
                return NotFound();
            return new ObjectResult(cart);
        }

        /*// POST api/<controller>
        [Authorize(Roles = "Admin, Member")]
        [HttpPost]
        public IActionResult Create([FromBody]Cart cart)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            db.Create(cart);
            db.Save();
            return Ok(cart);
        }*/

        // PUT api/<controller>
        [Authorize(Roles = "Admin, Member")]
        [HttpPut]
        public IActionResult Update([FromBody]Cart cart)
        {
            if (cart == null)
            {
                return BadRequest();
            }
            cart.TotalCaloricValue = MainLogic.GetTotalCaloricValue(cart);
            db.Update(cart);
            db.Save();
            return Ok(cart);
        }

        // DELETE api/<controller>/5
        [Authorize(Roles = "Admin, Member")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Cart cart = db.GetEntity(id);
            if (cart == null)
            {
                return NotFound();
            }
            db.Delete(id);
            db.Save();
            return Ok(cart);
        }
    }
}