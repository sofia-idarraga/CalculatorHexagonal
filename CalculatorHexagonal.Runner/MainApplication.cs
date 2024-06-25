
using CalculatorHexagonal.Infrastructure.Entrypoint;

namespace CalculatorHexagonal.Runner
{
    public class MainApplication
    {
        private readonly ApplicationConsole console;

        public MainApplication(ApplicationConsole console)
        {
            this.console = console;
        }
        public async Task Run()
        {
            await console.Run();
        }

    }
}
