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
        FoodOrderRepository foodOrderRepository;

        public BoxMigrationController()
        {
            db = new BoxMigrationRepository();
            foodOrderRepository = new FoodOrderRepository();
        }

        /*
        [Authorize(Roles = "Member, Admin")]
        */
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
        /*
        [Authorize(Roles = "Admin, Member")]
        */
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            BoxMigration boxMigration = db.GetEntity(id);
            if (boxMigration == null)
                return NotFound();
            return new ObjectResult(boxMigration);
        }

        [HttpGet("GetMapMarker")]
        [ProducesResponseType(typeof(BoxMigration), StatusCodes.Status200OK)]
        public IActionResult GetMapMarker()
        {
            BoxMigration boxMigration = db.GetEntityList().Last();
            if (boxMigration == null)
                return NotFound();
            return new ObjectResult(boxMigration);
        }

        // POST api/<controller>
        [HttpPost]
        [ProducesResponseType(typeof(BoxMigration), StatusCodes.Status200OK)]
        /*
        [Authorize(Roles = "Admin, Member")]
        */
        public IActionResult Create([FromBody]BoxMigration boxMigration)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            IEnumerable<FoodOrder> fo = foodOrderRepository.GetEntityList();
            boxMigration.FoodOrderId = fo.Last().Id;
            db.Create(boxMigration);
            db.Save();
            boxMigration.FoodOrder = null;
            return Ok(boxMigration);
        }

        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [HttpGet("CreateByCoordinates/{coordinates}")]
        public IActionResult CreateByCoordinates(string coordinates)
        {
            if (coordinates != null)
            {
                string[] coords = coordinates.Split(new char[] { ':' });
                if (coords.Length == 7)
                {
                    BoxMigration boxMigration = new BoxMigration();
                    boxMigration.Temperature = coords[2];
                    boxMigration.Latitude = double.Parse(coords[4]);
                    boxMigration.Longtitude = double.Parse(coords[6]);
                    boxMigration.Moment = DateTime.Now;
                    IEnumerable<FoodOrder> fo = foodOrderRepository.GetEntityList();
                    boxMigration.FoodOrderId = fo.Last().Id;
                    db.Create(boxMigration);
                    db.Save();
                    boxMigration.FoodOrder = null;
                }
            }
            return Ok("Created");
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