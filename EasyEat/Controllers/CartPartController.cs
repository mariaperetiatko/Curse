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
    [Route("api/CartPart")]
    public class CartPartController : Controller
    {
        CartPartRepository db;
        CartRepository cr;
        MainLogic ml;

        public CartPartController()
        {
            db = new CartPartRepository();
            ml = new MainLogic();
            cr = new CartRepository();
        }

        // GET: api/<controller>
        [ProducesResponseType(typeof(IEnumerable<CartPart>), StatusCodes.Status200OK)]
        [Authorize(Roles = "Admin, Member")]
        [HttpGet]
        public IEnumerable<CartPart> Get()
        {
            string userJWTId = User.FindFirst("id")?.Value;
            Customer customer = db.GetCustomer(userJWTId);
            if (customer == null)
            {
                return db.GetEntityList();
            }
            List<CartPart> cartPart = db.GetCartPartByCustomer(customer.Id).ToList();
            for (int i = 0; i < cartPart.Count(); i++)
                cartPart[i].Cart = null;
            return cartPart.AsEnumerable();
        }

        // GET api/<controller>/5
        [ProducesResponseType(typeof(CartPart), StatusCodes.Status200OK)]
        [Authorize(Roles = "Admin, Member")]
        [HttpGet("Get/{id}")]
        public IActionResult Get([FromQuery]CartPartKey id)
        {
            CartPart cartPart = db.GetEntity(id);
            if (cartPart == null)
                return NotFound();
            return new ObjectResult(cartPart);
        }

        // POST api/<controller>
        [ProducesResponseType(typeof(CartPart), StatusCodes.Status200OK)]
        [Authorize(Roles = "Admin, Member")]
        [HttpPost]
        public IActionResult Create([FromBody]CartPart cartPart)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            string userJWTId = User.FindFirst("id")?.Value;
            Customer customer = db.GetCustomer(userJWTId);
            if (customer != null)
            {
                cartPart.CartId = customer.Id;
            }
            
            db.Create(cartPart);
            db.Save();
            Cart cart = cr.GetEntity(cartPart.CartId);
            cart.TotalCaloricValue = ml.GetTotalCaloricValue(cart);
            cr.Update(cart);
            cr.Save();
            return Ok(cartPart);
        }

        // PUT api/<controller>
        [ProducesResponseType(typeof(CartPart), StatusCodes.Status200OK)]
        [Authorize(Roles = "Admin, Member")]
        [HttpPut]
        public IActionResult Undate([FromBody]CartPart cartPart)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            string userJWTId = User.FindFirst("id")?.Value;
            Customer customer = db.GetCustomer(userJWTId);
            if (customer != null)
            {
                cartPart.CartId = customer.Id;
            }
            db.Update(cartPart);
            db.Save();
            Cart cart = cr.GetEntity(cartPart.CartId);
            cart.TotalCaloricValue = ml.GetTotalCaloricValue(cart);
            cr.Update(cart);
            cr.Save();
            return Ok(cartPart);
        }

        // DELETE api/<controller>/5
        [ProducesResponseType(typeof(CartPart), StatusCodes.Status200OK)]
        [Authorize(Roles = "Admin, Member")]
        [HttpDelete("{id}")]
        [ActionName("Delete")]
        public IActionResult Delete([FromQuery]CartPartKey id)
        {
            string userJWTId = User.FindFirst("id")?.Value;
            Customer customer = db.GetCustomer(userJWTId);
            if (customer != null)
            {
                id.CartId = customer.Id;
            }
            CartPart cartPart = db.GetEntity(id);
            if (cartPart == null)
            {
                return NotFound();
            }
            db.Delete(id);
            db.Save();
            return Ok(cartPart);
        }

        [ProducesResponseType(typeof(IEnumerable<CartPart>), StatusCodes.Status200OK)]
        [Authorize(Roles = "Admin, Member")]
        [HttpGet("GetCartPartByCustomer/{id}")]
        public IEnumerable<CartPart> GetCartPartByCustomer(int id)
        {
            return db.GetCartPartByCustomer(id);
        }
    }
}