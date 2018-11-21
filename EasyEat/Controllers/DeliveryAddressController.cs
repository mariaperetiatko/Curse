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

namespace EasyEat.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/DeliveryAddress")]
    public class DeliveryAddressController : Controller
    {
        DeliveryAddressRepository db;

        public DeliveryAddressController()
        {
            db = new DeliveryAddressRepository();
        }       

        [Authorize(Roles = "Member, Admin")]
        [ProducesResponseType(typeof(IEnumerable<DeliveryAddress>), StatusCodes.Status200OK)]
        [HttpGet]
        public IEnumerable<DeliveryAddress> Get()
        {
            string userJWTId = User.FindFirst("id")?.Value;
            Customer customer = db.GetCustomer(userJWTId);
            if (customer == null)
            {
                return db.GetEntityList();
            }
            List<DeliveryAddress> addresses = db.GetAddressByCustomer(customer.Id).ToList();
            for (int i = 0; i < addresses.Count(); i++)
                addresses[i].Customer = null;
            return addresses.AsEnumerable();
        }

        // GET api/<controller>/5
        [ProducesResponseType(typeof(DeliveryAddress), StatusCodes.Status200OK)]
        [Authorize(Roles = "Admin, Member")]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            DeliveryAddress deliveryAddress = db.GetEntity(id);
            if (deliveryAddress == null)
                return NotFound();
            return new ObjectResult(deliveryAddress);
        }

        // POST api/<controller>
        [ProducesResponseType(typeof(DeliveryAddress), StatusCodes.Status200OK)]
        [HttpPost]
        [Authorize(Roles = "Admin, Member")]
        public IActionResult Create([FromBody]DeliveryAddress deliveryAddress)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            string userJWTId = User.FindFirst("id")?.Value;
            Customer customer = db.GetCustomer(userJWTId);
            if (customer != null)
            {
                deliveryAddress.CustomerId = customer.Id;
            }

            db.Create(deliveryAddress);
            db.Save();
            deliveryAddress.Customer = null;
            return Ok(deliveryAddress);
        }

        // PUT api/<controller>
        [ProducesResponseType(typeof(DeliveryAddress), StatusCodes.Status200OK)]
        [HttpPut]
        public IActionResult Update([FromBody]DeliveryAddress deliveryAddress)
        {
            if (deliveryAddress == null)
            {
                return BadRequest();
            }

            string userJWTId = User.FindFirst("id")?.Value;
            Customer customer = db.GetCustomer(userJWTId);
            if (customer != null)
            {
                deliveryAddress.CustomerId = customer.Id;
            }
            deliveryAddress = null;
            db.Update(deliveryAddress);

            db.Save();
            return Ok(deliveryAddress);
        }

        // DELETE api/<controller>/5
        [ProducesResponseType(typeof(DeliveryAddress), StatusCodes.Status200OK)]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            DeliveryAddress deliveryAddress = db.GetEntity(id);
            if (deliveryAddress == null)
            {
                return NotFound();
            }
            db.Delete(id);
            db.Save();
            return Ok(deliveryAddress);
        }
    }
}