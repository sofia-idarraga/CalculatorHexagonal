// See https://aka.ms/new-console-template for more information
using CalculatorHexagonal.Application.Services;
using CalculatorHexagonal.Core.Adapters;
using CalculatorHexagonal.Core.Services;
using CalculatorHexagonal.Infrastructure.Data.Adapters;
using CalculatorHexagonal.Infrastructure.Entrypoint.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CalculatorHexagonal.Infrastructure.Data.Contexts;


namespace CalculatorHexagonal.Infrastructure.Entrypoint
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();

            IConfiguration configuration;
            configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
            serviceCollection.AddSingleton<IConfiguration>(configuration);
            serviceCollection.AddScoped<ICalculatorService, CalculatorService>();
            serviceCollection.AddScoped<IOperationService, OperationService>();
            serviceCollection.AddScoped<IOperationAdapter, OperationAdapter>();


            var connectionString =
                configuration["DB_CONNECTION"] ??
                configuration.GetSection("ConnectionStrings:Default").Value;
            //var connectionString = "Server=mysql;port=3306;Database=develop;User=root;Password=12345;";
            serviceCollection.AddDbContext<OperationDbContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));



            var serviceProvider = serviceCollection.BuildServiceProvider();
            var calculatorService = serviceProvider.GetRequiredService<ICalculatorService>();
            var operationService = serviceProvider.GetRequiredService<IOperationService>();
            var app = new MainApplication(calculatorService, operationService);

            await app.Run();
        }
    }
}