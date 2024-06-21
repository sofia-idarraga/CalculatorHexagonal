using CalculatorHexagonal.Core.Models;
using CalculatorHexagonal.Core.Services;

namespace CalculatorHexagonal.Application.Services
{
    public class CalculatorService : ICalculatorService
    {
        private readonly IOperationService operationService;

        public CalculatorService(IOperationService operationService)
        {
            this.operationService = operationService;
        }

        public async Task<Result<Operation>> Sum(Operand operand1, Operand operand2)
        {
            if (operand1.IsNull() || operand2.IsNull())
                return Result<Operation>.Error(false, "Operands cannot be null.");

            if (!operand1.IsGreaterThan(0) || !operand2.IsGreaterThan(0))
                return Result<Operation>.Error(false, "Operands cannot be negative.");

            int sum = (int)operand1.Value! + (int)operand2.Value!;


            Operation operation = Operation.Create((int)operand1.Value, (int)operand2.Value, sum, "sum");

            var result = await operationService.Save(operation);

            if(result.Success) return Result<Operation>.Create(true, $"The result of the sum is: {sum}", operation);

            return Result<Operation>.Create(false, $"Operation saving failed.The result of the sum is: {sum}", operation);
        }
    }
}
