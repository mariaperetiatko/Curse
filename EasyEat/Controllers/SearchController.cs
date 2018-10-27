using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EasyEat.Models;
using EasyEat.Repositories;
using EasyEat.BusinessLogic;

namespace EasyEat.Controllers
{
    [Produces("application/json")]
    [Route("api/Search")]
    public class SearchController : Controller
    {
        IRepository<Customer> CustomerDb;

        IRepository<Product> ProductDb;

        IRepository<Restaurant> RestaurantDb;

        IRepository<DeliveryAddress> DeliveryAddressDb;

        public SearchController()
        {
            CustomerDb = new CustomerRepository();
            ProductDb = new ProductRepository();
            RestaurantDb = new RestaurantRepository();
            DeliveryAddressDb = new DeliveryAddressRepository();
        }

        // GET api/<controller>/5
        [HttpGet("{customerId}, {radius}, {addressId}")]
        public IActionResult FindRestaurants(int customerId, int radius, int addressId)
        {
            Customer customer = CustomerDb.GetEntity(customerId);
            DeliveryAddress deliveryAddress = DeliveryAddressDb.GetEntity(addressId);
            List<Restaurant> allRestaurants = RestaurantDb.GetEntityList().ToList();
            List<Product> products = ProductDb.GetEntityList().ToList();

            if (customer == null || deliveryAddress == null || allRestaurants == null
                || products == null)
            {
                return NotFound();
            }

            List<Restaurant> restaurantsInRadius = MainLogic.FindInRadius(deliveryAddress.Xcoordinate,
                deliveryAddress.Ycoordinate, allRestaurants, radius);

            List<Restaurant> appropriateRestaurants = MainLogic.FindAppropriateRestaurants(customer,
                restaurantsInRadius, products);
            
            return new ObjectResult(appropriateRestaurants);
        }
    }
}