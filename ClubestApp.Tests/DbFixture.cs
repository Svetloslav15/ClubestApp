using ClubestApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ClubestApp.Tests
{
    public class DbFixture 
    {
        public DbFixture()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection
                .AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase(Guid.NewGuid().ToString()),
                    ServiceLifetime.Transient);

            var builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.Development.json");

            var configuration = builder.Build();
            serviceCollection.AddScoped<IConfiguration>(_ => configuration);

            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        public ServiceProvider ServiceProvider { get; private set; }
    }
}
