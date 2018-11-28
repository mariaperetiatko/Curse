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
using EasyEat.BusinessLogic;


namespace EasyEat.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/FoodOrder")]
    public class FoodOrderController : Controller
    {
        FoodOrderRepository dbFoodOrder;
        IRepository<Customer> dbCustomer;
        MainLogic ml;


        public FoodOrderController()
        {
            dbFoodOrder = new FoodOrderRepository();
            dbCustomer = new CustomerRepository();
            ml = new MainLogic();

        }

        // GET: api/<controller>
        [ProducesResponseType(typeof(IEnumerable<FoodOrder>), StatusCodes.Status200OK)]
        [HttpGet]
        public IEnumerable<FoodOrder> Get()
        {
            string userJWTId = User.FindFirst("id")?.Value;
            Customer customer = dbFoodOrder.GetCustomer(userJWTId);
            if (customer == null)
            {
                return dbFoodOrder.GetEntityList();
            }
            List<FoodOrder> foodOrder = dbFoodOrder
                .GetFoodOrderByCustomer(customer.Id).ToList();
            for (int i = 0; i < foodOrder.Count(); i++)
                foodOrder[i].Customer = null;
            return foodOrder.AsEnumerable();
        }

        // GET api/<controller>/5
        [ProducesResponseType(typeof(FoodOrder), StatusCodes.Status200OK)]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            FoodOrder foodOrder = dbFoodOrder.GetEntity(id);
            if (foodOrder == null)
                return NotFound();
            return new ObjectResult(foodOrder);
        }

        // POST api/<controller>
        [ProducesResponseType(typeof(FoodOrder), StatusCodes.Status200OK)]
        [Authorize(Roles = "Admin, Member")]
        [HttpPost]
        public IActionResult Create([FromBody]FoodOrder foodOrder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            string userJWTId = User.FindFirst("id")?.Value;
            Customer customer = dbFoodOrder.GetCustomer(userJWTId);
            if (customer != null)
            {
                foodOrder.CustomerId = customer.Id;
            }
            //foodOrder.Customer = dbCustomer.GetEntity(foodOrder.CustomerId);
            Cart cart = dbFoodOrder.GetCart(foodOrder);
            foodOrder.Address = cart.Address;
            foodOrder.DeliveryDate = cart.DeliveryDate;
            foodOrder.TotalCost = ml.GetTotalCost(foodOrder);
            dbFoodOrder.Create(foodOrder);
            dbFoodOrder.Save();
            foodOrder.Customer = null;
            return Ok(foodOrder);
        }

        // PUT api/<controller>
        [ProducesResponseType(typeof(FoodOrder), StatusCodes.Status200OK)]
        [Authorize(Roles = "Admin, Member")]
        [HttpPut]
        public IActionResult Update([FromBody]FoodOrder foodOrder)
        {
            if (foodOrder == null)
            {
                return BadRequest();
            }
            string userJWTId = User.FindFirst("id")?.Value;
            Customer customer = dbFoodOrder.GetCustomer(userJWTId);
            if (customer != null)
            {
                foodOrder.CustomerId = customer.Id;
            }
            foodOrder.TotalCost = ml.GetTotalCost(foodOrder);
            dbFoodOrder.Update(foodOrder);
            dbFoodOrder.Save();
            return Ok(foodOrder);
        }

        // DELETE api/<controller>/5
        [ProducesResponseType(typeof(FoodOrder), StatusCodes.Status200OK)]
        [Authorize(Roles = "Admin, Member")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            FoodOrder foodOrder = dbFoodOrder.GetEntity(id);
            if (foodOrder == null)
            {
                return NotFound();
            }
            dbFoodOrder.Delete(id);
            dbFoodOrder.Save();
            return Ok(foodOrder);
        }

    
    }
}