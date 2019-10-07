namespace Clubest.Data.Data.Seeding
{
    using Microsoft.AspNetCore.Identity;
    using System.Linq;
    using System.Threading.Tasks;

    public class AAUserRolesSeeder : ISeeder
    {
        private readonly ClubestDbContext dbContext;

        public AAUserRolesSeeder(ClubestDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Seed()
        {
            if (!dbContext.Roles.Any())
            {
                dbContext.Roles.Add(new IdentityRole
                {
                    Name = "SystemAdmin",
                    NormalizedName = "SYSTEMADMIN"
                });
            }
            dbContext.SaveChanges();
        }
    }
}
