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
        CartRepository db;
        MainLogic ml;
        CustomerRepository dbCustomer;


        public CartController()
        {
            db = new CartRepository();
            ml = new MainLogic();
            dbCustomer = new CustomerRepository();

        }

        // GET: api/<controller>
        [ProducesResponseType(typeof(IEnumerable<Cart>), StatusCodes.Status200OK)]
        /*         
        [Authorize(Roles = "Admin, Member")]
        */
        [HttpGet("Get")]
        public IEnumerable<Cart> Get()
        {
            return db.GetEntityList();
        }

        [ProducesResponseType(typeof(Cart), StatusCodes.Status200OK)]
        /*
        [Authorize(Roles = "Member")]
        */
        [HttpGet("GetCart")]        
        
        public IActionResult GetCart()
        {
            string userJWTId = User.FindFirst("id")?.Value;
            Customer customer = db.GetCustomer(userJWTId);
            if (customer == null)
            {
                return NotFound();
            }
            Cart cart = db.GetEntity(customer.Id);
            cart.Customer = null;
            return new ObjectResult(cart);
        }

        // GET api/<controller>/5
        [ProducesResponseType(typeof(Cart), StatusCodes.Status200OK)]
        /*
        [Authorize(Roles = "Admin, Member")]
        */
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
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        /*
        [Authorize(Roles = "Admin, Member")]
        */
        [HttpPut]
        public IActionResult Update([FromBody]Cart cart)
        {
            if (cart == null)
            {
                return BadRequest();
            }
            string userJWTId = User.FindFirst("id")?.Value;
            Customer customer = db.GetCustomer(userJWTId);
            if (customer != null)
            {
                cart.CustomerId = customer.Id;
            }
            string result = "";
            Customer appropriateCustomer = dbCustomer.GetWholeEntity(cart.CustomerId);
            // Cart savedCart = db.GetWholeEntity(cart.CustomerId);
            cart.TotalCaloricValue = ml.GetTotalCaloricValue(cart);
            if (appropriateCustomer.CaloricGoal != null)
            {
                int allowedCaloricValue = ml.GetCaloricValue((int)appropriateCustomer.CaloricGoal, 
                    cart.MealTimeId);

                if (allowedCaloricValue < cart.TotalCaloricValue)
                    result += "Too much calories for this mealtime! Should be " + allowedCaloricValue.ToString();
            }
            db.Update(cart);
            db.Save();
            cart.Customer = null;
            cart.CartPart = null;
            cart.Address = null;
            cart.MealTime = null;
            result += " Cart is apdated!";
            return new ObjectResult(result);
        }

        // DELETE api/<controller>/5
        [ProducesResponseType(typeof(Cart), StatusCodes.Status200OK)]
        /*
        [Authorize(Roles = "Admin, Member")]
        */
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            string userJWTId = User.FindFirst("id")?.Value;
            Customer customer = db.GetCustomer(userJWTId);
            if (customer != null)
            {
                id = customer.Id;
            }
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