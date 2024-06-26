using CalculatorHexagonal.Core.Models;

namespace CalculatorHexagonal.Core.Services
{
    public interface IMathService
    {
        Result<T> Sum<T>(Operand operand1, Operand operand2);
    }
}
