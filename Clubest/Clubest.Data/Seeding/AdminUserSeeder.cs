namespace Clubest.Data.Data.Seeding
{
    using Clubest.Data.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Threading.Tasks;

    public class AdminUserSeeder : ISeeder
    {
        private readonly UserManager<ApplicationUser> userManager;

        public AdminUserSeeder(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }
        public async Task Seed()
        {
            ApplicationUser user = new ApplicationUser
            {
                UserName = "admin@admin.bg",
                Email = "admin@admin.bg",
            };

            IdentityResult result = await this.userManager.CreateAsync(user, "SystemAdmin123+");
            IdentityResult finalResult = await this.userManager.AddToRoleAsync(user, "SystemAdmin");

            if (!finalResult.Succeeded)
            {
                throw new Exception("Admin user was not seeded!");
            }
        }
    }
}