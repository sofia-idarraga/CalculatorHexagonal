using CalculatorHexagonal.Core.Models;

namespace CalculatorHexagonal.Core.Services
{
    public interface IValidationService
    {
        Result<bool> ValidateSum(Operand operand1, Operand operand2); 
        Result<bool> ValidateSumComplex(Operand operand1, Operand operand2);
    }
}
