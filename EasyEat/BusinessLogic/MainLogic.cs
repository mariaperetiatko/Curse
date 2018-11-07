using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyEat.Models;
using EasyEat.Repositories;

namespace EasyEat.BusinessLogic
{
    public class MainLogic
    {
        CartPartRepository db;
        CustomerRepository cr;
        MenuRepository mr;
        DishRepository dr;
        IngredientRepository ir;
        ProductRepository pr;




        public MainLogic()
        {
            db = new CartPartRepository();
            cr = new CustomerRepository();
            mr = new MenuRepository();
            dr = new DishRepository();
            ir = new IngredientRepository();
            pr = new ProductRepository();
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


        public int GetTotalCost(FoodOrder foodOrder)
        {

            Customer customer = cr.GetWholeEntity(foodOrder.CustomerId);
            List<CartPart> cartPart = db.GetWholeEntityByCustomerList(customer).ToList();
            int totalCost = cartPart.Select(x => x.Menu.Cost * x.DishCount).Sum();
            return totalCost;
        }

        public int GetTotalCaloricValue(Cart cart)
        {
            Customer customer = cr.GetWholeEntity(cart.CustomerId);
            List<CartPart> cartParts = db.GetWholeEntityByCustomerList(customer).ToList();

            List<Menu> menues = cartParts.Select(x => x.Menu).ToList();
            for(int i = 0; i < menues.Count(); i++)
                menues[i] = mr.GetWholeEntity(menues[i].Id);

            int totalCaloric = 0;
            List<Dish> dishes = menues.Select(x => x.Dish).ToList();
            for (int i = 0; i < dishes.Count(); i++)
            {
                dishes[i] = dr.GetWholeEntity(dishes[i].Id);
                
                List<Ingredient> ingredients = dishes[i].Ingredient.ToList();                
                for(int j = 0; j < ingredients.Count(); j++)
                {
                    Product product = pr.GetEntity(ingredients[j].ProductId);
                    totalCaloric += product.CaloricValue;
                }
            }

            //for ()

            //int totalCaloricValue = dishes.Select(x => x.Ingredient.Select(y => y.Product.CaloricValue).Sum()).Sum();
            
            return totalCaloric;
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
