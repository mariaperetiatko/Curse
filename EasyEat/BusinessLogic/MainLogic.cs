﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyEat.Models;
using EasyEat.Repositories;

namespace EasyEat.BusinessLogic
{
    public class MainLogic
    {
        IRepository<CartPart> db;

        public MainLogic()
        {
            db = new CartPartRepository();
        }
        public int GetCaloricValue(int totalCaloricValue, int mealNumber)
        {
            switch(mealNumber)
            {
                case 0:
                    return (int)(totalCaloricValue * 0.3);
                case 1:
                    return (int)(totalCaloricValue * 0.45);
                case 2:
                    return (int)(totalCaloricValue * 0.25);
                default:
                    return 0;
            }
        }


        public static int GetTotalCost(FoodOrder foodOrder)
        {
            
            Customer customer = foodOrder.Customer;
            Cart cart = customer.Cart;
            List<CartPart> cartPart = cart.CartPart.ToList();
            int totalCost = cartPart.Select(x => x.Menu.Cost * x.DishCount).Sum();
            return totalCost ;
        }

        public int GetTotalCaloricValue(Cart cart)
        {
            List<CartPart> cartParts = cart.CartPart.ToList();
            for(int i = 0; i < cartParts.Count(); i++)
            {
                cartParts[i] = db.GetEntity(new { cartParts[i].MenuId, cartParts[i].CartId });
            }
            List<Menu> menues = cartParts.Select(x => x.Menu).ToList();
            List<Dish> dishes = menues.Select(x => x.Dish).ToList();
            int totalCaloricValue = dishes.Select(x => x.Ingredient.Select(y => y.Product.CaloricValue).Sum()).Sum();
            
            return totalCaloricValue;
        }


        public static int RefreshBalance(int balance, int moneyValue)
        {
            return balance + moneyValue;
        }


        public static List<Restaurant> FindInRadius(double customerXCoordinate, double customerYCoordinate, 
                                             List<Restaurant> allRestaurants,int radius)
        {
            List<Restaurant> resultingRestaurants = new List<Restaurant>();
            foreach (Restaurant restaurant in allRestaurants)
            {
                if(Math.Sqrt(Math.Pow(restaurant.Xcoordinate - customerXCoordinate, 2) +
                        Math.Pow(restaurant.Ycoordinate - customerYCoordinate, 2)) <= radius)
                {
                    resultingRestaurants.Add(restaurant);
                }
            }
            return resultingRestaurants; 
        }


        public static List<int> FindAppropriateRestaurants(Customer customer, List<Restaurant> restaurantsInRadius, 
            List<Product> products)
        {
            List<int> resultingRestaurants = new List<int>();
            List<int> allowedProductIds = customer.SpecialProduct.Where(x => x.Allowance == 1)
                .Select(x => x.ProductId).ToList();
            List<Product> allowedProducts = products.Where(x => allowedProductIds.Contains(x.Id))
                .ToList();

            foreach (Restaurant restaurant in restaurantsInRadius)
            {
                foreach (Menu menu in restaurant.Menu)
                {
                    bool isAppropriate = true;

                    foreach (Ingredient ingredient in menu.Dish.Ingredient)
                    {
                        if (!allowedProducts.Contains(ingredient.Product))
                        {
                            isAppropriate = false;
                            break;
                        }                      
                    }
                    if (isAppropriate)
                    {
                        resultingRestaurants.Add(restaurant.Id);
                        break;
                    }
                }
            }
            return resultingRestaurants;
        }


        public static List<int> FindByFavourites(Customer customer, List<Restaurant> restaurantsInRadius)
        {
            List<int> resultingRestaurants = new List<int>();
            List<int> favouriteDishesId = customer.FavouriteDish.Select(x => x.DishId).ToList();

            foreach (Restaurant restaurant in restaurantsInRadius)
            {
                bool isAppropriate = false;

                foreach (Menu menu in restaurant.Menu)
                {
                    if (favouriteDishesId.Contains(menu.DishId))
                    {
                        isAppropriate = true;
                        break;
                    }

                }

                if (isAppropriate)
                {
                    resultingRestaurants.Add(restaurant.Id);
                    break;
                }
            }
            return resultingRestaurants;
        }
    }

}
