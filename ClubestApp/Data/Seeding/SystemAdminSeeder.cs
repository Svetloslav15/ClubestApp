namespace ClubestApp.Data.Seeding
{
    using System.Threading.Tasks;
    using ClubestApp.Common;
    using ClubestApp.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Configuration;

    public class SystemAdminSeeder : ISeeder
    {
        private readonly UserManager<User> userManager;

        private IConfiguration configuration;

        public SystemAdminSeeder(UserManager<User> userManager,
                                 IConfiguration configuration)
        {
            this.userManager = userManager;
            this.configuration = configuration;
        }

        public async Task Seed()
        {
            User user = new User()
            {
                UserName = this.configuration.GetConnectionString("SystemAdminEmail"),
                Email = this.configuration.GetConnectionString("SystemAdminEmail"),
                FirstName = UserRoles.SystemAdmin,
                LastName = UserRoles.SystemAdmin
            };

            IdentityResult result = await this.userManager.CreateAsync(user, this.configuration.GetConnectionString("SystemAdminPassword"));
            IdentityResult roleResult = await this.userManager.AddToRoleAsync(user, UserRoles.SystemAdmin);
            System.Console.WriteLine(roleResult.Succeeded);
        }
    }
}