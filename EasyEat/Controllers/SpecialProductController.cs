using System;
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
    public class SpecialProductController : Controller
    {
        IRepository<SpecialProduct> db;

        public SpecialProductController()
        {
            this.db = new SpecialProductRepository();
        }

        [HttpPost]
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

        [HttpPost]
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

    }
}