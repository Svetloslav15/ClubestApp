namespace ClubestApp.Extensions
{
    using ClubestApp.Data;
    using ClubestApp.Data.Seeding;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using System.Linq;
    using System.Reflection;

    public static class ApplicationBuilderExtensions
    {
        public static void UseDatabaseSeeding(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider
                    .GetRequiredService<ApplicationDbContext>();

                context.Database.EnsureCreated();

                Assembly.GetAssembly(typeof(ApplicationDbContext))
                    .GetTypes()
                    .Where(type => typeof(ISeeder).IsAssignableFrom(type))
                    .Where(type => type.IsClass)
                    .Select(type => (ISeeder)serviceScope.ServiceProvider.GetRequiredService(type))
                    .ToList()
                    .ForEach(seeder => seeder.Seed()
                                             .GetAwaiter()
                                             .GetResult());
            }
        }
    }
}