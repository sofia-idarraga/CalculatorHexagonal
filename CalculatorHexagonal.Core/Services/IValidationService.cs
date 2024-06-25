using CalculatorHexagonal.Core.Models;

namespace CalculatorHexagonal.Core.Services
{
    public interface IValidationService
    {
        Result<bool> ValidateSum(Operand operand1, Operand operand2);
    }
}
