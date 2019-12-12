namespace ClubestApp.Data.Seeding
{
    using ClubestApp.Common;
    using Microsoft.AspNetCore.Identity;
    using System.Linq;
    using System.Threading.Tasks;

    public class AAUserRolesSeeder : ISeeder
    {
        private readonly ApplicationDbContext dbContext;

        public AAUserRolesSeeder(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Seed()
        {
            if (!this.dbContext.Roles.Any())
            {
                this.dbContext.Roles.Add(new IdentityRole
                {
                    Name = UserRoles.SystemAdmin,
                    NormalizedName = UserRoles.SystemAdmin.ToUpper()
                });

                this.dbContext.Roles.Add(new IdentityRole
                {
                    Name = UserRoles.ClubAdmin,
                    NormalizedName = UserRoles.ClubAdmin.ToUpper()
                }); ;
            }

            this.dbContext.SaveChanges();
        }
    }
}