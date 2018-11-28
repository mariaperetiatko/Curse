using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EasyEat.Models;
using EasyEat.Repositories;
using EasyEat.BusinessLogic;
using Microsoft.AspNetCore.Authorization;


namespace EasyEat.Controllers
{
    //[Authorize]
    [Produces("application/json")]
    [Route("api/Search")]
    public class SearchController : Controller
    {
        IRepository<Customer> CustomerDb;

        ProductRepository ProductDb;

        IRepository<Restaurant> RestaurantDb;

        IRepository<DeliveryAddress> DeliveryAddressDb;
        MainLogic ml;


        public SearchController()
        {
            CustomerDb = new CustomerRepository();
            ProductDb = new ProductRepository();
            RestaurantDb = new RestaurantRepository();
            DeliveryAddressDb = new DeliveryAddressRepository();
            ml = new MainLogic();

        }

        // GET api/<controller>/5
        [HttpGet("Appropriate/{customerId}, {radius}, {addressId}")]
        //[Route("Appropriate")]
        [ProducesResponseType(typeof(List<Restaurant>), StatusCodes.Status200OK)]

        public IActionResult FindRestaurantsByAppropriate(int customerId, int radius, int addressId)
        {
            Customer customer = CustomerDb.GetEntity(customerId);
            DeliveryAddress deliveryAddress = DeliveryAddressDb.GetEntity(addressId);
            List<Restaurant> allRestaurants = RestaurantDb.GetEntityList().ToList();
            List<Product> products = ProductDb.GetWholeEntityList().ToList();

            if (customer == null || deliveryAddress == null || allRestaurants == null
                || products == null)
            {
                return NotFound();
            }

            List<Restaurant> restaurantsInRadius = MainLogic.FindInRadius(deliveryAddress.Xcoordinate,
                deliveryAddress.Ycoordinate, allRestaurants, radius);

            List<Restaurant> appropriateRestaurants = ml.FindAppropriateRestaurants(customer,
                restaurantsInRadius, products);

            return new ObjectResult(appropriateRestaurants);
        }

        // GET api/<controller>/5
        [HttpGet("AppropriateRuntimePosition/{customerId}, {radius}, {Xcoordinate}, {Ycoordinate}")]
        //[Route("Appropriate")]
        [ProducesResponseType(typeof(List<Restaurant>), StatusCodes.Status200OK)]

        public IActionResult FindRestaurantsByAppropriateRuntimePosition(int customerId, int radius, 
            double Xcoordinate, double Ycoordinate)
        {
            Customer customer = CustomerDb.GetEntity(customerId);
            List<Restaurant> allRestaurants = RestaurantDb.GetEntityList().ToList();
            List<Product> products = ProductDb.GetWholeEntityList().ToList();

            if (customer == null || allRestaurants == null
                || products == null)
            {
                return NotFound();
            }

            List<Restaurant> restaurantsInRadius = MainLogic.FindInRadius(Xcoordinate,
                Ycoordinate, allRestaurants, radius);

            List<Restaurant> appropriateRestaurants = ml.FindAppropriateRestaurants(customer,
                restaurantsInRadius, products);

            return new ObjectResult(appropriateRestaurants);
        }

        // GET api/<controller>/5
        [ProducesResponseType(typeof(List<Restaurant>), StatusCodes.Status200OK)]
        [HttpGet("Favourite/{customerId}, {radius}, {addressId}")]
        //[Route("Favourite")]
        public IActionResult FindRestaurantsByFavourite(int customerId, int radius, int addressId)
        {
            Customer customer = CustomerDb.GetEntity(customerId);
            DeliveryAddress deliveryAddress = DeliveryAddressDb.GetEntity(addressId);
            List<Restaurant> allRestaurants = RestaurantDb.GetEntityList().ToList();

            if (customer == null || deliveryAddress == null || allRestaurants == null)
            {
                return NotFound();
            }

            List<Restaurant> restaurantsInRadius = MainLogic.FindInRadius(deliveryAddress.Xcoordinate,
                deliveryAddress.Ycoordinate, allRestaurants, radius);

            List<Restaurant> favouriteRestaurants = ml.FindByFavourites(customer,
                restaurantsInRadius);

            return new OkObjectResult(favouriteRestaurants);
        }

        // GET api/<controller>/5
        [ProducesResponseType(typeof(List<Restaurant>), StatusCodes.Status200OK)]
        [HttpGet("FavouriteRuntimePosition/{customerId}, {radius}, {Xcoordinate}, {Ycoordinate}")]
        //[Route("Favourite")]
        public IActionResult FindRestaurantsByFavouriteRuntimePosition(int customerId, int radius,
            double Xcoordinate, double Ycoordinate)
        {
            Customer customer = CustomerDb.GetEntity(customerId);
            List<Restaurant> allRestaurants = RestaurantDb.GetEntityList().ToList();

            if (customer == null || allRestaurants == null)
            {
                return NotFound();
            }

            List<Restaurant> restaurantsInRadius = MainLogic.FindInRadius(Xcoordinate,
                Ycoordinate, allRestaurants, radius);

            List<Restaurant> favouriteRestaurants = ml.FindByFavourites(customer,
                restaurantsInRadius);

            return new OkObjectResult(favouriteRestaurants);
        }

    }
}