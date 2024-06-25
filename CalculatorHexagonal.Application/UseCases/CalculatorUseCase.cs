using CalculatorHexagonal.Core.Models;
using CalculatorHexagonal.Core.Services;
using CalculatorHexagonal.Core.UseCases;

namespace CalculatorHexagonal.Application.Services
{
    public class CalculatorUseCase : ICalculatorUseCase
    {
        private readonly IOperationUseCase operationService;
        private readonly IValidationService validationService;
        private readonly IMathService mathService;

        public CalculatorUseCase(IOperationUseCase operationService, IValidationService validationService, IMathService mathService)
        {
            this.operationService = operationService;
            this.validationService = validationService;
            this.mathService = mathService;
        }

        public async Task<Result<Operation>> Sum(Operand operand1, Operand operand2)
        {
            Result<bool> validation = validationService.ValidateSum(operand1, operand2);

            if (!validation.Success) return Result<Operation>.Error(validation.Message); 

            Result<int> sumResult = mathService.Sum(operand1, operand2);

            if (!sumResult.Success) return Result<Operation>.Error(validation.Message);          


            Operation operation = Operation.Create((int)operand1.Value, (int)operand2.Value, sumResult.Value, "sum");      

            var result = await operationService.Save(operation);

            if(result.Success) return Result<Operation>.Create(true, $"The result of the sum is: {sumResult.Value}", operation);

            return Result<Operation>.Create(false, $"Operation saving failed.The result of the sum is: {sumResult.Value}", operation);
        }
    }
}
