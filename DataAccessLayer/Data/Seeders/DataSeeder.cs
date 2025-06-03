
using Microsoft.AspNetCore.Identity;
using ProjectAPI.DataAccessLayer.Data.Context;
using ProjectAPI.DataAccessLayer.Data.Models;

namespace ProjectAPI.DataAccessLayer.Data.Seeders
{
    public class DataSeeder : IDataSeeder
    {
        private readonly StoreContext _storeContext;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DataSeeder(StoreContext storeContext, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _storeContext = storeContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task InitializeIdentityAsync()
        {
            try
            {
                if (!_roleManager.Roles.Any())
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    await _roleManager.CreateAsync(new IdentityRole("User"));
                }

                if (!_userManager.Users.Any())
                {
                    var admin = new User
                    {
                        FirstName = "Admin",
                        LastName = "User",
                        Email = "admin@jupitersoftware.net",
                        UserName = "admin",
                    };

                    var adminResult = await _userManager.CreateAsync(admin, "admin@123");


                    if (adminResult.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(admin, "Admin");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while seeding data: {ex.Message}");
                throw;
            }
        }
    }
}