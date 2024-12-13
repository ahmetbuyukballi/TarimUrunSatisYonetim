using Domain.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class DataSeeder
    {   private readonly UserManager<AppUser> _userManager; 
        private readonly RoleManager<AppRoles> _roleManager;
        public DataSeeder(UserManager<AppUser> userManager,RoleManager<AppRoles> roleManager) 
        { 
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task SeedAsync()
        {
            var AdminUser = await _userManager.FindByNameAsync("Admin");
            if (AdminUser == null)
            {
                AdminUser = new AppUser
                {
                    Name = "Admin",
                    Surname = "Admin",
                    Email = "admin@example.com",
                    UserName = "admin@example.com",

                };
            }
            var result=await _userManager.CreateAsync(AdminUser,"admin123");
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(AdminUser, "Admin");
            }
        }
    }
}
