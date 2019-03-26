using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EasyEat.Models;
using EasyEat.Repositories;
using Microsoft.AspNetCore.Authorization;
using EasyEat.BusinessLogic;

namespace EasyEat.Controllers
{
    /*[Authorize]*/
    [Produces("application/json")]
    [Route("api/MealTime")]
    public class MealTimeController : Controller
    {
        IRepository<MealTime> db;

        public MealTimeController()
        {
            db = new MealTimeRepository();
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<MealTime> Get()
        {
            return db.GetEntityList();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            MealTime mealTime = db.GetEntity(id);
            if (mealTime == null)
                return NotFound();
            return new ObjectResult(mealTime);
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Create([FromBody]MealTime mealTime)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            db.Create(mealTime);
            db.Save();
            return Ok(mealTime);
        }

        // PUT api/<controller>
        [HttpPut]
        public IActionResult Update([FromBody]MealTime mealTime)
        {
            if (mealTime == null)
            {
                return BadRequest();
            }
            db.Update(mealTime);
            db.Save();
            return Ok(mealTime);
        }

        // DELETE api/<controller>/5
        
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            MealTime mealTime = db.GetEntity(id);
            if (mealTime == null)
            {
                return NotFound();
            }
            db.Delete(id);
            db.Save();
            return Ok(mealTime);
        }
    }
}
