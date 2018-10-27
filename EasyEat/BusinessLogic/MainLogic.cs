﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyEat.Models;
using EasyEat.Repositories;

namespace EasyEat.BusinessLogic
{
    public static class MainLogic
    {
        public static int GetCaloricValue(int totalCaloricValue, int mealNumber)
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

        public static int GetTotalCost(int[] costs)
        {
            return costs.Sum();
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

        public static List<Restaurant> FindAppropriateRestaurants(Customer customer, List<Restaurant> restaurantsInRadius, 
            List<Product> products)
        {
            List<Restaurant> resultingRestaurants = new List<Restaurant>();
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
                        resultingRestaurants.Add(restaurant);
                        break;
                    }
                }
            }
            return resultingRestaurants;
        }

    }

}
