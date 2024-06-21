using CalculatorHexagonal.Core.Models;

namespace CalculatorHexagonal.Core.Services
{
    public interface ICalculatorService
    {
        Task<Result<Operation>> Sum(Operand operand1, Operand operand2);
    }
}
