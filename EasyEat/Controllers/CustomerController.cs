using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EasyEat.Models;
using EasyEat.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;


namespace EasyEat
{
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        CustomerRepository db;
        FoodStyleRepository fr;
        SpecialProductRepository sr;
        FoodOrderRepository fo;

        public CustomerController()
        {
            db = new CustomerRepository();
            fr = new FoodStyleRepository();
            sr = new SpecialProductRepository();
            fo = new FoodOrderRepository();
        }


        // GET: api/<controller>
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(IEnumerable<Customer>), StatusCodes.Status200OK)]
        [HttpGet]
        public IEnumerable<Customer> Get()
        {
            return db.GetEntityList();
        }

        [Authorize(Roles = "Member")]
        [HttpGet("GetCustomer")]
        [ProducesResponseType(typeof(Customer), StatusCodes.Status200OK)]
        public IActionResult GetCustomer()
        {
            string userJWTId = User.FindFirst("id")?.Value;
            Customer customer = db.GetCustomer(userJWTId);
            if (customer == null)
            {
                return NotFound();
            }
            return new ObjectResult(customer);
        }

        [HttpGet("GetCustomerByToken")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public int GetCustomerByToken()
        {
            string userJWTId = User.FindFirst("id")?.Value;
            return db.GetCustomer(userJWTId).Id;
        }

        // GET api/<controller>/5
        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Customer), StatusCodes.Status200OK)]
        public IActionResult Get(int id)
        {
            Customer customer = db.GetEntity(id);
            if (customer == null)
                return NotFound();
            return new ObjectResult(customer);
        }

        // POST api/<controller>
        [HttpPost]
        [ProducesResponseType(typeof(Customer), StatusCodes.Status200OK)]
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
        [ProducesResponseType(typeof(Customer), StatusCodes.Status200OK)]
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

        [HttpGet("ChangeCaloricGoal/{customerId}, {caloricValue}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public IActionResult ChangeCaloricGoal(int customerId, int caloricValue)
        {
            if (caloricValue <= 0)
                return new ObjectResult("caloricValue must be positive!");

            Customer customer = db.GetEntity(customerId);
      
            customer.CaloricGoal = caloricValue;
            db.Update(customer);
            db.Save();
            fo.Save();
            return new ObjectResult("Succesful operation!");
        }

        [HttpGet("ChangeFirstName/{customerId}, {firstName}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public IActionResult ChangeFirstName(int customerId, string firstName)
        {
            if (firstName == "")
                return new ObjectResult("caloricValue must be!");

            Customer customer = db.GetEntity(customerId);
            customer.FirstName = firstName;
            db.Update(customer);
            db.Save();
            fo.Save();
            return new ObjectResult("Succesful operation!");
        }

        [HttpGet("ChangeLastName/{customerId}, {lastName}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public IActionResult ChangeLastName(int customerId, string lastName)
        {
            if (lastName == "")
                return new ObjectResult("caloricValue must be!");

            Customer customer = db.GetEntity(customerId);
            customer.LastName = lastName;
            db.Update(customer);
            db.Save();
            fo.Save();
            return new ObjectResult("Succesful operation!");
        }

        [HttpGet("ChangePhoneNumber/{customerId}, {phoneNumber}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public IActionResult ChangePhoneNumber(int customerId, long phoneNumber)
        {
            if (phoneNumber <= 0)
                return new ObjectResult("phoneNumber must be positive!");

            Customer customer = db.GetEntity(customerId);
            customer.Phone = phoneNumber;
            db.Update(customer);
            db.Save();
            fo.Save();
            return new ObjectResult("Succesful operation!");
        }

        [HttpGet("ChangeFoodStyle/{customerId}, {foodStyleId}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public IActionResult ChangeFoodStyle(int customerId, int foodStyleId)
        {         

            Customer customer = db.GetWholeEntity(customerId);
            FoodStyle foodStyle = fr.GetWholeEntity(foodStyleId);
            customer.CaloricGoal = foodStyle.CaloricValue;
            customer.FoodStyleId = foodStyleId;
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
        
            db.Update(customer);
            db.Save();
            fo.Save();
            return new ObjectResult("Succesful operation!");
        }

        [HttpGet("DeleteCustomersFoodStyle/{customerId}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public IActionResult DeleteCustomersFoodStyle(int customerId)
        {
            Customer customer = db.GetEntity(customerId);
            customer.FoodStyleId = null;
            customer.FoodStyle = null;

            db.Update(customer);
            db.Save();
            fo.Save();
            return new ObjectResult("Succesful operation!");
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
