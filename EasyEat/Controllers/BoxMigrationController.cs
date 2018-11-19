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
    [Produces("application/json")]
    [Route("api/BoxMigration")]
    public class BoxMigrationController : Controller
    {
        BoxMigrationRepository db;

        public BoxMigrationController()
        {
            db = new BoxMigrationRepository();
        }

        [Authorize(Roles = "Member, Admin")]
        [HttpGet]
        public IEnumerable<BoxMigration> Get()
        {
            string userJWTId = User.FindFirst("id")?.Value;
            Customer customer = db.GetCustomer(userJWTId);
            if (customer == null)
            {
                return db.GetEntityList();
            }
            List<BoxMigration> boxMigration = db.GetMigrationsByCustomer(customer.Id).ToList();
            for (int i = 0; i < boxMigration.Count(); i++)
                boxMigration[i].FoodOrder = null;
            return boxMigration.AsEnumerable();
        }

        // GET api/<controller>/5
        [Authorize(Roles = "Admin, Member")]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            BoxMigration boxMigration = db.GetEntity(id);
            if (boxMigration == null)
                return NotFound();
            return new ObjectResult(boxMigration);
        }

        // POST api/<controller>
        [HttpPost]
        [Authorize(Roles = "Admin, Member")]
        public IActionResult Create([FromBody]BoxMigration boxMigration)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            
            db.Create(boxMigration);
            db.Save();
            boxMigration.FoodOrder = null;
            return Ok(boxMigration);
        }

        // PUT api/<controller>
        [HttpPut]
        public IActionResult Update([FromBody]BoxMigration boxMigration)
        {
            if (boxMigration == null)
            {
                return BadRequest();
            }

            db.Update(boxMigration);

            db.Save();
            return Ok(boxMigration);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            BoxMigration boxMigration = db.GetEntity(id);
            if (boxMigration == null)
            {
                return NotFound();
            }
            db.Delete(id);
            db.Save();
            return Ok(boxMigration);
        }
    }
}