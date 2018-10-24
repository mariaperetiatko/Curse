﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EasyEat.Models;
using EasyEat.Repositories;


namespace EasyEat.Controllers
{
    [Produces("application/json")]
    [Route("api/Product")]
    public class ProductController : Controller
    {
        IRepository<Product> db;

        public ProductController()
        {
            db = new ProductRepository();
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return db.GetEntityList();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Product product = db.GetEntity(id);
            if (product == null)
                return NotFound();
            return new ObjectResult(product);
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            db.Create(product);
            db.Save();
            return Ok(product);
        }

        // PUT api/<controller>
        [HttpPut]
        public IActionResult Update([FromBody]Product product)
        {
            if (product == null)
            {
                return BadRequest();
            }

            db.Update(product);
            db.Save();
            return Ok(product);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Product product = db.GetEntity(id);
            if (product == null)
            {
                return NotFound();
            }
            db.Delete(id);
            db.Save();
            return Ok(product);
        }
    }
}