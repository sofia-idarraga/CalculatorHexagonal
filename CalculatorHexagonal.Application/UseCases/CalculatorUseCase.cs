using CalculatorHexagonal.Core.Models;
using CalculatorHexagonal.Core.Services;
using CalculatorHexagonal.Core.UseCases;
using System.Numerics;

namespace CalculatorHexagonal.Application.Services
{
    public class CalculatorUseCase : ICalculatorUseCase
    {
        private readonly IOperationUseCase operationUseCase;
        private readonly IValidationService validationService;
        private readonly IMathService mathService;

        public CalculatorUseCase(IOperationUseCase operationService, IValidationService validationService, IMathService mathService)
        {
            this.operationUseCase = operationService;
            this.validationService = validationService;
            this.mathService = mathService;
        }

        public async Task<Result<Operation>> Sum<T>(Operand operand1, Operand operand2)
        {
            Result<bool> validation = validationService.ValidateSum(operand1, operand2);

            if (!validation.Success) return Result<Operation>.Error(validation.Message);

            Result<T> sumResult = mathService.Sum<T>(operand1, operand2);

            if (!sumResult.Success) return Result<Operation>.Error(sumResult.Message);

            
            Operation operation = Operation.Create(Convert.ToString(operand1.Value), Convert.ToString(operand2.Value), Convert.ToString(sumResult.Value), "sum");

            var result = await operationUseCase.Save(operation);

            if (result.Success) return Result<Operation>.Create(true, $"The result of the sum is: {sumResult.Value}", operation);

            return Result<Operation>.Create(false, $"Operation saving failed.The result of the sum is: {sumResult.Value}", operation);
        }

        public async Task<Result<Operation>> SumComplex<Complex>(Operand operand1, Operand operand2)
        {
            Result<bool> validation = validationService.ValidateSumComplex(operand1, operand2);

            if (!validation.Success) return Result<Operation>.Error(validation.Message);

            Result<Complex> sumResult = mathService.Sum<Complex>(operand1, operand2);

            if (!sumResult.Success) return Result<Operation>.Error(sumResult.Message);


            Operation operation = Operation.Create(Convert.ToString(operand1.Value), Convert.ToString(operand2.Value), Convert.ToString(sumResult.Value), "sum complex");

            var result = await operationUseCase.Save(operation);

            if (result.Success) return Result<Operation>.Create(true, $"The result of the sum is: {sumResult.Value}", operation);

            return Result<Operation>.Create(false, $"Operation saving failed. The result of the sum is: {sumResult.Value}", operation);
        }
    }
}
