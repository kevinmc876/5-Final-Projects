
using Interface.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Interface.Areas.Identity.Data
{
    public static class DbSeeder
    {
        public static async Task SeedRolesAndAdminAsync(IServiceProvider services)
        {
            var userManager = services.GetService<UserManager<InterfaceUser>>();
            RoleManager<IdentityRole> rolemanager = services.GetService<RoleManager<IdentityRole>>();
            await rolemanager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await rolemanager.CreateAsync(new IdentityRole(Roles.Manager.ToString()));

            var user = new InterfaceUser
            {
                UserName = "admin@admin.com",
                Email = "admin@admin.com",
                FirstName = "Test",
                LastName = "Test",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
            };

            var userDb = await userManager.FindByEmailAsync(user.Email);
            if(userDb == null)
            {
                await userManager.CreateAsync(user, "Password1234");
                await userManager.AddToRoleAsync(user, Roles.Admin.ToString());
            }
        }
    }
}
