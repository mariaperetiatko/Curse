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
        IRepository<Customer> db;

        public CustomerController()
        {
            db = new CustomerRepository();
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
