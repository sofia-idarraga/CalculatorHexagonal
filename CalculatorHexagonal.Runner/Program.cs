using CalculatorHexagonal.Application.Services;
using CalculatorHexagonal.Core.Adapters;
using CalculatorHexagonal.Infrastructure.Data.Adapters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CalculatorHexagonal.Infrastructure.Data.Contexts;
using CalculatorHexagonal.Infrastructure.Entrypoint;
using CalculatorHexagonal.Core.UseCases;
using CalculatorHexagonal.Core.Services;

namespace CalculatorHexagonal.Runner
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

            serviceCollection.AddSingleton<IValidationService, ValidationService>();
            serviceCollection.AddSingleton<IMathService, MathService>();

            serviceCollection.AddSingleton<ICalculatorUseCase, CalculatorUseCase>();
            serviceCollection.AddSingleton<IOperationUseCase, OperationUseCase>();
            
            serviceCollection.AddSingleton<IOperationAdapter, OperationAdapter>();
            serviceCollection.AddSingleton<ApplicationConsole>();


            var connectionString =
                configuration["DB_CONNECTION"] ??
                configuration.GetSection("ConnectionStrings:Default").Value;
            serviceCollection.AddDbContext<OperationDbContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

            var serviceProvider = serviceCollection.BuildServiceProvider();
            var calculatorService = serviceProvider.GetRequiredService<ICalculatorUseCase>();
            var operationService = serviceProvider.GetRequiredService<IOperationUseCase>();
            var console = new ApplicationConsole(calculatorService,operationService);
            var app = new MainApplication(console);

            await app.Run();
        }
    }
}