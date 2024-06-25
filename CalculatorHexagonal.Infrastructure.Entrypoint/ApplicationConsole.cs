using CalculatorHexagonal.Core.Models;
using CalculatorHexagonal.Core.UseCases;

namespace CalculatorHexagonal.Infrastructure.Entrypoint
{
    public class ApplicationConsole
    {
        private readonly ICalculatorUseCase _calculatorService;
        private readonly IOperationUseCase _operationService;

        public ApplicationConsole(ICalculatorUseCase calculatorService, IOperationUseCase operationService)
        {
            _calculatorService = calculatorService;
            _operationService = operationService;
        }
        public async Task Run()
        {
            Console.WriteLine("Console Calculator\r");
            Console.WriteLine("------------------------\n");

            while (true)
            {
                Console.WriteLine("------------------------\n");
                Console.WriteLine("Choose an option from the following list:");
                Console.WriteLine("\ta - Add");
                Console.WriteLine("\tb - Get Operations");
                string op = Console.ReadLine();
                await DoOperation(op);
            }
        }

        private async Task DoOperation(string operation)
        {
            switch (operation)
            {
                case "a":
                    await Sum();
                    break;
                case "b":
                    await ListOperations();
                    break;
                default:
                    break;
            }
        }

        private async Task Sum()
        {

            int? operand1Value = ReadOperand("Enter the first operand:");
            int? operand2Value = ReadOperand("Enter the second operand:");

            Operand operand1 = new Operand(operand1Value);
            Operand operand2 = new Operand(operand2Value);

            var result = await _calculatorService.Sum(operand1, operand2);

            if (result.Success)
            {
                Console.WriteLine(result.Message);
            }
            else
            {
                Console.WriteLine($"Error: {result.Message}");
            }
        }

        private int? ReadOperand(string message)
        {
            Console.WriteLine(message);
            if (!int.TryParse(Console.ReadLine(), out int operandValue))
            {
                Console.WriteLine("Invalid input. It will be taken as null.");
                return null;
            }
            return operandValue;
        }

        private async Task ListOperations()
        {
            Console.WriteLine("Enter the start date and time (e.g., 2024-06-18T10:00:00):");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime initDate))
            {
                Console.WriteLine("Invalid start date and time.");
                return;
            }

            Console.WriteLine("Enter the end date and time (e.g., 2024-06-18T18:00:00):");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime endDate))
            {
                Console.WriteLine("Invalid end date and time.");
                return;
            }

            var result = await _operationService.FindByDate(initDate, endDate);

            if (result.Success)
            {
                Console.WriteLine(result.Message);
                foreach (var operation in result.Value)
                {

                    Console.WriteLine(operation.ToString());
                }
            }
            else
            {
                Console.WriteLine($"Error retrieving operations: {result.Message}");
            }

        }
    }
}
