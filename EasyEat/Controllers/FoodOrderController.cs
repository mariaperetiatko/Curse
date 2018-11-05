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
    [Produces("application/json")]
    [Route("api/FoodOrder")]
    public class FoodOrderController : Controller
    {
        IRepository<FoodOrder> dbFoodOrder;
        IRepository<Customer> dbCustomer;


        public FoodOrderController()
        {
            dbFoodOrder = new FoodOrderRepository();
            dbCustomer = new CustomerRepository();
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<FoodOrder> Get()
        {
            return dbFoodOrder.GetEntityList();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            FoodOrder foodOrder = dbFoodOrder.GetEntity(id);
            if (foodOrder == null)
                return NotFound();
            return new ObjectResult(foodOrder);
        }

        // POST api/<controller>
        [Authorize(Roles = "Admin, Member")]
        [HttpPost]
        public IActionResult Create([FromBody]FoodOrder foodOrder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            foodOrder.TotalCost = MainLogic.GetTotalCost(foodOrder);
            dbFoodOrder.Create(foodOrder);
            dbFoodOrder.Save();
            return Ok(foodOrder);
        }

        // PUT api/<controller>
        [Authorize(Roles = "Admin, Member")]
        [HttpPut]
        public IActionResult Update([FromBody]FoodOrder foodOrder)
        {
            if (foodOrder == null)
            {
                return BadRequest();
            }
            foodOrder.TotalCost = MainLogic.GetTotalCost(foodOrder);
            dbFoodOrder.Update(foodOrder);
            dbFoodOrder.Save();
            return Ok(foodOrder);
        }

        // DELETE api/<controller>/5
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

        public IActionResult Pay(int id)
        {
            FoodOrder foodOrder = dbFoodOrder.GetEntity(id);
            if(foodOrder.TotalCost > foodOrder.Customer.Balance)
            {
                return new ObjectResult("You can not pay, your balance is less that total cost!");
            }
            int newBalance = MainLogic.RefreshBalance((int)foodOrder.Customer.Balance, -foodOrder.TotalCost);
            Customer customer = foodOrder.Customer;
            customer.Balance = newBalance;
            dbCustomer.Update(customer);
            dbCustomer.Save();
            dbFoodOrder.Delete(id);
            dbFoodOrder.Save();
            return new ObjectResult("Succesful pay!");

        }
    }
}