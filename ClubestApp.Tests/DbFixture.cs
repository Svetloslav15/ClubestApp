namespace ClubestApp.Tests
{
    using ClubestApp.Data;
    using ClubestApp.Services;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    
    using System;

    public class DbFixture
    {
        public ServiceProvider ServiceProvider { get; private set; }

        public DbFixture()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection
                .AddDbContext<ApplicationDbContext>(options =>
                        options.UseInMemoryDatabase(Guid.NewGuid().ToString()),
                    ServiceLifetime.Transient);

            IConfigurationBuilder builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.Development.json");

            IConfiguration configuration = builder.Build();
            serviceCollection.AddScoped<IConfiguration>(_ => configuration);
            serviceCollection.AddTransient<ClubService>();

            this.ServiceProvider = serviceCollection.BuildServiceProvider();
        }
    }
}