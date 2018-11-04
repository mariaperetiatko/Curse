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
    [Produces("application/json")]
    [Route("api/CartPart")]
    public class CartPartController : Controller
    {
        IRepository<CartPart> db;

        public CartPartController()
        {
            db = new CartPartRepository();
        }

        // GET: api/<controller>
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IEnumerable<CartPart> Get()
        {
            return db.GetEntityList();
        }

        // GET api/<controller>/5
        [Authorize(Roles = "Admin, Member")]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            CartPart cartPart = db.GetEntity(id);
            if (cartPart == null)
                return NotFound();
            return new ObjectResult(cartPart);
        }

        // POST api/<controller>
        [Authorize(Roles = "Admin, Member")]
        [HttpPost]
        public IActionResult Create([FromBody]CartPart cartPart)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            db.Create(cartPart);
            db.Save();
            return Ok(cartPart);
        }
    }
}