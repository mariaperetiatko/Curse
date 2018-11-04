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
        IRepository<SpecialProduct> db;

        public SpecialProductController()
        {
            this.db = new SpecialProductRepository();
        }

        [HttpPost("CreateAllowed")]
        public IActionResult CreateAllowed([FromBody]SpecialProduct specialProduct)
        {
            if (specialProduct == null)
            {
                return BadRequest();
            }
            specialProduct.Allowance = 1;
            db.Create(specialProduct);
            db.Save();
            return Ok(specialProduct);
        }

        [HttpPost("CreateNotAllowed")]
        public IActionResult CreateNotAllowed([FromBody]SpecialProduct specialProduct)
        {
            if (specialProduct == null)
            {
                return BadRequest();
            }
            specialProduct.Allowance = 0;
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

    }
}