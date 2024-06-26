using CalculatorHexagonal.Core.Models;

namespace CalculatorHexagonal.Core.UseCases
{
    public interface ICalculatorUseCase
    {
        Task<Result<Operation>> Sum<T>(Operand operand1, Operand operand2);
        Task<Result<Operation>> SumComplex<Complex>(Operand operand1, Operand operand2);

    }
}
