using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EasyEat.Models
{
    public static class Seed

    {

        public static async Task CreateRoles(IServiceProvider serviceProvider, IConfiguration Configuration)

        {

            //adding customs roles

            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<User>>();
            string[] roleNames = { "Admin", "Manager", "Member" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    //create the roles and seed them to the database: Question 1
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
            var poweruser = new User
            {
                //FirstName = Configuration.GetSection("userAppSettingOptions")["FirstName"],
                //LastName = Configuration.GetSection("userAppSettingOptions")["LastName"],
                UserName = Configuration.GetSection("userAppSettingOptions")["Email"],
                Email = Configuration.GetSection("userAppSettingOptions")["Email"],
            };
            //Here you could create a super user who will maintain the web app
            var Email = Configuration.GetSection("userAppSettingOptions")["Email"];

            //Ensure you have these values in your appsettings.json file
            string userPWD = Configuration.GetSection("userAppSettingOptions")["Password"];
            var _user = await UserManager.FindByEmailAsync(Email);

            if (_user == null)
            {
                var createPowerUser = await UserManager.CreateAsync(poweruser, userPWD);
                if (createPowerUser.Succeeded)
                {
                    //here we tie the new user to the role
                    await UserManager.AddToRoleAsync(poweruser, "Admin");

                }
            }
        }

    }



}
