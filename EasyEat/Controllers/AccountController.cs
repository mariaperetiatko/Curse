using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using EasyEat.Models;
using EasyEat.ViewModels;
using EasyEat.Helpers;

using AutoMapper;

namespace EasyEat.Controllers
{
    [Produces("application/json")]
    [Route("api/Account")]
    public class AccountController : Controller
    {
        private readonly EatContext _appDbContext;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;


        public AccountController(UserManager<User> userManager, IMapper mapper, EatContext appDbContext)
        {
            _userManager = userManager;
            _mapper = mapper;
            _appDbContext = appDbContext;
        }

        // POST api/accounts
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]RegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userIdentity = _mapper.Map<User>(model);

            var result = await _userManager.CreateAsync(userIdentity, model.Password);

            if (!result.Succeeded) return new BadRequestObjectResult(Errors.AddErrorsToModelState(result, ModelState));

            result = await _userManager.AddToRoleAsync(userIdentity, model.Role);

            if (model.Role == "member")
            {

                Customer customer = new Customer
                {
                    IdentityId = userIdentity.Id,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Phone = model.Phone,
                    IsDeleted = 0,
                    Balance = 0

                };
                await _appDbContext.Customer.AddAsync(customer);

                //Customer cust = _appDbContext.Customer.Where(x => x.IdentityId == customer.IdentityId)
                //    .SingleOrDefault();
                await _appDbContext.SaveChangesAsync();


                _appDbContext.Cart.Add( new Cart
                {
                    CustomerId = customer.Id,
                    TotalCaloricValue = 0,
                    AddressId = 0,
                    MealTimeId = 0,
                    DeliveryDate = DateTime.Now
                });

                //var e = await _appDbContext.Cart.AddAsync(cart);
                try
                {
                    var x = await _appDbContext.SaveChangesAsync();
                } catch(Exception e)
                {
                    return new ObjectResult(e.Message);
                }


                //await _appDbContext.Cart.AddAsync(new Cart
                //{
                //    CustomerId = customer.Id,
                //    Customer = customer,
                //    TotalCaloricValue = 0,
                //    AddressId = 0,
                //    MealTimeId = 0,
                //    DeliveryDate = DateTime.Now

                //});
            }
            await _appDbContext.SaveChangesAsync();

            return new OkObjectResult("Account created");
        }
    }
}