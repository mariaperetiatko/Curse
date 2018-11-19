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
        ProductRepository productRepository;

        public SpecialProductController()
        {
            this.db = new SpecialProductRepository();
            this.productRepository = new ProductRepository();
        }

        // GET: api/<controller>
        [Authorize(Roles = "Admin, Member")]
        [ProducesResponseType(typeof(IEnumerable<SpecialProduct>), StatusCodes.Status200OK)]
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


        // GET: api/<controller>
        [Authorize(Roles = "Admin, Member")]
        [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
        [HttpGet("NotSpecialProducts")]
        public IEnumerable<Product> NotSpecialProducts()
        {
            string userJWTId = User.FindFirst("id")?.Value;
            Customer customer = db.GetCustomer(userJWTId);
            if (customer == null)
            {
                return productRepository.GetEntityList();
            }
            List<int> specialProductIds = db.GetSpecialProductByCustomer(customer.Id)
                .Select(x => x.ProductId).ToList();

            List<int> productIds = productRepository.GetEntityList()
                .Select(x => x.Id).ToList();
            List<Product> notSpecialProducts = new List<Product>();
            for (int i = 0; i < productIds.Count(); i++)
            {
                if (!specialProductIds.Contains(productIds[i]))
                {
                    notSpecialProducts.Add(productRepository.GetEntity(productIds[i]));
                    notSpecialProducts.Last().FoodStyleProduct = null;
                    notSpecialProducts.Last().Ingredient = null;
                    notSpecialProducts.Last().SpecialProduct = null;
                }
            }
            return notSpecialProducts.AsEnumerable();
        }

        // GET: api/<controller>
        [Authorize(Roles = "Member")]
        [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
        [HttpGet("AllowedProductBySpecial")]
        public IEnumerable<Product> AllowedProductBySpecial()
        {
            string userJWTId = User.FindFirst("id")?.Value;
            Customer customer = db.GetCustomer(userJWTId);
            List<SpecialProduct> specialProducts = db.GetSpecialProductByCustomer(customer.Id)
                .Where(x => x.Allowance == 1).ToList();
            List<Product> products = new List<Product>();
            for (int i = 0; i < specialProducts.Count(); i++)
            {
                products.Add(productRepository.GetEntity(specialProducts[i].ProductId));
                products[i].Ingredient = null;
                products[i].SpecialProduct = null;
                products[i].FoodStyleProduct = null;
            }
            return products.AsEnumerable();
        }

        // GET: api/<controller>
        [Authorize(Roles = "Member")]
        [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
        [HttpGet("NotAllowedProductBySpecial")]
        public IEnumerable<Product> NotAllowedProductBySpecial()
        {
            string userJWTId = User.FindFirst("id")?.Value;
            Customer customer = db.GetCustomer(userJWTId);
            List<SpecialProduct> specialProducts = db.GetSpecialProductByCustomer(customer.Id)
                .Where(x => x.Allowance == 0).ToList();
            List<Product> products = new List<Product>();
            for (int i = 0; i < specialProducts.Count(); i++)
            {
                products.Add(productRepository.GetEntity(specialProducts[i].ProductId));
                products[i].Ingredient = null;
                products[i].SpecialProduct = null;
                products[i].FoodStyleProduct = null;
            }
            return products.AsEnumerable();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(SpecialProduct), StatusCodes.Status200OK)]
        [ActionName("Get")]
        public IActionResult Get([FromQuery]SpecialProductKey id)
        {
            SpecialProduct specialProduct = db.GetEntity(id);
            if (specialProduct == null)
                return NotFound();
            return new ObjectResult(specialProduct);
        }

        [HttpPost("CreateAllowed")]
        [ProducesResponseType(typeof(SpecialProduct), StatusCodes.Status200OK)]
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
        [ProducesResponseType(typeof(SpecialProduct), StatusCodes.Status200OK)]
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
        [ProducesResponseType(typeof(SpecialProduct), StatusCodes.Status200OK)]
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
        [ActionName("Delete")]
        [ProducesResponseType(typeof(SpecialProduct), StatusCodes.Status200OK)]
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