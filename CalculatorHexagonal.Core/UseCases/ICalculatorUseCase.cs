using CalculatorHexagonal.Core.Models;

namespace CalculatorHexagonal.Core.UseCases
{
    public interface ICalculatorUseCase
    {
        Task<Result<Operation>> Sum(Operand operand1, Operand operand2);
    }
}
