using CalculatorHexagonal.Core.Models;

namespace CalculatorHexagonal.Core.Services
{
    public interface IMathService
    {
        Result<int> Sum(Operand operand1, Operand operand2);
    }
}
