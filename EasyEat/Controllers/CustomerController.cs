using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EasyEat.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EasyEat
{
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        EatContext db;

        public CustomerController(EatContext context)
        {
            this.db = context;
        }

    
    // GET: api/<controller>
    [HttpGet]
        public IEnumerable<Customer> Get()
        {
            return db.Customer.ToList();
        }
        [HttpGet("{id}")]

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Customer customer = db.Customer.FirstOrDefault(x => x.Id == id);
            if (customer == null)
                return NotFound();
            return new ObjectResult(customer);
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Create([FromBody]Customer customer)
        {
            if (customer == null)
            {
                return BadRequest();
            }

            db.Customer.Add(customer);
            db.SaveChanges();
            return Ok(customer);
        }

        // PUT api/<controller>
        [HttpPut]
        public IActionResult Update([FromBody]Customer customer)
        {
            if (customer == null)
            {
                return BadRequest();
            }
            if (!db.Customer.Any(x => x.Id == customer.Id))
            {
                return NotFound();
            }

            db.Update(customer);
            db.SaveChanges();
            return Ok(customer);
        }
        
        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Customer customer = db.Customer.FirstOrDefault(x => x.Id == id);
            if (customer == null)
            {
                return NotFound();
            }
            customer.IsDeleted = 1;
            db.SaveChanges();
            return Ok(customer);
        }
    }
}
