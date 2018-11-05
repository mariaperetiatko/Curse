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
        IRepository<DeliveryAddress> db;

        public DeliveryAddressController()
        {
            db = new DeliveryAddressRepository();
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<DeliveryAddress> Get()
        {
            return db.GetEntityList();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            DeliveryAddress deliveryAddress = db.GetEntity(id);
            if (deliveryAddress == null)
                return NotFound();
            return new ObjectResult(deliveryAddress);
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Create([FromBody]DeliveryAddress deliveryAddress)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            db.Create(deliveryAddress);
            db.Save();
            return Ok(deliveryAddress);
        }

        // PUT api/<controller>
        [HttpPut]
        public IActionResult Update([FromBody]DeliveryAddress deliveryAddress)
        {
            if (deliveryAddress == null)
            {
                return BadRequest();
            }

            db.Update(deliveryAddress);
            db.Save();
            return Ok(deliveryAddress);
        }

        // DELETE api/<controller>/5
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