using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EasyEat.Models;
using EasyEat.Repositories;
using Microsoft.AspNetCore.Authorization;


namespace EasyEat
{
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        CustomerRepository db;
        FoodStyleRepository fr;
        SpecialProductRepository sr;

        public CustomerController()
        {
            db = new CustomerRepository();
            fr = new FoodStyleRepository();
            sr = new SpecialProductRepository();
        }


        // GET: api/<controller>
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IEnumerable<Customer> Get()
        {
            return db.GetEntityList();
        }

        // GET api/<controller>/5
        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Customer customer = db.GetEntity(id);
            if (customer == null)
                return NotFound();
            return new ObjectResult(customer);
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            db.Create(customer);
            db.Save();
            return Ok(customer);
        }

        // PUT api/<controller>
        [Authorize(Roles = "Admin, Member")]
        [HttpPut]
        public IActionResult Update([FromBody]Customer customer)
        {
            if (customer == null)
            {
                return BadRequest();
            }
            string userJWTId = User.FindFirst("id")?.Value;
            Customer appropriateCustomer = db.GetCustomer(userJWTId);

            if(appropriateCustomer != null)
                customer.Id = appropriateCustomer.Id;
            
            if(customer.FoodStyleId != null)
            {
                FoodStyle foodStyle = fr.GetWholeEntity(customer.FoodStyleId);
                customer.CaloricGoal = foodStyle.CaloricValue;
                List<FoodStyleProduct> foodStyleProducts = foodStyle.FoodStyleProduct.ToList();
                List<SpecialProduct> specialProducts = customer.SpecialProduct.ToList();
                List<int> specialProductsIds = new List<int>();

                for (int i = 0; i < specialProducts.Count(); i++)
                    specialProductsIds.Add(specialProducts[i].ProductId);

                for (int i = 0; i < foodStyleProducts.Count(); i++)
                {
                    SpecialProduct newSpecialProduct = new SpecialProduct();
                    newSpecialProduct.CustomerId = customer.Id;
                    newSpecialProduct.ProductId = foodStyleProducts[i].ProductId;
                    newSpecialProduct.Allowance = 1;
                    if (specialProductsIds.Contains(foodStyleProducts[i].ProductId))
                        sr.Update(newSpecialProduct);
                    else
                        sr.Create(newSpecialProduct);
                    sr.Save();
                    }
                }
            
            db.Update(customer);
            db.Save();
            return Ok(customer);
        }

        // DELETE api/<controller>/5
        [Authorize(Roles = "Admin, Member")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Customer customer = db.GetEntity(id);
            if (customer == null)
            {
                return NotFound();
            }
            db.Delete(id);
            db.Save();
            return Ok(customer);
        }
    }
}
