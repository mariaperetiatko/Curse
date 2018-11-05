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
        IRepository<FoodOrder> dbFoodOrder;
        IRepository<Customer> dbCustomer;


        public FinancialController()
        {
            dbFoodOrder = new FoodOrderRepository();
            dbCustomer = new CustomerRepository();
        }

        [HttpGet("{orderId}")]
        [Route("Pay")]
        public IActionResult Pay(int orderId)
        {
            FoodOrder foodOrder = dbFoodOrder.GetEntity(orderId);
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

        [HttpGet("{customerId}, {moneySum}")]
        [Route("IncreaseBalance")]
        public IActionResult IncreaseBalance(int customerId, int moneySum)
        {
            if (moneySum <= 0)
                return new ObjectResult("MoneySum must be positive!");

            Customer customer = dbCustomer.GetEntity(customerId);
            int newBalance = MainLogic.RefreshBalance((int)customer.Balance, -moneySum);
            customer.Balance = newBalance;
            dbCustomer.Update(customer);
            dbCustomer.Save();
            dbFoodOrder.Save();
            return new ObjectResult("Succesful operation!");
        }
    }
}