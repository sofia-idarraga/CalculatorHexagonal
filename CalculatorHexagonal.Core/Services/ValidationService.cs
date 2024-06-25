using CalculatorHexagonal.Core.Models;

namespace CalculatorHexagonal.Core.Services
{
    public class ValidationService : IValidationService
    {
        public Result<bool> ValidateSum(Operand operand1, Operand operand2)
        {
            if (IsNull(operand1, operand2))
                return Result<bool>.Error("Operands cannot be null.");

            if (IsNegative(operand1, operand2))
                return Result<bool>.Error("Operands cannot be negative.");

            return Result<bool>.Create(true);
        }

        private static bool IsNull(Operand operand1, Operand operand2)
        {
            return operand1.IsNull() || operand2.IsNull();
        }

        private static bool IsNegative(Operand operand1, Operand operand2)
        {
            return !operand1.IsGreaterThan(0) || !operand2.IsGreaterThan(0);
        }
    }
}
