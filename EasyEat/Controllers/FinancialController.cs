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
using EasyEat.BusinessLogic;

namespace EasyEat.Controllers
{
    [Produces("application/json")]
    [Route("api/Financial")]
    public class FinancialController : Controller
    {
        FoodOrderRepository dbFoodOrder;
        IRepository<Customer> dbCustomer;


        public FinancialController()
        {
            dbFoodOrder = new FoodOrderRepository();
            dbCustomer = new CustomerRepository();
        }

        //[Route("Pay")]
        [HttpGet("Pay/{orderId}")]
        public IActionResult Pay(int orderId)
        {
            FoodOrder foodOrder = dbFoodOrder.GetWholeEntity(orderId);
            if (foodOrder.TotalCost > foodOrder.Customer.Balance)
            {
                return new ObjectResult("You can not pay, your balance is less that total cost!");
            }
            int newBalance = MainLogic.RefreshBalance((int)foodOrder.Customer.Balance, -foodOrder.TotalCost);
            Customer customer = foodOrder.Customer;
            
            customer.Balance = newBalance;
            dbCustomer.Update(customer);
            dbCustomer.Save();
            dbFoodOrder.Delete(orderId);
            dbFoodOrder.Save();
            return new ObjectResult("Succesful pay!");
        }

        [HttpGet("IncreaseBalance/{customerId}, {moneySum}")]
        //[Route("IncreaseBalance")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public IActionResult IncreaseBalance(int customerId, int moneySum)
        {
            if (moneySum <= 0)
                return new ObjectResult("MoneySum must be positive!");

            Customer customer = dbCustomer.GetEntity(customerId);
            int newBalance = MainLogic.RefreshBalance((int)customer.Balance, moneySum);
            customer.Balance = newBalance;
            dbCustomer.Update(customer);
            dbCustomer.Save();
            dbFoodOrder.Save();
            return new ObjectResult("Succesful operation!");
        }
    }
}