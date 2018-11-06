using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EasyEat.Models;
using EasyEat.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace EasyEat.Controllers
{
    [Authorize(Roles = "Admin, Member")]
    [Produces("application/json")]
    [Route("api/SpecialProduct")]
    public class SpecialProductController : Controller
    {
        SpecialProductRepository db;

        public SpecialProductController()
        {
            this.db = new SpecialProductRepository();
        }

        // GET: api/<controller>
        [Authorize(Roles = "Admin, Member")]
        [HttpGet]
        public IEnumerable<SpecialProduct> Get()
        {
            string userJWTId = User.FindFirst("id")?.Value;
            Customer customer = db.GetCustomer(userJWTId);
            if (customer == null)
            {
                return db.GetEntityList();
            }
            List<SpecialProduct> specialProducts = db.GetSpecialProductByCustomer(customer.Id).ToList();
            for (int i = 0; i < specialProducts.Count(); i++)
                specialProducts[i].Customer = null;
            return specialProducts.AsEnumerable();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        [ActionName("Get")]
        public IActionResult Get([FromQuery]SpecialProductKey id)
        {
            SpecialProduct specialProduct = db.GetEntity(id);
            if (specialProduct == null)
                return NotFound();
            return new ObjectResult(specialProduct);
        }

        [HttpPost("CreateAllowed")]
        public IActionResult CreateAllowed([FromBody]SpecialProduct specialProduct)
        {
            if (specialProduct == null)
            {
                return BadRequest();
            }
            string userJWTId = User.FindFirst("id")?.Value;
            Customer customer = db.GetCustomer(userJWTId);
            if (customer != null)
            {
                specialProduct.CustomerId = customer.Id;
            }
           
            specialProduct.Allowance = 1;
            SpecialProductKey sk = new SpecialProductKey();
            sk.ProductId = specialProduct.ProductId;
            sk.CustomerId = specialProduct.CustomerId;
            SpecialProduct testSpecialProduct = db.GetEntity(sk);

            if (testSpecialProduct == null)
                db.Create(specialProduct);
     
            db.Save();
            specialProduct.Customer = null;
            return Ok(specialProduct);
        }

        [HttpPost("CreateNotAllowed")]
        public IActionResult CreateNotAllowed([FromBody]SpecialProduct specialProduct)
        {
            if (specialProduct == null)
            {
                return BadRequest();
            }
            string userJWTId = User.FindFirst("id")?.Value;
            Customer customer = db.GetCustomer(userJWTId);
            if (customer != null)
            {
                specialProduct.CustomerId = customer.Id;
            }
            specialProduct.Allowance = 0;
            SpecialProductKey sk = new SpecialProductKey();
            sk.ProductId = specialProduct.ProductId;
            sk.CustomerId = specialProduct.CustomerId;
            SpecialProduct testSpecialProduct = db.GetEntity(sk);

            if (testSpecialProduct == null)
                db.Create(specialProduct);
            db.Save();
            return Ok(specialProduct);
        }

        [HttpPut]
        public IActionResult Change([FromBody]SpecialProduct specialProduct)
        {
            if (specialProduct == null)
            {
                return BadRequest();
            }
            db.Update(specialProduct);
            db.Save();
            return Ok(specialProduct);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete([FromQuery]SpecialProductKey id)
        {
            string userJWTId = User.FindFirst("id")?.Value;
            Customer customer = db.GetCustomer(userJWTId);
            if (customer != null)
            {
                id.CustomerId = customer.Id;
            }
            SpecialProduct specialProduct = db.GetEntity(id);
            if (specialProduct == null)
            {
                return NotFound();
            }
            db.Delete(id);
            db.Save();
            return Ok(specialProduct);
        }

    }
}